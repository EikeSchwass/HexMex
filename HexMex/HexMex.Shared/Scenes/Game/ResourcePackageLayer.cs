using System.Collections.Generic;
using CocosSharp;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class ResourcePackageLayer : CCLayer
    {
        public ResourcePackageLayer(ResourcePackageManager resourcePackageManager, WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
            resourcePackageManager.PackageAdded += ResourcePackageManager_PackageAdded;
            resourcePackageManager.PackageRemoved += ResourcePackageManager_PackageRemoved;
        }

        public WorldSettings WorldSettings { get; }
        private Dictionary<ResourcePackage, CCDrawNode> Packages { get; } = new Dictionary<ResourcePackage, CCDrawNode>();

        private void DrawPackage(ResourcePackage package)
        {
            var drawNode = GetDrawNode(package);
            drawNode.Position = package.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
        }

        private CCDrawNode GetDrawNode(ResourcePackage package)
        {
            if (!Packages.ContainsKey(package))
            {
                var node = new CCDrawNode();
                node.DrawDot(CCPoint.Zero, WorldSettings.HexagonMargin, CCColor4B.Green);
                Packages.Add(package, node);
            }
            var drawNode = Packages[package];
            return drawNode;
        }

        private void ResourcePackageManager_PackageAdded(ResourcePackageManager packageManager, ResourcePackage package)
        {
            package.RequiresRedraw += DrawPackage;
            var drawNode = GetDrawNode(package);
            drawNode.Position = package.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            AddChild(drawNode);
            DrawPackage(package);
        }

        private void ResourcePackageManager_PackageRemoved(ResourcePackageManager packageManager, ResourcePackage package)
        {
            package.RequiresRedraw -= DrawPackage;
            RemoveChild(Packages[package]);
            Packages.Remove(package);
        }
    }
}