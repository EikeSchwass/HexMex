using System.Collections.Generic;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class ResourcePackageLayer : TouchLayer
    {
        public ResourcePackageLayer(World world, HexMexCamera camera) : base(camera)
        {
            World = world;
            world.ResourcePackageManager.PackageAdded += ResourcePackageManager_PackageAdded;
            world.ResourcePackageManager.PackageRemoved += ResourcePackageManager_PackageRemoved;
            AddChild(DrawNode);
            Schedule();
        }

        public World World { get; }
        private CCDrawNode DrawNode { get; } = new CCDrawNode();
        private List<ResourcePackage> Packages { get; } = new List<ResourcePackage>();

        private bool RedrawRequested { get; set; }

        private void ResourcePackageManager_PackageAdded(ResourcePackageManager packageManager, ResourcePackage package)
        {
            Packages.Add(package);
            package.RequiresRedraw += r => RedrawRequested = true;
            RedrawRequested = true;
        }

        private void ResourcePackageManager_PackageRemoved(ResourcePackageManager packageManager, ResourcePackage package)
        {
            Packages.Remove(package);
            RedrawRequested = true;
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
                var radius = World.WorldSettings.HexagonMargin / 2;
                var posiition = package.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
                DrawNode.DrawCircle(posiition, radius, package.ResourceType.GetColor(), 3, CCColor4B.Black);
            }
        }
    }
}