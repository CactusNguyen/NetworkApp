namespace Utilities
{
    public static class Constant
    {
    
    }

    public static class Layers
    {
        public const int Player = 1 << 3;
        public const int Item = 1 << 6;
        public const int Enemy = 1 << 8;
        public const int Hittable = 1 << 8 | 1 << 3;
    }
}
