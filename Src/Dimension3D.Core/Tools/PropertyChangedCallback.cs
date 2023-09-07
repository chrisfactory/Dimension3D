using System.Windows;

namespace Dimension3D.Core
{
    public delegate void PropertyChangedCallback<T>(T d, DependencyPropertyChangedEventArgs e)
        where T : DependencyObject;
}
