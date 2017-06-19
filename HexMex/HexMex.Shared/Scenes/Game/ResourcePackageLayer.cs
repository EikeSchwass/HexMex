using System.Collections.Generic;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class ResourcePackageLayer : TouchLayer
    {
        public World World { get; }
        private CCDrawNode DrawNode { get; } = new CCDrawNode();
        private Dictionary<ResourcePackage, CCSprite> Packages { get; } = new Dictionary<ResourcePackage, CCSprite>();

        private bool RedrawRequested { get; set; }

        public ResourcePackageLayer(World world, HexMexCamera camera) : base(camera)
        {
            World = world;
            world.ResourceManager.PackageStarted += ResourceManager_PackageStarted;
            world.ResourceManager.PackageArrived += ResourceManager_PackageArrived;
            AddChild(DrawNode);
            Schedule();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!RedrawRequested)
                return;
            RedrawRequested = false;
            Render();
        }

        private void Render()
        {
            DrawNode.Clear();
            foreach (var kvp in Packages)
            {
                var package = kvp.Key;
                var radius = World.GameSettings.VisualSettings.ResourcePackageRadius;
                var position = package.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
                //DrawNode.DrawCircle(position, radius, World.GameSettings.VisualSettings.ColorCollection.ResourcePackageBackground, 3, World.GameSettings.VisualSettings.ColorCollection.ResourcePackageBorder);
                kvp.Value.Position = position;
                kvp.Value.ContentSize = new CCSize(radius * 2, radius * 2) * 1f;
            }
        }

        private void ResourceManager_PackageStarted(ResourceManager packageManager, ResourcePackage package)
        {
            var spriteFrame = package.ResourceType.GetSpriteFrame();
            CCSprite sprite = new CCSprite(spriteFrame) { BlendFunc = CCBlendFunc.NonPremultiplied };
            Packages.Add(package, sprite);
            package.RequiresRedraw += r => RedrawRequested = true;
            RedrawRequested = true;
            AddChild(sprite);
        }

        private void ResourceManager_PackageArrived(ResourceManager packageManager, ResourcePackage package)
        {
            RemoveChild(Packages[package]);
            Packages.Remove(package);
            RedrawRequested = true;
        }
    }
}