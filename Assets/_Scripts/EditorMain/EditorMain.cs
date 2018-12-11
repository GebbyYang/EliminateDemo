namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;

	public class EditorMain : MonoBehaviour {
		
		public EditorModuleController Controller;

		public Transform EditorRoot;

		public EditorLevelConfig currentLevelConfig;

		public EditorGrid[,] Grids;

		public EditorMain()
		{
			Controller = new EditorModuleController();
		}

		void Start()
		{
			InitModules();
		}

		private void InitModules()
		{
			Controller.AddModule<EditorFileModule>(new EditorFileModule(this));
			Controller.AddModule<EditorPieceModule>(new EditorPieceModule(this));
			Controller.AddModule<EditorLayerModule>(new EditorLayerModule(this));

		}

	}
}

