using System;
using System.Reflection;
using System.Windows.Media.Media3D;

namespace Dimension3D.Core
{
    internal static class ModelVisual3DExtensions
    {
        private static readonly PropertyInfo VisualDescendantBounds;
        private static readonly PropertyInfo VisualContentBounds;

        static ModelVisual3DExtensions()
        {
            VisualDescendantBounds = typeof(ModelVisual3D).GetProperty("VisualDescendantBounds", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic) ?? throw new InvalidProgramException("VisualDescendantBounds");
            VisualContentBounds = typeof(ModelVisual3D).GetProperty("VisualContentBounds", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic) ?? throw new InvalidProgramException("VisualContentBounds");
        }


        internal static Rect3D GetBounds(this ModelVisual3D model, Transform3D? transform = null)
        {

            var contentBounds = model.GetVisualContentBounds();
            var childBounds = model.GetVisualDescendantBounds();
            var rec = Rect3D.Empty;
            rec.Union(contentBounds);
            rec.Union(childBounds);


            if (transform != null)
            {

            }
            return rec;
        }



        private static Rect3D GetVisualContentBounds(this ModelVisual3D model)
        {
            var result = VisualContentBounds.GetValue(model);
            if (result is Rect3D rect)
                return rect;

            return Rect3D.Empty;
        }

        private static Rect3D GetVisualDescendantBounds(this ModelVisual3D model)
        {
            var result = VisualDescendantBounds.GetValue(model);
            if (result is Rect3D rect)
                return rect;

            return Rect3D.Empty;
        }
    }
}
