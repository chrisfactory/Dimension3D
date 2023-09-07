using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Dimension3D.Core
{
    public class FrameworkPropertyMetadata<T> : FrameworkPropertyMetadata
        where T : DependencyObject
    {
   
        public FrameworkPropertyMetadata(PropertyChangedCallback<T> propertyChangedCallback) : base(Convert(propertyChangedCallback)) { } 
        public FrameworkPropertyMetadata(PropertyChangedCallback<T> propertyChangedCallback, CoerceValueCallback<T> coerceValueCallback) : base(Convert(propertyChangedCallback), Convert(coerceValueCallback)) { }
        public FrameworkPropertyMetadata(object? defaultValue, PropertyChangedCallback<T> propertyChangedCallback) : base(defaultValue, Convert(propertyChangedCallback)) { }
        public FrameworkPropertyMetadata(object? defaultValue, PropertyChangedCallback<T> propertyChangedCallback, CoerceValueCallback<T> coerceValueCallback) : base(defaultValue, Convert(propertyChangedCallback), Convert(coerceValueCallback)) { }
        public FrameworkPropertyMetadata(object? defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback<T> propertyChangedCallback) : base(defaultValue, flags, Convert(propertyChangedCallback)) { }
        public FrameworkPropertyMetadata(object? defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback<T> propertyChangedCallback, CoerceValueCallback<T> coerceValueCallback) : base(defaultValue, flags, Convert(propertyChangedCallback), Convert(coerceValueCallback)) { }
        public FrameworkPropertyMetadata(object? defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback<T> propertyChangedCallback, CoerceValueCallback<T> coerceValueCallback, bool isAnimationProhibited) : base(defaultValue, flags, Convert(propertyChangedCallback), Convert(coerceValueCallback), isAnimationProhibited) { }
        public FrameworkPropertyMetadata(object? defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback<T> propertyChangedCallback, CoerceValueCallback<T> coerceValueCallback, bool isAnimationProhibited, UpdateSourceTrigger defaultUpdateSourceTrigger) : base(defaultValue, flags, Convert(propertyChangedCallback), Convert(coerceValueCallback), isAnimationProhibited, defaultUpdateSourceTrigger) { }

         

        private static PropertyChangedCallback Convert(PropertyChangedCallback<T> propertyChangedCallback) => new PropertyChangedCallback((d, e) => propertyChangedCallback((T)d, e));
        private static CoerceValueCallback Convert(CoerceValueCallback<T> coerceValueCallback) => new CoerceValueCallback((d, e) => coerceValueCallback((T)d, e));
         
    }
}
