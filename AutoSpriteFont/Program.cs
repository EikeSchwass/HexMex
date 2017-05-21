using System;
using System.IO;

namespace AutoSpriteFont
{
    class Program
    {
        private static readonly string FirstHalf = "<?xml version=\"1.0\" encoding=\"utf-8\"?><XnaContent xmlns:Graphics=\"Microsoft.Xna.Framework.Content.Pipeline.Graphics\"><Asset Type=\"Graphics:FontDescription\"><FontName>Arial</FontName><Size>";
        private static readonly string SecondHalf = "</Size><Spacing>0</Spacing><UseKerning>true</UseKerning><Style>Regular</Style><CharacterRegions><CharacterRegion><Start>&#32;</Start><End>&#126;</End></CharacterRegion></CharacterRegions></Asset></XnaContent>";

        public static void Main()
        {
            string mgcb = "" + Environment.NewLine + "#----------------------------- Global Properties ----------------------------#" + Environment.NewLine + Environment.NewLine + @"/outputDir:..\..\HexMex\HexMex.Droid\Assets\Content\fonts" + Environment.NewLine + "/intermediateDir:obj" + Environment.NewLine + "/platform:Android" + Environment.NewLine + "/config:" + Environment.NewLine + "/profile:Reach" + Environment.NewLine + "/compress:False" + Environment.NewLine + Environment.NewLine + "#-------------------------------- References --------------------------------#" + Environment.NewLine + Environment.NewLine + "#---------------------------------- Content ---------------------------------#";


            const string path = @"G:\Dokumente\Visual Studio\Projects\HexMex\Data\MonoGamePipeline\test";
            for (int i = 5; i < 40; i++)
            {
                using (var fileStream = File.Create(Path.Combine(path, $"arial-{i}.spritefont")))
                {
                    using (var sw = new StreamWriter(fileStream))
                    {
                        sw.Write(FirstHalf);
                        sw.Write(i);
                        sw.Write(SecondHalf);
                    }
                }
                mgcb += $"#begin arial-{i}.spritefont {Environment.NewLine}/importer:FontDescriptionImporter{Environment.NewLine}/processor:FontDescriptionProcessor{Environment.NewLine}/processorParam:PremultiplyAlpha=True{Environment.NewLine}/processorParam:TextureFormat=Compressed{Environment.NewLine}/build:arial-{i}.spritefont{Environment.NewLine}";
            }
            Console.WriteLine(mgcb);
        }
    }
}