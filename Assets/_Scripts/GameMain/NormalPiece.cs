namespace Eliminate.Main
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class NormalPiece : BasePiece {
		
		/// <summary>
		/// 初始化
		/// </summary>
		public NormalPiece(int _x, int _y, string _color)
		{
			PosX = _x;
			PosY = _y;
			PieceColor = _color;
		}

	}
}

