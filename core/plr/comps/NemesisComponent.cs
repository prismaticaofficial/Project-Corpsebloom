using ProjectCorpsebloom.cont.npcs;
using ProjectCorpsebloom.core.def;
using ProjectCorpsebloom.core.ext;

namespace ProjectCorpsebloom.core.plr.comps
{
    internal class NemesisComponent : PlayerComponent
    {
        public NemesisData currentNemesis;

        public bool attemptingSpawn = false;
        public int spawnTimer;

        public static bool CanSpawnInWorld() => Main.player.All(x => x.TryGetComponent(out NemesisComponent nC) && !nC.attemptingSpawn && !nC.currentNemesis.activeInWorld);

        public bool SpawnNemesis()
        {
            attemptingSpawn = true;

            Vector2 spawnPoint = Vector2.Zero;
            int checkTimer = 60;
            int attemptCount = 0;

            while (spawnPoint == Vector2.Zero)
            {
                checkTimer--;

                if (attemptCount >= 10)
                    break;

                if (checkTimer < 0)
                {
                    if (!Player.InLiquid() && Player.IsAlive() && Player.velocity.Y == 0)
                        spawnPoint = new((int)Player.position.X, (int)Player.position.Y);

                    else
                    {
                        checkTimer = 90;
                        attemptCount++;
                    }
                }
            }

            if (spawnPoint == Vector2.Zero && attemptCount >= 10)
                return attemptingSpawn = false;

            else
            {
                NPC.NewNPC(new EntitySource_Parent(Player), (int)spawnPoint.X, (int)spawnPoint.Y, ModContent.NPCType<Nemesis_Shell>());
                attemptingSpawn = false;
                return true;
            }
        }

        public override void C_SaveData(TagCompound t)
        {
            t[nameof(currentNemesis)] = currentNemesis;

            base.C_SaveData(t);
        }

        public override void C_LoadData(TagCompound t)
        {
            currentNemesis = t.Get<NemesisData>(nameof(currentNemesis));

            base.C_LoadData(t);
        }
    }
}