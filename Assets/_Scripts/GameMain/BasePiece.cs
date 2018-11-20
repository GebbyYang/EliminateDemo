namespace GameMain
{
	
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// 普通消除物逻辑
	/// </summary>
	public abstract class BasePiece {
		// 
		public int PosX{set; get;}
		public int PosY{set; get;}
		// 
		public int TargetX{set; get;}
		public int TargetY{set; get;}
		// 
		public int PieceLayer{set; get;}
		// 
		public GameObject pieceGameObject {private set; get;}

		/// <summary>
		/// 创建一个普通消除物
		/// </summary>
		public virtual void cretatePiece()
		{
			
		}

		/// <summary>
		/// 销毁当前普通消除物
		/// </summary>
		public virtual void onCrush()
		{

		}

		/// <summary>
		/// 普通消除物的下落停靠
		/// </summary>
		public virtual void onLanded()
		{

		}

		/// <summary>
		/// 设置
		/// </summary>
		public void setPosition()
		{
			
		}
		
	}

}
