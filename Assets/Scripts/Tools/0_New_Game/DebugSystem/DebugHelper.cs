/*****
************Add by 朱桂清

************创建于2015-01-10

************
*****/

using UnityEngine;
using System.Collections;

public class DebugHelper {
	static bool debugWorked = false;

	public static void InitDebuger(){
		//TODO
		debugWorked = true;
	}

	public static void Log(object obj){
		if(debugWorked) {
			Debug.Log(obj);
		}
	}

	public static void LogError(object obj){
		if(debugWorked){
		 	Debug.LogError(obj);
		}
	}
}
