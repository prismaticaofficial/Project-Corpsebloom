using ProjectCorpsebloom.core.help;

namespace ProjectCorpsebloom.cont.items.tools
{
    internal class Bioscanner : ModItem
    {
        public override string Texture => PathHelpers.PlaceholderAssetPath;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

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