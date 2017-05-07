using CocosSharp;

namespace HexMex.Scenes
{
    public class HexMexCamera : CCCamera
    {
        public const float MinZoomFactor = 0.25f;
        public const float MaxZoomFactor = 1.5f;

        public HexMexCamera(CCSize targetVisibleDimensionsWorldspace) : base(CCCameraProjection.Projection2D, targetVisibleDimensionsWorldspace, new CCPoint3(0, 0, -1))
        {
            StartVisibleArea = targetVisibleDimensionsWorldspace;
            MoveToPosition(CCPoint.Zero);
        }

        public CCPoint Position { get; private set; }
        public CCSize StartVisibleArea { get; }
        public float ZoomFactor { get; private set; } = 1;

        public void MoveToPosition(CCPoint value)
        {
            Position = value;
            TargetInWorldspace = new CCPoint3(value, -1);
            CenterInWorldspace = new CCPoint3(value, 0);
        }

        public void SetZoomFactor(float zoomFactor)
        {
            zoomFactor = CCMathHelper.Clamp(zoomFactor, MinZoomFactor, MaxZoomFactor);
            ZoomFactor = zoomFactor;
            var currentWidth = StartVisibleArea.Width / zoomFactor;
            var currentHeight = StartVisibleArea.Height / zoomFactor;
            OrthographicViewSizeWorldspace = new CCSize(currentWidth, currentHeight);
        }
    }
}