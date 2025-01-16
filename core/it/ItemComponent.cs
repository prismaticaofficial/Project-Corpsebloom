using System.IO;

namespace ProjectCorpsebloom.core.it
{
    internal class ItemComponent : GlobalItem
    {
        /// <summary>
        /// Whether or not this component is enabled on this item.
        /// </summary>
        public bool Enabled { get; set; }
        public override bool InstancePerEntity { get; } = true;

        public virtual void OnEnable(Item it) { }
        public virtual void OnDisable(Item it) { }

        #region COMPONENT FUNCTION OVERRIDES
        public virtual bool C_CanPickup(Item it, Player plr) => true;
        public virtual bool C_CanRClk(Item it) => false;
        public virtual bool C_OnPickup(Item it, Player plr) => true;

        /// <summary>
        /// Handles both saving and loading in one method, because I hate having separate methods.
        /// </summary>
        /// <param name="it">The item</param>
        /// <param name="t">The tag compound</param>
        /// <param name="save">Whether or not you're saving or loading data</param>
        public virtual void DataHandler(Item it, TagCompound t, bool save) { }
        public virtual void C_Load() { }
        public virtual void C_ModTooltips(Item it, List<TooltipLine> tt) { }
        public virtual void C_NetRecieve(Item it, BinaryReader bR) { }
        public virtual void C_NetSend(Item it, BinaryWriter bW) { }
        public virtual void C_OnCreate(Item it, ItemCreationContext c) { }
        /// <summary>
        /// Allows you to draw in front of the item in the inventory, even if PreDraw returns false.
        /// </summary>
        /// <param name="it">The item in question</param>
        /// <param name="sB"></param>
        /// <param name="pos">The position of the item in the inventory</param>
        /// <param name="fr">The item's frame</param>
        /// <param name="dC"></param>
        /// <param name="iC"></param>
        /// <param name="orig"></param>
        /// <param name="sc">The scale of the item</param>
        public virtual void C_PostDrawInInv(Item it, SpriteBatch sB, Vector2 pos, Rectangle fr, Color dC, Color iC, Vector2 orig, float sc) { }
        public virtual void C_PostDrawInWorld(Item it, SpriteBatch sB, Color lC, Color aC, float rot, float sc, int wAI) { }
        public virtual void C_RClk(Item it, Player plr) { }
        public virtual void C_SetDefaults(Item it) { }
        public virtual void C_Unload() { }
        public virtual void C_UpdateInv(Item it, Player plr) { }
        #endregion

        #region SEALED OVERRIDES
        public sealed override bool CanPickup(Item item, Player player)
        {
            if (Enabled)
                return C_CanPickup(item, player);

            else
                return base.CanPickup(item, player);
        }
        public sealed override bool CanRightClick(Item item)
        {
            if (Enabled)
                return C_CanRClk(item);

            else
                return base.CanRightClick(item);
        }
        public sealed override bool OnPickup(Item item, Player player)
        {
            if (Enabled)
                return C_OnPickup(item, player);

            else
                return base.OnPickup(item, player);
        }

        public sealed override void Load()
        {
            if (Enabled)
                C_Load();
        }
        public sealed override void LoadData(Item item, TagCompound tag)
        {
            if (Enabled)
                DataHandler(item, tag, false);
        }
        public sealed override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (Enabled)
                C_ModTooltips(item, tooltips);
        }
        public sealed override void NetReceive(Item item, BinaryReader reader)
        {
            if (Enabled)
                C_NetRecieve(item, reader);
        }
        public sealed override void NetSend(Item item, BinaryWriter writer)
        {
            if (Enabled)
                C_NetSend(item, writer);
        }
        public sealed override void OnCreated(Item item, ItemCreationContext context)
        {
            if (Enabled)
                C_OnCreate(item, context);

            base.OnCreated(item, context);
        }
        public sealed override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Enabled)
                C_PostDrawInInv(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public sealed override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (Enabled)
                C_PostDrawInWorld(item, spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }
        public sealed override void RightClick(Item item, Player player)
        {
            if (Enabled)
                C_RClk(item, player);
        }
        public sealed override void SaveData(Item item, TagCompound tag)
        {
            if (Enabled)
                DataHandler(item, tag, true);
        }
        public sealed override void SetDefaults(Item entity)
        {
            if (Enabled)
                C_SetDefaults(entity);
        }
        public sealed override void Unload()
        {
            if (Enabled)
                C_Unload();
        }
        public sealed override void UpdateInventory(Item item, Player player)
        {
            if (Enabled)
                C_UpdateInv(item, player);
        }
        #endregion
    }
}