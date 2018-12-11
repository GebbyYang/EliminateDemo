namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using Eliminate.Common;

	public class EditorGrid {

		public bool Enable{set; get;}
		public bool IsPort{set; get;}
		public int[] PieceID;
		public string[] PieceStr;
		public int x;
		public int y;
		
		private GameObject m_GridView;

		
		public EditorGrid(int posX, int posY)
		{
			x = posX;
			y = posY;
			// 
			PieceID = new int[GlobelConfigs.maxLayerCount];
			PieceStr = new string[GlobelConfigs.maxLayerCount];
			Enable = true;
			IsPort = false;
		}

		public void BuidGridView(Transform root)
		{
			var go = TSingleTon<PrefabLoad>.Singleton().LoadFromResource("EditorView", "GridView");
			BuidGridView(go, root);
		}

		public void BuidGridView(GameObject go, Transform root)
		{
			m_GridView = GameObject.Instantiate(go);
			m_GridView.transform.SetParent(root);
			float offsetX = GlobelConfigs.maxColumn/2f;
			float offsetY = GlobelConfigs.maxRow/2f;
			float tempX = (x - offsetX) * 80 + 40;
			float tempY = (y - offsetY) * 80 + 40;
			m_GridView.transform.localPosition = new Vector3(tempX, tempY, 0);
		}

	}
}

