[System.Flags]
public enum ColorClass
{
    Blank = 0,
    Red = 1,
    Yellow = 2,
    Blue = 4,

    Orange = Red | Yellow,
    Green = Blue | Yellow,
    Purple = Red | Blue,
    Black = Red | Yellow | Blue,
    White = 20
}