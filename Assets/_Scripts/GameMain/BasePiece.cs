namespace Eliminate.Main
{
	
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Type;

	/// <summary>
	/// 普通消除物逻辑
	/// </summary>
	public abstract class BasePiece {
		// 
		protected int PosX{set; get;}
		protected int PosY{set; get;}
		// 
		protected string PieceColor{set; get;}
		// 
		public int TargetX{set; get;}
		public int TargetY{set; get;}
		// 
		public bool Moveable{set; get;}
		// 
		public bool IsCrushing{set; get;}
		// 
		public bool IsDroping{set; get;}
		// 
		public PieceLayerType PieceLayer{set; get;}
		// 
		public GameObject pieceGameObject {private set; get;}

		/// <summary>
		/// 创建一个普通消除物
		/// </summary>
		public virtual void CretatePiece()
		{
			
		}

		public virtual void OnDroping()
		{

		}

		/// <summary>
		/// 销毁当前普通消除物
		/// </summary>
		public virtual void OnCrush()
		{

		}

		/// <summary>
		/// 普通消除物的下落停靠
		/// </summary>
		public virtual void OnLanded()
		{

		}

		/// <summary>
		/// 设置
		/// </summary>
		public void SetPosition()
		{
			
		}
		
	}

}
