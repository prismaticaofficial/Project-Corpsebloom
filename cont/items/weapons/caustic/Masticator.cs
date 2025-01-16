using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.help;
using ProjectCorpsebloom.core.it.comps;

namespace ProjectCorpsebloom.cont.items.weapons.caustic
{
    internal class Masticator : ModItem //Rotted Fork Caustic upgrade drop
    {
        public override string Texture => "Terraria/Images/NPC_0";

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.DamageType = DamageClass.Melee;

            Item.TryEnableComponent<TextureComponent>(x => x.path = PathHelpers.PlaceholderAssetPath);

            base.SetDefaults();
        }
    }
}