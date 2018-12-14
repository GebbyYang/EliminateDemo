namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;

	public class EditorPiece {

		private int m_Id;
		private string m_Name;
		private int m_Layer;
		private GameObject m_PieceView;
		private EditorPieceView m_EPieceView;
		private EditorPieceModule m_main;
		private Sprite m_Sprite;

		public EditorPiece(int id, string pieceName, int layer, EditorPieceModule main)
		{
			m_Id = id;
			m_Name = pieceName;
			m_Layer = layer;
			m_main = main;
		}

		public int GetPieceId()
		{
			return m_Id;
		}

		public string GetPieceName()
		{
			return m_Name;
		}

		public int GetPieceLayer()
		{
			return m_Layer;
		}

		public void BuildView(Transform root)
		{
			var go = TSingleTon<PrefabLoad>.Singleton().LoadFromResource("EditorView", "PieceView");
			BuildView(go, root);
		}

		public void BuildView(GameObject go, Transform root)
		{
			m_PieceView = GameObject.Instantiate(go);
			m_PieceView.transform.SetParent(root);
			m_EPieceView = m_PieceView.GetComponent<EditorPieceView>();
			m_EPieceView.SelectButton.onClick.AddListener(OnClick);
			SetSprite();
			float tempX = -m_Id * 80;
			m_PieceView.transform.localPosition = new Vector3(tempX, 0, 0);
		}

		public void SelectPiece(bool select)
		{
			m_EPieceView.Selected.gameObject.SetActive(select);
		}

		public Sprite GetSprite()
		{
			return m_Sprite;
		}

		private void SetSprite()
		{
			GameObject prefab = TSingleTon<PrefabLoad>.Singleton().LoadFromResource("Pieces", m_Name) as GameObject;
			m_EPieceView.Icon.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
			m_Sprite = m_EPieceView.Icon.sprite;
		}
		

		private void OnClick()
		{
			if(m_main.selectPiece != null)
			{
				if(m_main.selectPiece == this)
				{
					m_main.selectPiece = null;
					SelectPiece(false);
				}else{
					m_main.selectPiece.SelectPiece(false);
					m_main.selectPiece = this;
					SelectPiece(true);
				}
			}else{
				m_main.selectPiece = this;
				SelectPiece(true);
			}
		}
		
	}
}

