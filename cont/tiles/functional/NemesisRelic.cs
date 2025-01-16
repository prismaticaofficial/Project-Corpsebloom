using ProjectCorpsebloom.cont.items.tiles.functional;
using ProjectCorpsebloom.cont.tiles.entities;
using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.help;
using Terraria.GameContent.ObjectInteractions;

namespace ProjectCorpsebloom.cont.tiles.functional
{
    internal class NemesisRelic : ModTile
    {
        public Asset<Texture2D> nem_tex;

        public override string Texture => PathHelpers.PlaceholderAssetPath;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            NemesisRelicTE te = ModContent.GetInstance<NemesisRelicTE>();
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(te.Hook_AfterPlacement, -1, 0, false);
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override bool RightClick(int i, int j)
        {
            NemesisRelicTE te = Main.tile[i, j].GetModTileEntity<NemesisRelicTE>(i, j);

            Main.NewText($"[test]: \nNAME: {te.storedNemesis.nem_name} \nPERSONALITY: {te.storedNemesis.personalityID}");

            return true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int ind = Item.NewItem(new EntitySource_TileBreak(i, j), i, j, 20, 20, ModContent.ItemType<NemesisRelicItem>());

            if (Main.item[ind].ModItem is NemesisRelicItem it) //transmit back to the item
                it.nem_dat = Main.tile[i, j].GetModTileEntity<NemesisRelicTE>(i, j).storedNemesis;

            ModContent.GetInstance<NemesisRelicTE>().Kill(i, j);

            base.KillMultiTile(i, j, frameX, frameY);
        }

        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Point p = new(i, j);
            Tile t = Main.tile[p.X, p.Y];
            if (t == null || !t.HasTile)
                return;

            Rectangle frame = nem_tex.Value.Frame(1, 1, 0, t.TileFrameX / t.FrameWidth());

            spriteBatch.Draw(nem_tex.Value, MathAssist.CalcRelicBob(p), frame, Lighting.GetColor(p.X, p.Y), 0f, frame.Size() / 2f, 1f, SpriteEffects.None, 0f);

            base.SpecialDraw(i, j, spriteBatch);
        }
    }
}