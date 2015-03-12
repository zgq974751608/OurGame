/*****
************Add by 朱桂清

************创建于2015-02-28

************资源内存数据管理
*****/

using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class MGResourceManager {
	
	private static Hashtable texts = new Hashtable();
	
	public static string LoadTextFile(string path, string ext)
	{
		object obj = texts[path];
		if (obj == null)
		{
			string text = string.Empty;
			TextAsset ta = Resources.Load(path)as TextAsset;
			texts.Add(path, ta.text);
			return text;
		}
		return obj as string;
	}
}
