using CocosSharp;

namespace HexMex.Scenes
{
    public class HexMexCamera : CCCamera
    {
        private CCPoint position;

        public HexMexCamera(CCSize targetVisibleDimensionsWorldspace) : base(CCCameraProjection.Projection2D, targetVisibleDimensionsWorldspace, new CCPoint3(0, 0, -1))
        {
        }

        public CCPoint Position
        {
            get => position;
            set
            {
                position = value;
                MoveToPosition(value);
            }
        }

        public void MoveToPosition(CCPoint value)
        {
            position = value;
            TargetInWorldspace = new CCPoint3(value, -1);
            CenterInWorldspace = new CCPoint3(value, 0);
        }
    }
}