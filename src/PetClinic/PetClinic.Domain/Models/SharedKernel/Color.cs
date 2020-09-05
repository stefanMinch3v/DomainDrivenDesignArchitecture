namespace PetClinic.Domain.Models.SharedKernel
{
    using Common;

    public class Color : Enumeration
    {
        public static readonly Color Red = new Color(1, nameof(Red));
        public static readonly Color Black = new Color(2, nameof(Black));
        public static readonly Color Gray = new Color(3, nameof(Gray));
        public static readonly Color Yellow = new Color(4, nameof(Yellow));
        public static readonly Color Orange = new Color(5, nameof(Orange));
        public static readonly Color White = new Color(6, nameof(White));

        private Color(int value)
            : base(value)
        {
        }

        private Color(int value, string name)
            : base(value, name)
        {
        }
    }
}
