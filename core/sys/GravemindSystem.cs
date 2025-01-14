using ProjectCorpsebloom.core.def;
using ProjectCorpsebloom.core.ext;
using ProjectCorpsebloom.core.help;
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

        public Point originPoint;

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
            //select point on the map some odd 600-800 blocks away from spawn (whereabouts the usual evil would spawn)
            int x = Main.rand.Next(Main.maxTilesX - Main.spawnTileX - 800, Main.spawnTileX - 600);
            int y = 0;
            while (y < Main.worldSurface)
            {
                if (WorldGen.SolidTile(x, y))
                    break;

                y++;
            }

            //ENSURE the area is not a mountain

            //gen wide stone + evil tileID semi-circle, 66 wide, 22 deep at x/y origin
            //gen empty semi-circle 60 wide, 15 deep, offset x by 3 (how we sim the crater shape)

            //"cocoon" variable gen depending on what we choose here. just make a cobalt brick blob for a reference point for now (offset to be placed within the crater at center)

            //create the obelisk, either a bulbous mass or a Dead Space Marker style object

            //figure out a way to spawn cultists(?)

            Rectangle area = new(x, y + 20, 60, 40);
            originPoint = area.Center;

            activeMind.UpdatePoints(activeMind.isCrimson ? WorldHelper.CrimTileIDs : WorldHelper.CorrTileIDs);
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