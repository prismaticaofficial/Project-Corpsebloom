using Terraria.WorldBuilding;

namespace ProjectCorpsebloom.core.help
{
    internal class WorldHelper
    {
        public static ushort[] CorrTileIDs = 
            [ TileID.CorruptGrass, TileID.CorruptHardenedSand, TileID.CorruptIce, TileID.CorruptJungleGrass, TileID.CorruptSandstone, TileID.Ebonsand, TileID.Ebonstone];

        public static ushort[] CrimTileIDs = 
            [ TileID.CrimsonGrass, TileID.CrimsonHardenedSand, TileID.FleshIce, TileID.CrimsonJungleGrass, TileID.CrimsonSandstone, TileID.Crimstone, TileID.Crimsand];

        public static Dictionary<ushort, int> TileCounter(ushort[] tileIDs, GenShape genShape, Point origin)
        {
            Dictionary<ushort, int> tileCounts = [];
            WorldUtils.Gen(origin, genShape, new Actions.TileScanner(tileIDs).Output(tileCounts));
            return tileCounts;
        }
    }
}