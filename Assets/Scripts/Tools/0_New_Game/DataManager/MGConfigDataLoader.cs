/*****
************Add by 朱桂清

************创建于2015-02-28

************数据配置表加载器(单例)
*****/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

public class MGConfigDataLoader {
	static MGConfigDataLoader instance;

	public static MGConfigDataLoader Instance(){
		if(instance == null){
			instance = new MGConfigDataLoader();
		}

		return instance;
	}

	bool configDataLoaded = false;				//标志位,控制是否加载过/

	public void LoadConfigData(){
		if(configDataLoaded){
			DebugHelper.Log("配置表已经加载过了");
			return;
		}

		try{
			Dictionary<string,MGAssetManager.ResourceInfoStruct> dic = MGAssetManager.newerResourceDic;
			foreach(KeyValuePair<string,MGAssetManager.ResourceInfoStruct> kvp in dic){
				MGAssetManager.ResourceInfoStruct tStruct = kvp.Value;
				if(tStruct.resType != (int)MGAssetManager.ResourceType.Config){
					continue;
				}
			}
		}catch(Exception e){
			DebugHelper.Log(StringTool.Append("加载配置表发生错误" , e.ToString()));
		}
	}

	void OnLoadedConfig(UnityEngine.Object obj){
		if(obj != null){
			string content = "";
			TextAsset text = obj as TextAsset;
			content = text.text;
			MemoryConfigData(content);
		}
	}

	//将配置表加入内存/
	public void MemoryConfigData(string text){
		Xml tmxl = Xml.GetHelper(text);
		SecurityElement se = tmxl.LoadXml();
	}
}
