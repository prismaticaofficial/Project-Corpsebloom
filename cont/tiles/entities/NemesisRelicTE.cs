using ProjectCorpsebloom.core.def;

namespace ProjectCorpsebloom.cont.tiles.entities
{
    internal class NemesisRelicTE : ModTileEntity
    {
        public NemesisData storedNemesis;

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile t = Main.tile[x, y];

            return true;
        }

        public override void SaveData(TagCompound tag) { tag[nameof(storedNemesis)] = storedNemesis; }
        public override void LoadData(TagCompound tag) { storedNemesis = tag.Get<NemesisData>(nameof(storedNemesis)); }
    }
}