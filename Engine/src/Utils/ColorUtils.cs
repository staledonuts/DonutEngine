using System;
using System.Numerics;
using Raylib_cs;

namespace Engine.Utils;
public static class ColorUtil
{
    public static Vector3 ColorToVector3(Color color)
    {
        Vector3 v = new()
        {
            X = color.R,
            Y = color.G,
            Z = color.B
        };
        return v;
    }

    public static Vector3 ColorToHSV(Color color)
    {
        Vector3 c = ColorToVector3(color);
        float v = Math.Max(c.X, Math.Max(c.Y, c.Z));
        float chroma = v - Math.Min(c.X, Math.Min(c.Y, c.Z));

        if (chroma == 0f)
        {
            return new Vector3(0, 0, v);
        }

        float s = chroma / v;

        if (c.X >= c.Y && c.Y >= c.Z)
        {
            float h = (c.Y - c.Z) / chroma;
            if (h < 0)
            {
                h += 6;
            }
            return new Vector3(h, s, v);
        }
        else if (c.Y >= c.Z && c.Y >= c.X)
        {
            return new Vector3((c.Z - c.X) / chroma + 2, s, v);
        }
        else
        {
            return new Vector3((c.X - c.Y) / chroma + 4, s, v);
        }
    }

    public static Color HSVToColor(Vector3 hsv)
    {
        return HSVToColor((int)hsv.X, (int)hsv.Y, (int)hsv.Z);
    }

    public static Color HSVToColor(int h, int s, int v)
    {
        if (h == 0 && s == 0)
        {
            return new Color(v, v, v, v);
        }

        int c = s * v;
        int x = c * (1 - Math.Abs(h % 2 - 1));
        int m = v - c;

        if (h < 1) 
        {
            return new Color(c + m, x + m, m, v);
        }
        else if (h < 2) 
        {
            return new Color(x + m, c + m, m, v);
        }
        else if (h < 3) 
        {
            return new Color(m, c + m, x + m, v);
        }
        else if (h < 4) 
        {
            return new Color(m, x + m, c + m, v);
        }
        else if (h < 5) 
        {
            return new Color(x + m, m, c + m, v);
        }
        else
        {
            return new Color(c + m, m, x + m, v);
        }
    }

    /// <summary>
	/// Given a string of two numbers separated by a space, get a 2d vector
	/// This method takes a string created from Vector2.StringFromVector() and does the reverse
	/// </summary>
	/// <param name="strVector">the vector string</param>
	/// <returns>a 2d vector with the values from the string!</returns>
	public static Color ToColor(this string color)
	{
		var myColor = Color.RayWhite;

		if (!string.IsNullOrEmpty(color))
		{
			//tokenize teh string
			string[] pathinfo = color.Split(new Char[] { ' ' });
			if (pathinfo.Length >= 1)
			{
				myColor.R = Convert.ToByte(pathinfo[0]);
			}
			if (pathinfo.Length >= 2)
			{
				myColor.G = Convert.ToByte(pathinfo[1]);
			}
			if (pathinfo.Length >= 3)
			{
				myColor.B = Convert.ToByte(pathinfo[2]);
			}
			if (pathinfo.Length >= 4)
			{
				myColor.A = Convert.ToByte(pathinfo[3]);
			}
		}

		return myColor;
	}

    public static Color ColorLerp(Color color1, Color color2, float amount)
    {
        return new
        (
            (byte)GameMath.Lerp(color1.R, color2.R, amount),
            (byte)GameMath.Lerp(color1.G, color2.G, amount),
            (byte)GameMath.Lerp(color1.B, color2.B, amount),
            (byte)GameMath.Lerp(color1.A, color2.A, amount)
        );
    }

	/// <summary>
	/// Extension method to simply convert between vector2 and string
	/// </summary>
	/// <returns>string created from the vector</returns>
	/// <param name="myVector">A vector to convert to string</param>
	public static string StringFromColor(this Color color)
	{
		return $"{color.R.ToString()} {color.G.ToString()} {color.B.ToString()} {color.A.ToString()}";
	}
}