using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AutoImportTexture : AssetPostprocessor {
	
	/// <summary>
	/// Add this function in a subclass to get a notification just before the texture importer is run.
	/// </summary>
	private void OnPreprocessTexture()
	{
		TextureImporter textureImporter = assetImporter as TextureImporter;
		// 根据地址进行分类
		if(assetPath.Contains("_Sprite"))
		{
			textureImporter.textureType = TextureImporterType.Sprite;
			if(assetPath.Contains("_Single"))
			{
				textureImporter.spriteImportMode = SpriteImportMode.Single;
			}else{
				textureImporter.spriteImportMode = SpriteImportMode.Multiple;
			}
			string AtlasName = new DirectoryInfo(Path.GetDirectoryName(assetPath)).Name;
			textureImporter.spritePackingTag = AtlasName;
		}else if(assetPath.Contains("_Texture"))
		{
			textureImporter.textureType = TextureImporterType.Default;
			textureImporter.textureShape = TextureImporterShape.Texture2D;
		}else if(assetPath.Contains("_Bumpmap"))
		{
			textureImporter.textureType = TextureImporterType.NormalMap;
			textureImporter.textureShape = TextureImporterShape.Texture2D;
		}
		textureImporter.spritePixelsPerUnit = 80;
		textureImporter.wrapMode = TextureWrapMode.Clamp;
		textureImporter.filterMode = FilterMode.Bilinear;

	}

}
