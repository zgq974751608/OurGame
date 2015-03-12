/*****
************Add by 朱桂清

************创建于2015-03-11

************文件工具函数类
*****/

using UnityEngine;
using System.Collections;

public class MGFileUtil {

	public static string LoadText(string path)
	{
		return MGFileUtil.LoadTextFile(path, ".txt");
	}
	
	public static string LoadXml(string path)
	{
		return MGFileUtil.LoadTextFile(path, ".xml");
	}

	public static string LoadTextFile(string path, string ext)
	{
		return MGResourceManager.LoadTextFile(path, ext);
	}
}
