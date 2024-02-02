namespace Domain.Enums;

public enum SallaryGrade
{
    Unknown = 0,

    /// <summary>
    /// Up to 400 $
    /// </summary>
    Low = 1,

    /// <summary>
    /// Up to 2000 $
    /// </summary>
    Middle = 2,

    /// <summary>
    ///  Up to 5000 $
    /// </summary>
    High = 3,

    /// <summary>
    /// More then 5000
    /// </summary>
    VeryHigh = 4
}
