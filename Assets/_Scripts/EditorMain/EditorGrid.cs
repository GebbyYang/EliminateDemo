namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;
	using UnityEngine.UI;

	/// <summary>
	/// 对应棋盘中对应X和Y坐标的Grid的信息记录
	/// </summary>
	public class EditorGrid {

		public bool Enable{set; get;}
		public bool IsPort{set; get;}
		public int[] PieceID;
		public string[] PieceStr;
		public int x;
		public int y;
		
		private GameObject[] m_GridViews;
		private EditorGridView[] m_EGridViews;
		private EditorGridModule m_main;
		private Sprite defaultSprite;
		
		/// <summary>
		/// 构造基础Grid对象
		/// </summary>
		public EditorGrid(int posX, int posY, EditorGridModule main)
		{
			x = posX;
			y = posY;
			// 
			PieceID = new int[GlobelConfigs.MaxLayerCount];
			PieceStr = new string[GlobelConfigs.MaxLayerCount];
			m_GridViews = new GameObject[GlobelConfigs.MaxLayerCount];
			m_EGridViews = new EditorGridView[GlobelConfigs.MaxLayerCount];
			Enable = true;
			IsPort = false;
			// 
			m_main = main;
		}

		/// <summary>
		/// 创建预设
		/// </summary>
		/// <param name="root"></param>
		public void BuidGridView(Transform root)
		{
			var go = TSingleTon<PrefabLoad>.Singleton().LoadFromResource("EditorView", "GridView");
			BuidGridView(go, root);
		}

		/// <summary>
		/// 实例化预设，设置坐标，尺寸和图片
		/// </summary>
		public void BuidGridView(GameObject go, Transform root)
		{
			float offsetX = GlobelConfigs.MaxColumn/2f;
			float offsetY = GlobelConfigs.MaxRow/2f;
			// 居中排列
			float tempX = (x - offsetX + 0.5f) * GlobelConfigs.PieceSizeUI;
			float tempY = (y - offsetY + 0.5f) * GlobelConfigs.PieceSizeUI;

			for(int i = 0; i < GlobelConfigs.MaxLayerCount; i++)
			{
				m_GridViews[i] = GameObject.Instantiate(go, root);
				m_GridViews[i].transform.localPosition = new Vector3(tempX, tempY, 0);
				m_EGridViews[i] = m_GridViews[i].GetComponent<EditorGridView>();
				m_EGridViews[i].GridBtn.onClick.AddListener(OnClick);
			}
			defaultSprite = m_EGridViews[0].IconImage.sprite;
		}

		/// <summary>
		/// 设置透明度
		/// </summary>
		/// <param name="value"></param>
		public void SetAlpha(float value, int layer)
		{
			Image[] images = m_GridViews[layer].GetComponentsInChildren<Image>();
			foreach(Image item in images)
			{
				Color temp = item.color;
				temp.a = value;
				item.color = temp;
			}
		}

		/// <summary>
		/// 调整格子的尺寸
		/// </summary>
		private void ResizeGrid()
		{
			if(m_GridViews == null || m_EGridViews == null)
			{
				return;
			}
			Vector2 currentSize = new Vector2(GlobelConfigs.PieceSizeUI, GlobelConfigs.PieceSizeUI);
			for(int i = 0; i < m_EGridViews.Length; i++)
			{
				m_EGridViews[i].BackgroundTransform.sizeDelta = currentSize;
				m_EGridViews[i].IconTransform.sizeDelta = currentSize;
			}
		}

		/// <summary>
		/// Grid被点击逻辑处理
		/// </summary>
		private void OnClick()
		{
			int currentLayer = m_main.GetCurrentLayer();
			EditorPiece piece = m_main.GetSelectPiece();
			if(piece != null && m_EGridViews[currentLayer].IconImage.sprite == defaultSprite)
			{
				m_EGridViews[currentLayer].IconImage.sprite = piece.GetSprite();
				m_main.SetGridPiece(x, y, piece.GetPieceId());
			}else{
				m_EGridViews[currentLayer].IconImage.sprite = defaultSprite;
				m_main.SetGridPiece(x, y, 0);
			}
		}

	}
}

