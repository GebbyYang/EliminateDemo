namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;
	using System;

	public class EditorPieceModule : EditorModuleBase {

		private EditorPieceModuleView moduleView;

		public EditorPiece selectPiece{set; get;}
		
		public EditorPieceModule(EditorMain mainIns) : base(mainIns)
		{
			moduleView = BuildView<EditorPieceModuleView>("PieceModuleView");
		}

		public override void InitView()
		{
			InitSelectPiece();
		}

		public void InitSelectPiece()
		{
			for(int i = 0; i < GlobelConfigs.NormalPieceStr.Length; i++)
			{
				EditorPiece editorPiece = new EditorPiece(GlobelConfigs.NormalPieceID[i], GlobelConfigs.NormalPieceStr[i], 1, this);
				editorPiece.BuildView(moduleView.transform);
			}
		}

	}
}

