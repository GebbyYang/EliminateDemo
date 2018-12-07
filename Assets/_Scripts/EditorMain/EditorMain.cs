namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;

	public class EditorMain : MonoBehaviour {
		
		public EditorModuleController controller;

		public Transform EditorRoot;

		public EditorMain()
		{
			controller = new EditorModuleController();
		}

		void Start()
		{
			InitModules();
		}

		private void InitModules()
		{
			controller.AddModule<EditorFileModule>(new EditorFileModule(this));
		}

	}
}

