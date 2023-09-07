using System.Windows;

namespace Dimension3D.Core
{
    public delegate object CoerceValueCallback<T>(T d, object baseValue) where T : DependencyObject;
}
