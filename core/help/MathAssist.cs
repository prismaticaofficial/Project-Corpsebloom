namespace ProjectCorpsebloom.core.help
{
    internal class MathAssist
    {
        public static Vector2 CalcRelicBob(Point p)
        {
            Vector2 offScreen = Main.drawToScreen ? Vector2.Zero : new(Main.offScreenRange);
            Vector2 wPos = p.ToWorldCoordinates(24f, 64f);
            float offSet = (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) / 5f);

            return wPos + offScreen - Main.screenPosition + new Vector2(0f, -40f) + new Vector2(0f, offSet * 4f);
        }
    }
}