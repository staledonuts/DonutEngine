using System.Numerics;

namespace Engine.Utils.Random
{
	/// <summary>
	///	Integer extensions.
	/// </summary>
	public static class NumericExtensions
	{
		#region Rotate Left

		/// <summary>
		///	Rotates the specified value left by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..31] is treated
		/// as congruent mod 32.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static byte RotateLeft(this byte value, byte shiftBit)
		{
			return (byte)((value << shiftBit) | (value >> (sizeof(byte) - shiftBit)));
		}

		/// <summary>
		///	Rotates the specified value left by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..31] is treated
		/// as congruent mod 32.
		/// </param>
		/// <returns>
		/// The rotated value.
		/// </returns>
		public static short RotateLeft(this short value, short shiftBit)
		{
			return (short)((value << shiftBit) | (value >> (sizeof(short) - shiftBit)));
		}

		/// <summary>
		///	Rotates the specified value left by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..31] is treated
		/// as congruent mod 32.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static ushort RotateLeft(this ushort value, ushort shiftBit)
		{
			return (ushort)((value << shiftBit) | (value >> (sizeof(ushort) - shiftBit)));
		}

		/// <summary>
		///	Rotates the specified value left by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..31] is treated
		/// as congruent mod 32.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static int RotateLeft(this int value, int shiftBit)
		{
			return (value << shiftBit) | (value >> (sizeof(int) - shiftBit));
		}

		/// <summary>
		///	Rotates the specified value left by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..31] is treated
		/// as congruent mod 32.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static uint RotateLeft(this uint value, int shiftBit)
		{
#if NET5_0_OR_GREATER
			return BitOperations.RotateLeft(value, shiftBit);
#else
			return (value << shiftBit) | (value >> (sizeof(uint) - shiftBit));
#endif
		}

		/// <summary>
		///	Rotates the specified value left by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		/// The number of bits to rotate by. Any value outside the range [0..31] is treated
		/// as congruent mod 32.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static long RotateLeft(this long value, int shiftBit)
		{
			return (value << shiftBit) | (value >> (sizeof(long) - shiftBit));
		}

		/// <summary>
		///	Rotates the specified value left by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		/// The number of bits to rotate by. Any value outside the range [0..31] is treated
		/// as congruent mod 32.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static ulong RotateLeft(this ulong value, int shiftBit)
		{
#if NET5_0_OR_GREATER
			return BitOperations.RotateLeft(value, shiftBit);
#else
			return (value << shiftBit) | (value >> (sizeof(ulong) - shiftBit));
#endif
		}

		#endregion Rotate Left

		#region Rotate Right

		/// <summary>
		///	Rotates the specified value right by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..63] is treated
		/// as congruent mod 64.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static byte RotateRight(this byte value, byte shiftBit)
		{
			return (byte)((value >> shiftBit) | (value << (sizeof(byte) - shiftBit)));
		}

		/// <summary>
		///	Rotates the specified value right by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..63] is treated
		///	as congruent mod 64.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static short RotateRight(this short value, short shiftBit)
		{
			return (short)((value >> shiftBit) | (value << (sizeof(short) - shiftBit)));
		}

		/// <summary>
		///	Rotates the specified value right by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..63] is treated
		///	as congruent mod 64.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static ushort RotateRight(this ushort value, ushort shiftBit)
		{
			return (ushort)((value >> shiftBit) | (value << (sizeof(ushort) - shiftBit)));
		}

		/// <summary>
		///	Rotates the specified value right by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..63] is treated
		///	as congruent mod 64.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static int RotateRight(this int value, int shiftBit)
		{
			return (value >> shiftBit) | (value << (sizeof(int) - shiftBit));
		}

		/// <summary>
		///	Rotates the specified value right by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..63] is treated
		///	as congruent mod 64.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static uint RotateRight(this uint value, int shiftBit)
		{
#if NET5_0_OR_GREATER
			return BitOperations.RotateRight(value, shiftBit);
#else
			return (value >> shiftBit) | (value << (sizeof(uint) - shiftBit));
#endif
		}

		/// <summary>
		///	Rotates the specified value right by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..63] is treated
		///	as congruent mod 64.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static long RotateRight(this long value, int shiftBit)
		{
			return (value >> shiftBit) | (value << (sizeof(long) - shiftBit));
		}

		/// <summary>
		///	Rotates the specified value right by the specified number of bits.
		/// </summary>
		/// <param name="value">
		///	The value to rotate.
		/// </param>
		/// <param name="shiftBit">
		///	The number of bits to rotate by. Any value outside the range [0..63] is treated
		///	as congruent mod 64.
		/// </param>
		/// <returns>
		///	The rotated value.
		/// </returns>
		public static ulong RotateRight(this ulong value, int shiftBit)
		{
#if NET5_0_OR_GREATER
			return BitOperations.RotateRight(value, shiftBit);
#else
			return (value >> shiftBit) | (value << (sizeof(ulong) - shiftBit));
#endif
		}

		#endregion Rotate Right
	}
}