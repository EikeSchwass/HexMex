using System.Collections.Generic;
using System.Collections.ObjectModel;
using CocosSharp;

namespace HexMex.Controls
{
    public abstract class Container : CCNode, IPointInBoundsCheck
    {
        private readonly List<IVisual> elements = new List<IVisual>();

        public ReadOnlyCollection<IVisual> Elements => elements.AsReadOnly();

        public void AddElement(IVisual element)
        {
            elements.Add(element);
            AddChild(element.Node);
            OnRepositionRequired(Elements);
        }

        public abstract bool IsPointInBounds(CCTouch touch);

        public void RemoveElement(IVisual element)
        {
            elements.Remove(element);
            RemoveChild(element.Node);
            OnRepositionRequired(Elements);
        }

        protected abstract void OnRepositionRequired(ReadOnlyCollection<IVisual> visuals);
    }

    public interface IVisual : IPointInBoundsCheck
    {
        CCNode Node { get; }
        CCSize Size { get; }
        void OnTouchDown(CCTouch touch);
        void OnTouchUp(CCTouch touch);
        void OnTouchCancelled(CCTouch touch);
    }
}