using CocosSharp;
using HexMex.Helper;

namespace HexMex.Game.Settings
{
    public class ColorCollection
    {
        // Color table generated via: http://paletton.com/#uid=7370I0kllllaFw0g0qFqFg0w0aF

        private ColorCollectionFile ColorCollectionFile { get; set; }

        public CCColor4B BuildButtonBorder => ColorCollectionFile["buildButtonBorder"];
        public CCColor4B BuildButtonFill => ColorCollectionFile["buildButtonFill"];

        public CCColor4B HexButtonFill => ColorCollectionFile["hexButtonFill"];
        public CCColor4B HexButtonBorder => ColorCollectionFile["hexButtonBorder"];
        public CCColor3B HexButtonForeground => new CCColor3B(ColorCollectionFile["hexButtonForeground"]);

        public CCColor4B ConstructionFill => ColorCollectionFile["constructionFill"];
        public CCColor4B ConstructionBorder => ColorCollectionFile["constructionBorder"];

        public CCColor4B ResourceNone => ColorCollectionFile["resourceNone"];
        public CCColor4B ResourcePureWater => ColorCollectionFile["resourcePureWater"];
        public CCColor4B ResourceTree => ColorCollectionFile["resourceTree"];
        public CCColor4B ResourceStone => ColorCollectionFile["resourceStone"];
        public CCColor4B ResourceCoalOre => ColorCollectionFile["resourceCoalOre"];
        public CCColor4B ResourceCopperOre => ColorCollectionFile["resourceCopperOre"];
        public CCColor4B ResourceIronOre => ColorCollectionFile["resourceIronOre"];
        public CCColor4B ResourceGoldOre => ColorCollectionFile["resourceGoldOre"];
        public CCColor4B ResourceDiamondOre => ColorCollectionFile["resourceDiamondOre"];
        public CCColor4B ResourceGold => ColorCollectionFile["resourceGold"];
        public CCColor4B ResourceCopper => ColorCollectionFile["resourceCopper"];
        public CCColor4B ResourceIron => ColorCollectionFile["resourceIron"];
        public CCColor4B ResourceWood => ColorCollectionFile["resourceWood"];
        public CCColor4B ResourceCoal => ColorCollectionFile["resourceCoal"];
        public CCColor4B ResourceSand => ColorCollectionFile["resourceSand"];
        public CCColor4B ResourceBrick => ColorCollectionFile["resourceBrick"];
        public CCColor4B ResourcePaper => ColorCollectionFile["resourcePaper"];
        public CCColor4B ResourceCircuit => ColorCollectionFile["resourceCircuit"];
        public CCColor4B ResourceTools => ColorCollectionFile["resourceTools"];
        public CCColor4B ResourceBarrel => ColorCollectionFile["resourceBarrel"];
        public CCColor4B ResourcePottasche => ColorCollectionFile["resourcePottasche"];
        public CCColor4B ResourceGlas => ColorCollectionFile["resourceGlas"];
        public CCColor4B ResourceWaterBarrel => ColorCollectionFile["resourceWaterBarrel"];
        public CCColor4B ResourceKnowledge1 => ColorCollectionFile["resourceKnowledge1"];
        public CCColor4B ResourceKnowledge2 => ColorCollectionFile["resourceKnowledge2"];
        public CCColor4B ResourceKnowledge3 => ColorCollectionFile["resourceKnowledge3"];
        public CCColor4B ResourceEnergy => ColorCollectionFile["resourceEnergy"];
        public CCColor4B ResourceWater => ColorCollectionFile["resourceWater"];
        public CCColor4B ResourceDegradeable => ColorCollectionFile["resourceDegradeable"];
        public CCColor4B ResourceDiamond => ColorCollectionFile["resourceDiamond"];
        public CCColor4B ResourceAnything => ColorCollectionFile["resourceAnything"];

