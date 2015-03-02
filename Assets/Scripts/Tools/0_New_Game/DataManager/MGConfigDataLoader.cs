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

	public Dictionary<string,BaseDataClass> configDataDic = new Dictionary<string, BaseDataClass>();

	bool configDataLoaded = false;				//标志位,控制是否加载过/

	/// <summary>
	/// 加载数据配置表.
	/// </summary>
	public void LoadConfigData (){
		Dictionary<string,MGDataManager.ResourceInfoStruct> localResourceDic = MGDataManager.localResourceDic;
		Dictionary<string,MGDataManager.ResourceInfoStruct> outResourceDic = MGDataManager.outResourceDic;
		foreach(KeyValuePair<string , MGDataManager.ResourceInfoStruct> kvp in localResourceDic){
			string tableName = kvp.Key;
			if(MGDataManager.CheckLoadOutResource(tableName)){
				//使用外部的配置表/
				MGDataManager.ResourceInfoStruct configInfo = outResourceDic[tableName];
			} else {
				//使用包内的配置表/
				MGDataManager.ResourceInfoStruct configInfo = localResourceDic[tableName];
				TextAsset text = Resources.Load(configInfo.filePath) as TextAsset;

			}
		}
	}
}
