using ProjectCorpsebloom.cont.tiles.functional;
using ProjectCorpsebloom.core.def;
using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.help;
using ProjectCorpsebloom.core.it.comps;

namespace ProjectCorpsebloom.cont.items.tiles.functional
{
    internal class NemesisRelicItem : ModItem
    {
        internal NemesisData nem_dat;

        public override string Texture => "Terraria/Images/NPC_0";

        public override void SetDefaults()
        {
            Item.height = Item.width = 20;

            Item.createTile = ModContent.TileType<NemesisRelic>();
            Item.useStyle = ItemUseStyleID.Swing;

            Item.TryEnableComponent<TextureComponent>(x => x.path = PathHelpers.PlaceholderAssetPath);
        }

        public override void SaveData(TagCompound tag) { tag[nameof(nem_dat)] = nem_dat; }
        public override void LoadData(TagCompound tag) { nem_dat = tag.Get<NemesisData>(nameof(nem_dat)); }
    }
} 