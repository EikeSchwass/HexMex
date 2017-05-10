using CocosSharp;
using static System.Math;

namespace HexMex.Helper
{
    public static class MathHelper
    {
        public static CCPoint RotateAround(this CCPoint point, CCPoint centerOfRotation, float angleInDegrees)
        {
            double angle = angleInDegrees / 360 * (2 * PI);
            var sin = Sin(angle);
            var cos = Cos(angle);
            float xOld = point.X - centerOfRotation.X;
            float yOld = point.Y - centerOfRotation.Y;
            float xNew = (float)(xOld * cos - yOld * sin);
            float yNew = (float)(yOld * cos + xOld * sin);
            xNew += centerOfRotation.X;
            yNew += centerOfRotation.Y;
            return new CCPoint(xNew, yNew);
        }

        public static CCPoint[] RotateAround(this CCPoint[] points, CCPoint centerOfRotation, float angleInDegrees)
        {
            var result = new CCPoint[points.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = points[i].RotateAround(centerOfRotation, angleInDegrees);
            }
            return result;
        }
    }
}
