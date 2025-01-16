using ProjectCorpsebloom.core.it;

namespace ProjectCorpsebloom.core.ext
{
    internal static class ItemExtensions
    {
        public static bool HasComponent<T>(this Item it) where T : ItemComponent => it.TryGetGlobalItem(out T temp) && temp.Enabled;

        public static void DisableAllComponents(this Item it)
        {
            foreach (GlobalItem git in it.Globals)
            {
                if (git is ItemComponent comp)
                {
                    comp.OnDisable(it);
                    comp.Enabled = false;
                }

            }
        }

        public static bool TryEnableComponent<T>(this Item it, Action<T> init = null) where T : ItemComponent
        {
            if (!it.TryGetGlobalItem(out T temp))
                return false;

            temp.Enabled = true;
            temp.OnEnable(it);
            init?.Invoke(temp);
            return true;
        }

        public static bool TryGetComponent<T>(this Item it, out T comp) where T : ItemComponent
        {
            if (!it.TryGetGlobalItem(out T temp))
            {
                comp = null;
                return false;
            }

            comp = temp;
            return true;
        }
    }
}