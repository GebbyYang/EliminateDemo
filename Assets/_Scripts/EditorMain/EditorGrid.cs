namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;
	using UnityEngine.UI;

	public class EditorGrid {

		public bool Enable{set; get;}
		public bool IsPort{set; get;}
		public int[] PieceID;
		public string[] PieceStr;
		public int x;
		public int y;
		
		private GameObject m_GridView;
		private EditorGridView m_EGridView;
		private EditorGridModule m_main;
		private Sprite defaultSprite;
		
		public EditorGrid(int posX, int posY, EditorGridModule main)
		{
			x = posX;
			y = posY;
			// 
			PieceID = new int[GlobelConfigs.MaxLayerCount];
			PieceStr = new string[GlobelConfigs.MaxLayerCount];
			Enable = true;
			IsPort = false;
			// 
			m_main = main;
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
			float offsetX = GlobelConfigs.MaxColumn/2f;
			float offsetY = GlobelConfigs.MaxRow/2f;
			// 居中排列
			float tempX = (x - offsetX + 0.5f) * GlobelConfigs.PieceSizeUI;
			float tempY = (y - offsetY + 0.5f) * GlobelConfigs.PieceSizeUI;
			m_GridView.transform.localPosition = new Vector3(tempX, tempY, 0);
			m_EGridView = m_GridView.GetComponent<EditorGridView>();
			m_EGridView.GridBtn.onClick.AddListener(OnClick);
			defaultSprite = m_EGridView.IconImage.sprite;
			ResizeGrid();
		}

		public void SetAlpha(float value)
		{
			Image[] images = m_GridView.GetComponentsInChildren<Image>();
			foreach(Image item in images)
			{
				Color temp = item.color;
				temp.a = value;
				item.color = temp;
			}
		}

		private void ResizeGrid()
		{
			if(m_GridView == null || m_EGridView == null)
			{
				return;
			}
			Vector2 currentSize = new Vector2(GlobelConfigs.PieceSizeUI, GlobelConfigs.PieceSizeUI);
			m_EGridView.BackgroundTransform.sizeDelta = currentSize;
			m_EGridView.IconTransform.sizeDelta = currentSize;
		}

		private void OnClick()
		{
			EditorPiece piece = m_main.GetSelectPiece();
			if(piece != null && m_EGridView.IconImage.sprite == defaultSprite)
			{
				m_EGridView.IconImage.sprite = piece.GetSprite();
				m_main.SetGridPiece(x, y, piece.GetPieceId());
			}else{
				m_EGridView.IconImage.sprite = defaultSprite;
				m_main.SetGridPiece(x, y, 0);
			}
		}

	}
}

