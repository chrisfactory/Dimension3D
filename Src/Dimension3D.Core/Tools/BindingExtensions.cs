using System.Windows.Data;
using System.Windows;

namespace Dimension3D.Core.Tools
{
    internal static class BindingExtensions
    {
        internal static void SetBindingTo(this DependencyObject target, DependencyProperty targetProp, DependencyProperty path, DependencyObject source)
        {
            if (!BindingOperations.IsDataBound(target, targetProp))
                BindingOperations.SetBinding(target, targetProp, new Binding()
                {
                    Source = source,
                    Path = new PropertyPath(path)
                });

        }
    }
}
