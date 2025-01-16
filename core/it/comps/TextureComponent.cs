namespace ProjectCorpsebloom.core.it.comps
{
    internal class TextureComponent : ItemComponent
    {
        public string path = string.Empty;

        private KeyValuePair<int, Asset<Texture2D>> storedTexture;

        public override void C_SetDefaults(Item it)
        {
            if (path != string.Empty)
            {
                storedTexture = new(it.netID, TextureAssets.Item[it.netID]);

                if (ModContent.HasAsset(path))
                    TextureAssets.Item[it.netID] = ModContent.Request<Texture2D>(path);

                else
                    Mod.Logger.Warn("[>prismBox] | texture asset at \"" + path + "\" could not be found. Please ensure the path is correct.");
            }

            base.C_SetDefaults(it);
        }

        public override void C_Unload()
        {
            TextureAssets.Item[storedTexture.Key] = storedTexture.Value;

            base.C_Unload();
        }
    }
}