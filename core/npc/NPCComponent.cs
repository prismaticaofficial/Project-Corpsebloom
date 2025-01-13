namespace ProjectCorpsebloom.core.npc
{
    internal class NPCComponent : GlobalNPC
    {
        public bool Enabled { get; internal set; }

        public sealed override bool InstancePerEntity { get; } = true;


    }
}