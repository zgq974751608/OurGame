﻿/*****
************Add by 朱桂清

************创建于2015-02-28

************游戏数据管理，包括资源列表，最先运行，放在GameObject上面
*****/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class MGAssetManager : MonoBehaviour{
	public delegate void OnResourceLoaded (UnityEngine.Object obj);

	/// <summary>
	/// 资源类型结构,读取资源列表文件时用.
	/// </summary>
	public struct ResourceInfoStruct {
		public string resName;
		public string filePath;
		public int resType;
		public int version;
		public int resourcePathType;
	}

	//资源类型,囊括一切可动态加载的内容/
	public enum ResourceType{
		Config,			//数据配置表/
	}

	public enum ResourcePathType{
		Resource,
		Streaming,
		Persistant,
	}

	public static Dictionary<string,ResourceInfoStruct> newerResourceDic;				//较新的资源列表/

	static Dictionary<string,ResourceInfoStruct> localResourceDic;						//本地资源列表/
	static Dictionary<string,ResourceInfoStruct> outResourceDic;						//外部资源列表,更新目录/

	/// <summary>
	/// 加载资源列表，并得到最新的资源列表.
	/// </summary>
	public static void LoadResourceList(){
		TextAsset text = Resources.Load(ResourcePath.LOCALRESOURCELISTPATH) as TextAsset;
		StringReader sr = new StringReader(text.text);
		try{
			while(sr.Peek() != -1){
				string line = sr.ReadLine();
				string[] configArgs = line.Split(',');
				ResourceInfoStruct tStruct = new ResourceInfoStruct();
				tStruct.resName = configArgs[0];
				tStruct.filePath = configArgs[1];
				tStruct.resType = int.Parse(configArgs[2]);
				tStruct.version = int.Parse(configArgs[3]);
				tStruct.resourcePathType = (int)ResourcePathType.Resource;

				localResourceDic.Add(tStruct.resName , tStruct);
			}
		} catch(Exception e){
			DebugHelper.Log(StringTool.Append("加载本地资源列表错误 ----> " + e.ToString()));
		} finally {
			sr.Close();
		}

		if(File.Exists(ResourcePath.OUTRESOURCELISTPATH)){
			FileStream fs = File.OpenRead(ResourcePath.OUTRESOURCELISTPATH);
			byte[] bytes = new byte[fs.Length];
			fs.Read(bytes, 0, (int)fs.Length);
			StringReader sr1 = new StringReader(Encoding.UTF8.GetString(bytes));

			try{
				while(sr1.Peek() != -1){
					string line = sr1.ReadLine();
					string[] configArgs = line.Split(',');
					ResourceInfoStruct tStruct = new ResourceInfoStruct();
					tStruct.resName = configArgs[0];
					tStruct.filePath = configArgs[1];
					tStruct.resType = int.Parse(configArgs[2]);
					tStruct.version = int.Parse(configArgs[3]);
					tStruct.resourcePathType = (int)ResourcePathType.Streaming;
					
					localResourceDic.Add(tStruct.resName , tStruct);
				}
			} catch(Exception e){
				DebugHelper.Log(StringTool.Append("加载外部资源列表错误 ----> ",e.ToString()));
			} finally {
				sr1.Close();
			}
		}

		foreach(KeyValuePair<string,ResourceInfoStruct> kvp in localResourceDic){
			if(outResourceDic.ContainsKey(kvp.Key)){
				ResourceInfoStruct tStruct = outResourceDic[kvp.Key];
				if(tStruct.version > kvp.Value.version){
					newerResourceDic.Add(kvp.Key,tStruct);
				} else {
					newerResourceDic.Add(kvp.Key,kvp.Value);
				}
			} else {
				newerResourceDic.Add(kvp.Key,kvp.Value);
			}
		}

		foreach(KeyValuePair<string,ResourceInfoStruct> kvp in outResourceDic){
			if(!localResourceDic.ContainsKey(kvp.Key)){
				newerResourceDic.Add(kvp.Key,kvp.Value);
			}
		}
	}

	/// <summary>
	/// 根据版本号，判断加载哪一个资源，参数是内部资源列表的key.
	/// </summary>
	public static bool CheckLoadOutResource(string strKey){
		if(outResourceDic.ContainsKey(strKey)){
			ResourceInfoStruct localStruct = localResourceDic[strKey];
			ResourceInfoStruct outStruct = outResourceDic[strKey];
			if(localStruct.version < outStruct.version){
				return true;
			} else {
				return false;
			}
		}

		return false;
	}

	#region 继承于mono

	void Start(){
		StartCoroutine(LoadOutResource());
	}

	struct WaitLoadStruct{
		public string assetPath;
		public OnResourceLoaded onResourceLoaded;
	}

	static List<WaitLoadStruct> waitToLoadList = new List<WaitLoadStruct>(); 

	public static void LoadResource(string assetPath , ResourcePathType pathType , OnResourceLoaded onResourceLoaded){
		if(pathType == ResourcePathType.Resource){
			UnityEngine.Object obj = Resources.Load(assetPath);
			if(onResourceLoaded != null){
				onResourceLoaded(obj);
			}
		} else if(pathType == ResourcePathType.Persistant){
			WaitLoadStruct tStruct = new WaitLoadStruct();
			tStruct.assetPath = assetPath;
			tStruct.onResourceLoaded = onResourceLoaded;
			waitToLoadList.Add(tStruct);
		}
	}

	IEnumerator LoadOutResource(){
		while(true){
			if(waitToLoadList.Count > 0){
				WaitLoadStruct tStruct = waitToLoadList[0];
				string realPath = StringTool.Append(ResourcePath.GetAppOutResourcePath(),"/",tStruct.assetPath);
				WWW tWww = new WWW(realPath);
				yield return tWww;

				if(tWww.error != null){
					DebugHelper.Log(StringTool.Append("加载资源错误----> " , tWww.error,"--->",realPath));
				} else {
					if(tStruct.onResourceLoaded != null){
						UnityEngine.Object asset = tWww.assetBundle.mainAsset;
						tStruct.onResourceLoaded(asset);
						tWww.assetBundle.Unload(false);
					}
					waitToLoadList.RemoveAt(0);
				}
			}

			yield return new WaitForEndOfFrame();
		}
	}
	#endregion
}