        public CCColor4B BuildMenuBackground => ColorCollectionFile["buildMenuBackground"];
        public CCColor4B BuildMenuBorder => ColorCollectionFile["buildMenuBorder"];
        public CCColor4B BuildMenuSelectedBackground => ColorCollectionFile["buildMenuSelectedBackground"];
        public CCColor4B BuildMenuSelectedBorder => ColorCollectionFile["buildMenuSelectedBorder"];
        public CCColor3B BuildMenuConstructButtonForeground => new CCColor3B(ColorCollectionFile["buildMenuConstructButtonForeground"]);
        public CCColor4B BuildMenuHeaderBackground => ColorCollectionFile["buildMenuHeaderBackground"];
        public CCColor4B BuildMenuHeaderBorder => ColorCollectionFile["buildMenuHeaderBorder"];
        public CCColor4B BuildMenuGridBackground => ColorCollectionFile["buildMenuGridBackground"];
        public CCColor4B BuildMenuGridBorder => ColorCollectionFile["buildMenuGridBorder"];
        public CCColor4B BuildMenuEntryPressedBackground => ColorCollectionFile["buildMenuEntryPressedBackground"];
        public CCColor4B BuildMenuEntryReleasedBackground => ColorCollectionFile["buildMenuEntryReleasedBackground"];
        public CCColor4B BuildMenuEntrySelectedBorder => ColorCollectionFile["buildMenuEntrySelectedBorder"];
        public CCColor4B BuildMenuEntryNotSelectedBorder => ColorCollectionFile["buildMenuEntryNotSelectedBorder"];

        public CCColor4F BuildButtonBackground => ColorCollectionFile["buildButtonBackground"].ToColor4F();
        public CCColor4F BuildButtonForeground => ColorCollectionFile["buildButtonForeground"].ToColor4F();

        public CCColor4F EdgeBackground => ColorCollectionFile["edgeBackground"].ToColor4F();

        public CCColor4B HexagonBorder => ColorCollectionFile["hexagonBorder"];

        public CCColor4B ResourcePackageBorder => ColorCollectionFile["resourcePackageBorder"];

        public CCColor4B StructureMenuResourceArrivedBorder => ColorCollectionFile["structureMenuResourceArrivedBorder"];
        public CCColor4B StructureMenuResourceNotArrivedBorder => ColorCollectionFile["structureMenuResourceNotArrivedBorder"];
        public CCColor4B StructureMenuProgressCircleFill => ColorCollectionFile["structureMenuProgressCircleFill"];
        public CCColor4B StructureMenuBackground => ColorCollectionFile["structureMenuBackground"];
        public CCColor4B StructureMenuBorder => ColorCollectionFile["structureMenuBorder"];
        public CCColor4B StructureMenuGridBackground => ColorCollectionFile["structureMenuGridBackground"];
        public CCColor4B StructureMenuGridBorder => ColorCollectionFile["structureMenuGridBorder"];
        public CCColor4B StructureMenuSuspendButtonPressed => ColorCollectionFile["structureMenuSuspendButtonPressed"];
        public CCColor4B StructureMenuSuspendButtonReleased => ColorCollectionFile["structureMenuSuspendButtonReleased"];
        public CCColor4B StructureMenuCancelButtonPressed => ColorCollectionFile["structureMenuCancelButtonPressed"];
        public CCColor4B StructureMenuCancelButtonReleased => ColorCollectionFile["structureMenuCancelButtonReleased"];
        public CCColor4B StructureMenuDeconstructButtonPressed => ColorCollectionFile["buildMenu"];
        public CCColor4B StructureMenuDeconstructButtonReleased => ColorCollectionFile["buildMenu"];

