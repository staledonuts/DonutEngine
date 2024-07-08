namespace Engine.Utils.Extensions;

public static class IntExt
{
	/// <summary>
	/// Clamps a number between two values
	/// </summary>
	public static int Clamp(int value, int min, int max)
	{
		return Math.Min(Math.Max(value, min), max);
	}
}