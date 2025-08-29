using System;

namespace SonJeremy.SModUtil.OxygenNotIncluded
{
    public static class SMath
    {
        public static float ToFloat(this double Number)
        {
            if (Number < 0) throw new ArgumentOutOfRangeException(nameof(Number));
            return (float)Number;
        }

        public static float GetPercent(this float Number, double PercentToGet)
        {
            if (PercentToGet >= 1) return Number;
            if (PercentToGet <= 0) return 0;

            return (float) PercentToGet * Number;
        }
    }
}