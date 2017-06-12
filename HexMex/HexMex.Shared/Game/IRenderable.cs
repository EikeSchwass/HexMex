using System;

namespace HexMex.Game
{
    public interface IRenderable<out T>
    {
        event Action<T> RequiresRedraw;
    }
}