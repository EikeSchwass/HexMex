using CocosSharp;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class StructureMenuResource
    {
        public ResourceTypeSource ResourceTypeSource { get; }
        public CCSprite Sprite { get; }
        public bool IsAvailable { get; set; }

        public StructureMenuResource(ResourceTypeSource resourceTypeSource, CCSprite sprite)
        {
            ResourceTypeSource = resourceTypeSource;
            Sprite = sprite;
        }
    }
}