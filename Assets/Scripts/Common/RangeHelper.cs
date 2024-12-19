namespace Scripts.Common
{
    public static class RangeHelper
    {
        public static bool IsMinRangeCorrect(int minValue, int value) => value > minValue;
        public static bool IsMinRangeCorrect(float minValue, float value) => value > minValue;

        public static bool IsMaxRangeCorrect(int maxValue, int value) => value < maxValue;
        public static bool IsMaxRangeCorrect(float maxValue, float value) => value < maxValue;
    }
}