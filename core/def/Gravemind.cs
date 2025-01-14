using ProjectCorpsebloom.core.help;
using Terraria.WorldBuilding;

namespace ProjectCorpsebloom.core.def
{
    internal class Gravemind : ModType, ILocalizedModType, TagSerializable
    {
        public bool isCrimson;
        public bool isActiveInWorld;

        public GrowthStage mindStage;

        public int totalPoints;

        public int killPoints;
        public int startingPoints;
        public int tilePoints;

        public int enemyAggression;
        public int spreadAggression;
        public int invasionFrequency;

        public int strainSeed;

        public Vector2 mindPosition;

        public string LocalizationCategory => "Gravemind";
        protected virtual LocalizedText GravemindTelepathyDialogue => this.GetLocalization(nameof(GravemindTelepathyDialogue), () => "");

        public Gravemind()
        {
            int temp = strainSeed = Main.rand.Next(11111111, 100000000);
            ReadSeed(temp);

            tilePoints = 0;
            killPoints = 0;
            totalPoints = 0;

            mindPosition = Vector2.Zero;

            UpdatePoints(isCrimson ? WorldHelper.CrimTileIDs : WorldHelper.CorrTileIDs);
        }

        public Gravemind(int strainSeed)
        {
            this.strainSeed = strainSeed;
            ReadSeed(strainSeed);

            tilePoints = 0;
            killPoints = 0;
            totalPoints = 0;

            mindPosition = Vector2.Zero;

            UpdatePoints(isCrimson ? WorldHelper.CrimTileIDs : WorldHelper.CorrTileIDs);
        }

        public static readonly Func<TagCompound, Gravemind> DESERIALIZER = Load;

        public TagCompound SerializeData() =>
            new()
            {
                [nameof(mindPosition)] = mindPosition,
                [nameof(killPoints)] = killPoints,
                [nameof(strainSeed)] = strainSeed,
            };

        public static Gravemind Load(TagCompound tag)
        {
            var n = new Gravemind()
            {
                mindPosition = tag.Get<Vector2>(nameof(mindPosition)),
                killPoints = tag.GetInt(nameof(killPoints)),
                strainSeed = tag.GetInt(nameof(strainSeed)),
            };

            var temp = n.strainSeed;

            n.ReadSeed(temp);
            n.UpdatePoints(n.isCrimson ? WorldHelper.CrimTileIDs : WorldHelper.CorrTileIDs);

            return n;
        }

        internal void ReadSeed(int seed)
        {
            int[] ints = new int[5];
            for (int i = 0; i < 8; i++)
            {
                if (i >= 4)
                    ints[4] += seed % 10;
                else
                    ints[i] = seed % 10 > 0 ? seed % 10 : 1;

                seed /= 10;
            }
            isCrimson = ints[0] > 4;
            enemyAggression = ints[1];
            spreadAggression = ints[2];
            invasionFrequency = ints[3];
            startingPoints = ints[4] * 10;
        }

        public void UpdatePoints(ushort[] tileIDs)
        {
            Dictionary<ushort, int> tileCounts = WorldHelper.TileCounter(tileIDs, new Shapes.Rectangle(Main.maxTilesX, Main.maxTilesY), new(0, 0));

            tilePoints = 0;
            foreach (ushort tileID in tileIDs)
                tilePoints += tileCounts[tileID];

            totalPoints = startingPoints + tilePoints + killPoints;
        }

        public void UpdateMind()
        {
            
        }

        protected override void Register()
        {
            ModTypeLookup<Gravemind>.Register(this);
        }
    }

    public enum GrowthStage
    {
        Seed,
        Budding,
        Blooming,
        Floweing,
        Crowned,

        Wilting,
        Rotting
    }
}