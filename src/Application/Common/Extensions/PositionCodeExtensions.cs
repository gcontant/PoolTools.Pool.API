namespace Application.Common.Extensions;

public static class PositionCodeExtensions
{
    public static string NormalizePosition(this string positionCode)
    {
        if (positionCode.Contains('D'))
        {
            return "D";
        }

        return positionCode.Split(new char[] { ',', '/' })[0];
    }
}
