using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices; 

namespace Dimension3D.Samples
{
    public class ErrorResult
    {
        public ErrorResult(IEnumerable errors)
        {
            Errors = errors;
            if (errors == null)
                IsEmpty = true;
            else
                IsEmpty = !errors.GetEnumerator().MoveNext();
        }
        public bool IsEmpty { get; }
        public IEnumerable Errors { get; }
    }
    public abstract partial class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
    {
        private readonly List<DelegateCommand> _commands = new List<DelegateCommand>();
        private readonly List<string> _propertiesErrors = new List<string>();
        private readonly Dictionary<string, IEnumerable> _errors = new Dictionary<string, IEnumerable>();
        private PropertyChangingEventHandler? _PropertyChanging;
        private EventHandler<DataErrorsChangedEventArgs>? _ErrorsChanged;
        private PropertyChangedEventHandler? _PropertyChanged;
        private bool _HasErrors;
        #region INotifyPropertyChanging
        event PropertyChangingEventHandler? INotifyPropertyChanging.PropertyChanging { add => _PropertyChanging += value; remove => _PropertyChanging -= value; }
        private void InnerOnPropertyChanging(string propertyName)
        {
            _PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            OnPropertyChanging(propertyName);
        }
        protected virtual void OnPropertyChanging(string propertyName) { }
        #endregion

        #region INotifyDataErrorInfo
        public bool HasErrors { get => _HasErrors; set => NotifySet(ref _HasErrors, value); }
        event EventHandler<DataErrorsChangedEventArgs>? INotifyDataErrorInfo.ErrorsChanged { add => _ErrorsChanged += value; remove => _ErrorsChanged -= value; }
        IEnumerable? INotifyDataErrorInfo.GetErrors(string? propertyName) => _errors.TryGetValue((propertyName ?? string.Empty), out IEnumerable? errors) ? errors : default(IEnumerable?);
        private void InnerOnErrorsChanged(string? propertyName)
        {
            _ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnErrorsChanged(propertyName);
        }
        protected virtual void OnErrorsChanged(string? propertyName) { }
        protected virtual ErrorResult? OnGetPropertyErrors(string? propertyName) => null;
        protected void RefreshErrors(string? propertyName = null)
        {
            var errors = OnGetPropertyErrors(propertyName);
            bool changed = true;
            if (errors == null || errors.IsEmpty)
                changed = _errors.Remove(propertyName ?? string.Empty);
            else
                _errors[propertyName ?? string.Empty] = errors.Errors;
            HasErrors = _errors.Any();
            if (changed)
                InnerOnErrorsChanged(propertyName);
        }
        protected void RegisterPropertiesErrors(params string[] properties)
        {
            if (properties == null)
                return;
            _propertiesErrors.AddRange(properties.Distinct());
        }
        protected void RefreshAllErrors()
        {
            var keys = _propertiesErrors.ToList();
            keys.AddRange(_errors.Keys.ToList());
            keys = keys.Distinct().ToList();
            foreach (string key in keys)
                RefreshErrors(key == string.Empty ? null : key);
        }
        protected void ClearAllErrors()
        {
            var keys = _errors.Keys.ToList();
            _errors.Clear();
            HasErrors = false;
            foreach (string key in keys)
                InnerOnErrorsChanged(key == string.Empty ? null : key);
        }
        #endregion

        #region INotifyPropertyChanged 
        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged { add => _PropertyChanged += value; remove => _PropertyChanged -= value; }
        private void InnerOnPropertyChanged(string propertyName)
        {
            _PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(propertyName);
            if (propertyName == nameof(HasErrors))
                this.RaiseCanExecuteChanged();
        }
        protected virtual void OnPropertyChanged(string propertyName) { }
        #endregion

        #region ICommand
        protected void RegisterCommands(params DelegateCommand[] commands)
        {
            if (commands == null)
                return;
            this._commands.AddRange(commands.Distinct());
        }
        protected virtual void RaiseCanExecuteChanged()
        {
            var cmds = _commands.ToList();
            foreach (var cmd in cmds)
                cmd.RaiseCanExecuteChanged();
        }
        #endregion

        protected void NotifySet<T>(ref T attribute, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (object.Equals(attribute, newValue)) return;

            if (!string.IsNullOrEmpty(propertyName))
                InnerOnPropertyChanging(propertyName);

            attribute = newValue;

            RefreshErrors(propertyName);

            if (!string.IsNullOrEmpty(propertyName))
                InnerOnPropertyChanged(propertyName);
        }

    }
}
