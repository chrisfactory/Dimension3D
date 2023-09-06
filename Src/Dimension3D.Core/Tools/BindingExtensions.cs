using System.Windows.Data;
using System.Windows;

namespace Dimension3D.Core.Tools
{
    internal static class BindingExtensions
    {
        internal static BindingExpressionBase SetBindingTo(this DependencyObject source, DependencyProperty from, DependencyProperty to, DependencyObject target)
        {
            if (BindingOperations.IsDataBound(target, to))
                BindingOperations.ClearBinding(target, to);

            return BindingOperations.SetBinding(target, to, new Binding()
            {
                Source = source,
                Path = new PropertyPath(from)
            });

        }
    }
}
