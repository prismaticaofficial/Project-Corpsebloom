namespace ProjectCorpsebloom.core.def
{
    internal class NemesisData : ModType, ILocalizedModType, TagSerializable
    {
        public Asset<Texture2D> nem_boss_head;
        public Asset<Texture2D> nem_head;
        public Asset<Texture2D> nem_torso;
        public Asset<Texture2D> nem_armLeft;
        public Asset<Texture2D> nem_armRight;

        public static readonly Func<TagCompound, NemesisData> DESERIALIZER = Load;

        public TagCompound SerializeData() =>
            new()
            { 
                [nameof(aggression)] = aggression,
                [nameof(personalityID)] = personalityID,
                [nameof(stolenItems)] = stolenItems,
            };

        public static NemesisData Load(TagCompound tag) => 
            new()
            {
                aggression = tag.GetInt(nameof(aggression)),
                personalityID = (PersonalityType)tag.GetInt(nameof(personalityID)),
                stolenItems = (List<Item>)tag.GetList<Item>(nameof(stolenItems)),
            };

        public bool activeInWorld;

        public int aggression;
        public PersonalityType personalityID;

        public List<Item> stolenItems;

        public string nem_name;

        public float speedSubsumeBonus;
        public float damageSubsumeBonus;
        public float healthSubsumeBonus;
        public float defenseSubsumeBonus;

        public string LocalizationCategory => "Nemesis";

        public void StealItems(Player plr)
        {
            for (int i = 0; i < 5; i++)
            {
                int ind = Main.rand.Next(0, 50);

                if (plr.inventory[ind].IsAir || plr.inventory[ind].IsCurrency)
                {
                    i--;
                    continue;
                }

                else
                {
                    if (plr.inventory[ind].maxStack == 1)
                    {
                        stolenItems.Add(plr.inventory[ind]);
                        plr.inventory[ind].TurnToAir();

                        if (Main.LocalPlayer == plr)
                            Main.NewText($"debug: {nem_name} has stolen {stolenItems.Last().Name}", Color.Gray);
                    }

                    else if (plr.inventory[ind].maxStack > 1)
                    {
                        int stackRand = Main.rand.Next(5, plr.inventory[ind].stack);
                        var inList = stolenItems.Find(x => x == plr.inventory[ind]);

                        if (inList is null)
                        {
                            stolenItems.Add(plr.inventory[ind]);
                            inList.stack = stackRand;
                        }
                        else
                            inList.stack += stackRand;

                        plr.inventory[ind].stack -= stackRand;
                        if (Main.LocalPlayer == plr)
                            Main.NewText($"debug: {nem_name} has stolen {stolenItems.Last().Name} ({stackRand})", Color.Gray);
                    }
                }
            }
        }

        public void UpdateSubsumeBonuses()
        {
            foreach (var it in stolenItems)
            {
                //if id is weapons...
                //add to attack bonus

                //if id is speed accessories...
                //add to speed bonus

                //if id is armor or defense accessories...
                //add to defense bonus

                //if id is health accessories...
                //add to health bonus
            }
        }

        protected override void Register()
        {
            ModTypeLookup<NemesisData>.Register(this);

            throw new NotImplementedException();
        }
    }

    public enum PersonalityType
    {
        Gluttonous,
        Greedy,
        Prideful,
        Sloth,
        Wrathful,
    }
}