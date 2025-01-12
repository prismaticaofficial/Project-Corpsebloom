using ProjectCorpsebloom.core.def;
using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.plr.comps;

namespace ProjectCorpsebloom.cont.npcs
{
    [AutoloadBossHead]
    internal class Nemesis_Shell : ModNPC
    {
        private int despawnTimer;

        internal bool targetDied;
        internal bool failedSequence;
        internal bool succeededSequence;

        public override LocalizedText DisplayName => base.DisplayName;

        public override string BossHeadTexture => base.BossHeadTexture;

        public Player GetPlayerTarget()
        {
            var plr = Main.player[NPC.target];
            return plr.dead || !plr.active ? null : plr;
        }

        public NemesisData GetData() => Main.player[NPC.target].TryGetComponent(out NemesisComponent nem) ? nem.currentNemesis : null;

        public override void SetDefaults()
        {
            NPC.boss = true;

            NPC.height = 30;
            NPC.width = 20;



            base.SetDefaults();
        }

        public override bool CheckDead()
        {
            if (targetDied)
            {
                GetData().aggression = Math.Clamp(--GetData().aggression, 1, 5);
                GetData().StealItems(Main.player[NPC.target]);
            }

            if (failedSequence)
            {
                GetData().aggression = Math.Clamp(++GetData().aggression, 1, 5);
            }

            if (succeededSequence)
            {
                return true;
            }

            GetData().activeInWorld = false;

            return false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_Parent p && p.Entity is Player plr)
                NPC.target = plr.whoAmI;

            GetData().activeInWorld = true;
            despawnTimer = 300;

            GetData().UpdateSubsumeBonuses();
            //announce presence

            //second spawn routine

            base.OnSpawn(source);
        }

        public override void AI()
        {
            if (GetPlayerTarget() is null)
            {
                despawnTimer--;
                Dust.NewDust(NPC.position, Main.rand.Next(2, 5), Main.rand.Next(2, 5), DustID.Smoke, 0.5f, 1f, 145, Color.Black);
                NPC.alpha = Math.Clamp(++NPC.alpha, 0, 255);
                if (despawnTimer <= 0)
                {
                    targetDied = GetPlayerTarget() is null;
                    failedSequence = false;
                    NPC.life = 0;
                }
            }



            //attack 1

            //attack 2

            //attack 3

            //attack 4

            //qte AI pause

            base.AI();
        }
    }
}