        public CCColor4B HexagonBlendColor => ColorCollectionFile["hexagonBlendColor"];
        public float InnerHexagonBlendIntensity => ColorCollectionFile.InnerHexagonBlendIntensity;
        public float OuterHexagonBlendIntensity => ColorCollectionFile.OuterHexagonBlendIntensity;
        public CCColor4B ResearchButtonBackground => ColorCollectionFile["researchButtonBackground"];
        public CCColor4B ResearchButtonBorder => ColorCollectionFile["researchButtonBorder"];
        public CCColor3B ResearchButtonLiquidColor => new CCColor3B(ColorCollectionFile["researchButtonLiquidColor"]);
        public CCColor3B Knowledge1 => new CCColor3B(ColorCollectionFile["knowledge1"]);
        public CCColor3B Knowledge2 => new CCColor3B(ColorCollectionFile["knowledge2"]);
        public CCColor3B Knowledge3 => new CCColor3B(ColorCollectionFile["knowledge3"]);
        public CCColor3B Energy => new CCColor3B(ColorCollectionFile["energy"]);
        public CCColor4B ResearchMenuBackground => ColorCollectionFile["researchMenuBackground"];
        public CCColor4B ResearchMenuBorder => ColorCollectionFile["researchMenuBorder"];
        public CCColor4B ResearchMenuEntryPressedBackground => ColorCollectionFile["researchMenuEntryPressedBackground"];
        public CCColor4B ResearchMenuEntryReleasedBackground => ColorCollectionFile["researchMenuEntryReleasedBackground"];
        public CCColor3B ResearchUnlockButtonIsPressedForeground => new CCColor3B(ColorCollectionFile["researchUnlockButtonIsPressedForeground"]);
        public CCColor3B ResearchUnlockButtonIsReleasedForeground => new CCColor3B(ColorCollectionFile["researchUnlockButtonIsReleasedForeground"]);
        public CCColor4B ResearchMenuEntrySelectedBorder => ColorCollectionFile["researchMenuEntrySelectedBorder"];
        public CCColor4B ResearchMenuEntryNotSelectedBorder => ColorCollectionFile["researchMenuEntryNotSelectedBorder"];
        public CCColor4B ResearchFooterBackground => ColorCollectionFile["researchFooterBackground"];
        public CCColor4B ResearchFooterBorder => ColorCollectionFile["researchFooterBorder"];
        public CCColor4B ResearchUnlockCostBackground => ColorCollectionFile["researchUnlockCostBackground"];
        public CCColor4B ResearchUnlockCostRectBackground => ColorCollectionFile["researchUnlockCostRectBackground"];
        public CCColor4B ResearchUnlockCostRectBorder => ColorCollectionFile["researchUnlockCostRectBorder"];
        public CCColor3B ResearchButtonDisabledForeground => new CCColor3B(ColorCollectionFile["researchButtonDisabledForeground"]);
        public CCColor4B SlowGameSpeedFill => ColorCollectionFile["slowGameSpeedFill"];
        public CCColor4B NormalGameSpeedFill => ColorCollectionFile["normalGameSpeedFill"];
        public CCColor4B FasterGameSpeedFill => ColorCollectionFile["fasterGameSpeedFill"];
        public CCColor4B MaximalGameSpeedFill => ColorCollectionFile["maximalGameSpeedFill"];
        public CCColor4B FastForewardBackground => ColorCollectionFile["fastForewardBackground"];
        public CCColor4B FastForewardBorder => ColorCollectionFile["fastForewardBorder"];
        public CCColor4B ResourcePackageBackground => ColorCollectionFile["resourcePackageBackground"];

        public CCColor4B GetInnerHexagonColor(ResourceType resourceType)
        {
            var color = resourceType.GetColor(this);
            var blendColor = HexagonBlendColor;
            var blendIntensity = InnerHexagonBlendIntensity;
            var result = CCColor4B.Lerp(color, blendColor, blendIntensity);
            return result;
        }

        public CCColor4B GetOuterHexagonColor(ResourceType resourceType)
        {
            var color = resourceType.GetColor(this);
            var blendColor = HexagonBlendColor;
            var blendIntensity = OuterHexagonBlendIntensity;
            var result = CCColor4B.Lerp(color, blendColor, blendIntensity);
            return result;
        }


        public ColorCollection(ColorCollectionFile colorCollectionFile)
        {
            ColorCollectionFile = colorCollectionFile;
        }

        public void ChangeColorCollectionFile(ColorCollectionFile colorCollectionFile)
        {
            ColorCollectionFile = colorCollectionFile;
        }
        public CCColor4B FromKey(string colorKey) => ColorCollectionFile[colorKey];
    }

    /*public class DarkerColors : ColorCollection
    {
        protected override CCColor4B[] GreenColorPalette { get; } =
        {
            new CCColor4B(0.035f, 0.541f, 0.067f, 1),
            new CCColor4B(0.224f, 0.702f, 0.255f, 1),
            new CCColor4B(0.086f, 0.655f, 0.125f, 1),
            new CCColor4B(0, 0.447f, 0.027f, 1),
            new CCColor4B(0, 0.329f, 0.024f, 1)
        };

        protected override CCColor4B[] BlueColorPalette { get; } =
        {
            new CCColor4B( 45, 20,120),
            new CCColor4B( 85, 62,156),
            new CCColor4B( 62, 34,145),
            new CCColor4B( 32, 10, 99),
            new CCColor4B( 22,  5, 74)
        };

        protected override CCColor4B[] YellowColorPalette { get; } =
        {
            new CCColor4B(175, 148, 11),
            new CCColor4B(227, 201, 72),
            new CCColor4B(212, 181, 28),
            new CCColor4B(145, 121, 0),
            new CCColor4B(107, 89, 0),
        };

        protected override CCColor4B[] RedColorPalette { get; } =
        {
            new CCColor4B(175, 15, 11),
            new CCColor4B(227, 76, 72),
            new CCColor4B(212, 33, 28),
            new CCColor4B(145,  4,  0),
            new CCColor4B(107,  3,  0),
        };
    }*/
}