namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class IntVector2 {

		public int x;

		public int y;

		public static readonly IntVector2 one = new IntVector2(1, 1);

		public static readonly IntVector2 zero = new IntVector2(0, 0);
		
		public IntVector2(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

		public IntVector2(Vector2 vector)
		{
			x = (int)vector.x;
			y = (int)vector.y;
		}

		public bool Equals(IntVector2 other)
		{
			return other.x == x && other.y == y;
		}
		

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is IntVector2 && Equals((IntVector2)obj);
		}

		public override int GetHashCode()
		{
			unchecked{
				return (x.GetHashCode() * 92821) ^ (y.GetHashCode() * 31);
			}
		}

		/// <summary>
		/// 隐式转换
		/// </summary>
		/// <param name="vector"></param>
		public static implicit operator Vector2(IntVector2 vector)
		{
			return new Vector2(vector.x, vector.y);
		} 

		public static explicit operator IntVector2(Vector2 vector)
		{
			return new IntVector2((int)vector.x, (int)vector.y);
		}

		public static bool operator == (IntVector2 left, IntVector2 right)
		{
			return left.Equals(right);
		}

		public static bool operator != (IntVector2 left, IntVector2 right)
		{
			return !left.Equals(right);
		}

		public static IntVector2 operator +(IntVector2 left, IntVector2 right)
		{
			return new IntVector2(left.x + right.x, left.y + right.y);
		}

		public static IntVector2 operator -(IntVector2 left, IntVector2 right)
		{
			return new IntVector2(left.x - right.x, left.y - right.y);
		}


	}
}

