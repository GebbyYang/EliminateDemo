using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class TextureUtils : Editor {
	
	[MenuItem("Assets/Editor Utils/SpriteToPrefab")]
	public static void SpriteToPrefab()
	{
		Texture2D[] objs = Selection.GetFiltered<Texture2D>(SelectionMode.DeepAssets);;
		foreach(Texture2D obj in objs)
		{
			Sprite tempSprite = AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(obj), typeof(Sprite)) as Sprite;
			// Create And And Base Component
			GameObject gameObject = new GameObject(obj.name);
			SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
			renderer.sprite = tempSprite;
			gameObject.AddComponent<BoxCollider2D>();
			// Path Config
			string folderName = new DirectoryInfo(Path.GetDirectoryName(AssetDatabase.GetAssetPath(obj))).Name;
			string relativeFolder = string.Format("_Prefabs/Resources/{0}", folderName);
			string absoluteFolder = Path.Combine(Application.dataPath, relativeFolder);
			if(!Directory.Exists(absoluteFolder))
			{
				Directory.CreateDirectory(absoluteFolder);
				Debug.LogWarning("Folder Not Exists, Create Target Folder");
				AssetDatabase.Refresh();
			}
			string filePath = string.Format(@"Assets/{0}/{1}.prefab", relativeFolder, obj.name);
			PrefabUtility.CreatePrefab(filePath, gameObject);
			GameObject.DestroyImmediate(gameObject);
		}
	}

}
