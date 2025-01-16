namespace ProjectCorpsebloom.core.ext
{
    internal static class TileExtensions
    {
        public static int FrameWidth(this Tile t) => TileObjectData.GetTileData(t).Width * 18;
        public static int FrameHeight(this Tile t) => TileObjectData.GetTileData(t).Height * 18;

        public static T GetModTileEntity<T>(this Tile t, int i, int j) where T : ModTileEntity
        {
            int ind = ModContent.GetInstance<T>().Find(i - t.TileFrameX % 36 / 18, j - t.TileFrameY / 18);
            return (T)TileEntity.ByID[ind];
        }
    }
}