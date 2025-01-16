namespace ProjectCorpsebloom.core.npc
{
    internal class NPCComponent : GlobalNPC
    {
        public bool Enabled { get; internal set; }
        public sealed override bool InstancePerEntity { get; } = true;

        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

        public virtual void C_AI(NPC npc) { }
        public virtual void C_FindFrame(NPC npc, int fH) { }
        public virtual void C_SetDefaults(NPC npc) { }

        public sealed override void AI(NPC npc)
        {
            if (Enabled)
                C_AI(npc);

            base.AI(npc);
        }
        public sealed override void FindFrame(NPC npc, int frameHeight)
        {
            if (Enabled)
                C_FindFrame(npc, frameHeight);

            base.FindFrame(npc, frameHeight);
        }
        public sealed override void SetDefaults(NPC entity)
        {
            if (Enabled)
                C_SetDefaults(entity);

            base.SetDefaults(entity);
        }
    }
}