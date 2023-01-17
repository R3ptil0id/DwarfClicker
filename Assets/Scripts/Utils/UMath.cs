using UnityEngine;
using Utils.EnumExtensions;

namespace Utils
{
    public static class UMath
    {
        public static Vector3 BezierCubicEaseOutBack(float t, Vector3 start, Vector3 middle, Vector3 end)
        {
            var xSm = EasingFunction.EaseOutBack(start.x, middle.x, t);
            var ySm = EasingFunction.EaseOutBack(start.y, middle.y, t);
            var xMe = EasingFunction.EaseOutBack(middle.x, end.x, t);
            var yMe = EasingFunction.EaseOutBack(middle.y, end.y, t);
            var v1 = new Vector3(xSm, ySm, start.y);
            var v2 = new Vector3(xMe, yMe, start.y);

            return Vector3.Lerp(v1, v2, t);
        }

        public static Vector3 BezierCubic(float t, Vector3 start, Vector3 middle, Vector3 end)
        {
            return Vector3.Lerp(Vector3.Lerp(start, middle, t), Vector3.Lerp(middle, end, t), t);
        }
    }
}