/*****
************Add by 朱桂清

************创建于2015-03-13

************字符串工具
*****/

using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class StringTool {

	/// <summary>
	/// 将字符串按顺序拼接.
	/// </summary>
	public static string Append(params string[] strs){
		StringBuilder sb = new StringBuilder();
		for(int i = 0;i < strs.Length;i++){
			sb.Append(strs[i]);
		}
		return sb.ToString();
	}
}
