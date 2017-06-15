namespace HexMex.Game
{
    public struct RenderInformation
    {
        public string FillColorKey { get; }
        public string BorderColorKey { get; }

        public RenderInformation(string fillColorKey, string borderColorKey)
        {
            FillColorKey = fillColorKey;
            BorderColorKey = borderColorKey;
        }
    }
}