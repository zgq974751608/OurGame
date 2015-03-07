/*****
************Add by 朱桂清

************创建于2015-02-28

************数据配置表加载器(单例)
*****/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MGConfigDataLoader {
	static MGConfigDataLoader instance;

	public MGConfigDataLoader Instance(){
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
			Dictionary<string,MGDataManager.ResourceInfoStruct> dic = MGDataManager.newerResourceDic;
			foreach(KeyValuePair<string,MGDataManager.ResourceInfoStruct> kvp in dic){
				MGDataManager.ResourceInfoStruct tStruct = kvp.Value;
				if(tStruct.resType != (int)MGDataManager.ResourceType.Config){
					continue;
				}

				if(tStruct.resourcePathType == (int)MGDataManager.ResourceTathType.Resource){

				} else if(tStruct.resourcePathType == (int)MGDataManager.ResourceTathType.Streaming){

				} else {
					DebugHelper.Log("错误的配置表路径类型--->:" + tStruct.resourcePathType);
				}
			}
		}catch(Exception e){
			DebugHelper.Log("加载配置表发生错误" + e.ToString());
		}
	}
}
