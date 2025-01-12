using ProjectCorpsebloom.cont.npcs;
using ProjectCorpsebloom.core.def;
using ProjectCorpsebloom.core.ext;

namespace ProjectCorpsebloom.core.plr.comps
{
    internal class NemesisComponent : PlayerComponent
    {
        public NemesisData currentNemesis;

        public int spawnTimer;

        public static bool CanSpawnInWorld() => Main.player.All(x => x.TryGetComponent(out NemesisComponent nC) && nC.currentNemesis.activeInWorld);

        public bool SpawnNemesis()
        {
            Vector2 spawnPoint = !Player.InLiquid() && Player.IsAlive() && Player.velocity.Y == 0 ? new((int)Player.position.X, (int)Player.position.Y) : new(0, 0);

            if (!Main.dedServ && spawnPoint == Vector2.Zero)
            {
                NPC.NewNPC(new EntitySource_Parent(Player), (int)spawnPoint.X, (int)spawnPoint.Y, ModContent.NPCType<Nemesis_Shell>());
                return true;
            }

            else return false;
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