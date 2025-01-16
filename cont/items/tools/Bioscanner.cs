using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.help;
using ProjectCorpsebloom.core.it.comps;

namespace ProjectCorpsebloom.cont.items.tools
{
    internal class Bioscanner : ModItem
    {
        public override string Texture => "Terraria/Images/NPC_0";

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.TryEnableComponent<TextureComponent>(t => t.path = PathHelpers.PlaceholderAssetPath);

            base.SetDefaults();
        }

        public override bool? UseItem(Player player)
        {
            //scan for corr tiles
            //scan for crim tiles
            //concat both

            //display totals to this player only

            //maybe add a rightclick switch for crim/corr, or make it general "infected" tiles?

            return base.UseItem(player);
        }
    }
}