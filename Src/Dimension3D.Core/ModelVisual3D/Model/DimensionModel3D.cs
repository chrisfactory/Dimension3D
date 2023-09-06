﻿using System;
using System.Windows;

namespace Dimension3D.Core
{
    public abstract class DimensionModel3D : DimensionElement3D
    {
        public static Type _typeofThis = typeof(DimensionModel3D);
        static DimensionModel3D()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofThis, new FrameworkPropertyMetadata(_typeofThis));
        }
    }
}
