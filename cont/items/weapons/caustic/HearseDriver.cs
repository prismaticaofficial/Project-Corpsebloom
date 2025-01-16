using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.help;
using ProjectCorpsebloom.core.it.comps;

namespace ProjectCorpsebloom.cont.items.weapons.caustic
{
    internal class HearseDriver : ModItem //Undertaker Caustic upgrade drop weapon
    {
        public override string Texture => "Terraria/Images/NPC_0";

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.DamageType = DamageClass.Ranged;

            Item.TryEnableComponent<TextureComponent>(x => x.path = PathHelpers.PlaceholderAssetPath);

            base.SetDefaults();
        }

        public override void OnCreated(ItemCreationContext context)
        {


            base.OnCreated(context);
        }
    }
}