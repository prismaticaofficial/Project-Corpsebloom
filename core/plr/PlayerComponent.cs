using Terraria.GameInput;

namespace ProjectCorpsebloom.core.plr
{
    internal class PlayerComponent : ModPlayer
    {
        /// <summary>
        /// Whether or not this Component is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// For handling any initialization or startup of the Component not handled when enabling.
        /// </summary>
        public virtual void OnEnabled() { }
        /// <summary>
        /// For handling any extraneous data or values when this Component is disabled.
        /// </summary>
        public virtual void OnDisabled() { }

        public virtual void C_Init() { }
        public virtual void C_Load() { }
        public virtual void C_LoadData(TagCompound t) { }
        public virtual void C_ProcessInput(TriggersSet tS) { }
        public virtual void C_SaveData(TagCompound t) { }
        public virtual void C_Unload() { }
        public virtual void C_UpdateDead() { }

        public sealed override void Initialize()
        {
            if (Enabled)
                C_Init();
        }
        public sealed override void Load()
        {
            if (Enabled)
                C_Load();
        }
        public sealed override void LoadData(TagCompound tag)
        {
            if (Enabled)
                C_LoadData(tag);
        }
        public sealed override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Enabled)
                C_ProcessInput(triggersSet);
        }
        public sealed override void SaveData(TagCompound tag)
        {
            if (Enabled)
                C_SaveData(tag);
        }
        public sealed override void Unload()
        {
            if (Enabled)
                C_Unload();
        }
        public sealed override void UpdateDead()
        {
            if (Enabled)
                C_UpdateDead();
        }

    }
}