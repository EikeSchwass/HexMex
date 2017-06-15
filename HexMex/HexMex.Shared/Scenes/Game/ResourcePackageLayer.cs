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
        private List<ResourcePackage> Packages { get; } = new List<ResourcePackage>();

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
            foreach (var package in Packages)
            {
                var radius = World.GameSettings.VisualSettings.ResourcePackageRadius;
                var posiition = package.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
                DrawNode.DrawCircle(posiition, radius, package.ResourceType.GetColor(World.GameSettings.VisualSettings.ColorCollection), 3, World.GameSettings.VisualSettings.ColorCollection.ResourcePackageBorder);
            }
        }

        private void ResourceManager_PackageStarted(ResourceManager packageManager, ResourcePackage package)
        {
            Packages.Add(package);
            package.RequiresRedraw += r => RedrawRequested = true;
            RedrawRequested = true;
        }

        private void ResourceManager_PackageArrived(ResourceManager packageManager, ResourcePackage package)
        {
            Packages.Remove(package);
            RedrawRequested = true;
        }
    }
}