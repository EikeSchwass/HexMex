using System;
using CocosSharp;

namespace HexMex.Game
{
    public abstract class Resource
    {
        public CCPoint WorldPosition { get; protected set; }
        public HexagonCornerPosition Position { get; protected set; }
        public virtual string ImageFilename { get; protected set; }

        public void MoveTo(Structure requestingStructure)
        {

        }

        public virtual bool CanBeUsedFor(Type type)
        {
            return type == GetType();
        }
    }
}