using System;

namespace HexMex.Game
{
    public interface IRenderable<T>
    {
        event Action<T> RequiresRedraw;
    }
}