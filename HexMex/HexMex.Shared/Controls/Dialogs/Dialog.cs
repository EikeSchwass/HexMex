using CocosSharp;
using HexMex.Scenes.Game;

namespace HexMex.Controls.Dialogs
{
    public class Dialog : CCLayer
    {
        public DialogContent Content { get; }
        public CCSize Size { get; private set; }
        public string Title { get; set; }

        private CCLabel TitleLabel { get; set; }

        public Dialog(string title, DialogContent content)
        {
            Title = title;
            Content = content;
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            Size = VisibleBoundsWorldspace.Size * 0.8f;

            var titleSize = new CCSize(Size.Width, Size.Height / 10);
            TitleLabel = new CCLabel(Title, Font.DialogTitleFont.FontPath, titleSize) {Position = titleSize.Center};
            AddChild(TitleLabel);
            AddChild(Content);
            Content.Position = Size.Center + new CCPoint(0, titleSize.Height);
        }
    }

    public class DialogContent : CCNode
    {
    }
}