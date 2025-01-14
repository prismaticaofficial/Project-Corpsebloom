using ProjectCorpsebloom.core.def;
using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.plr.comps;

namespace ProjectCorpsebloom.core.sys
{
    internal class GravemindSystem : ModSystem
    {
        public Gravemind activeMind;

        public bool customSeed;
        public bool timerStarted;

        public static double FullDayLength() => Main.dayLength + Main.nightLength;

        public static string GetTimeInHours() => Main.IsItDay() ? $"{Main.time / Main.dayLength * 24}" : $"{Main.time / Main.nightLength * 24}";

        public int worldSpawnTimer;

        public Vector2 originPoint;

        public override void PreUpdateWorld()
        {
            timerStarted = false;

            int temp = 11111111;

            if (NPC.downedBoss1 && !timerStarted && !activeMind.isActiveInWorld)
            {
                worldSpawnTimer = (int)FullDayLength() * Main.rand.Next(3, 6);
                timerStarted = true;
            }

            if (worldSpawnTimer > 0)
            {
                worldSpawnTimer--;
                if (worldSpawnTimer <= 0)
                {
                    activeMind = customSeed ? new Gravemind(temp) : new Gravemind();
                    activeMind.isActiveInWorld = true;
                    SpawnObelisk(activeMind.isCrimson);
                }
            }
        }

        public override void PostUpdateEverything()
        {
            if (activeMind.isActiveInWorld)
                activeMind.UpdateMind();

            base.PostUpdateEverything();
        }

        public void SpawnObelisk(bool isCrimson) 
        {
            ushort[] tileIDs = null; // = isCrimson ? WorldUtils.crimsonTiles : WorldUtils.corruptTiles;

            //select point on the map some odd 300-700 blocks away from spawn (whereabouts the usual evil would spawn)

            //create a crater, crater lined with stone and infected tiles

            //create the obelisk, either a bulbous mass or a Dead Space Marker style object

            activeMind.UpdatePoints(tileIDs); //account for newly spawned tiles
        }

        public void KillMind()
        {
            foreach (Player plr in Main.ActivePlayers)
            {
                if (plr.TryGetComponent(out NemesisComponent nm))
                {
                    nm.currentNemesis = null;
                    nm.Enabled = false;
                }
            }

            //start calcification spread here

            activeMind.isActiveInWorld = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag[nameof(activeMind)] = activeMind;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            activeMind = tag.Get<Gravemind>(nameof(activeMind));
        }
    }
}