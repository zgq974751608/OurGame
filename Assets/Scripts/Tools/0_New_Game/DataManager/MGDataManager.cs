/*****
************Add by 朱桂清

************创建于2015-02-28

************游戏数据管理，包括资源列表，最先运行
*****/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class MGDataManager {
	/// <summary>
	/// 资源类型结构,读取资源列表文件时用.
	/// </summary>
	public struct ResourceInfoStruct {
		public string resName;
		public string filePath;
		public int resType;
		public int version;
	}

	//资源类型,囊括一切可动态加载的内容/
	public enum ResourceType{
		Config,			//数据配置表/
	}

	public static Dictionary<string,ResourceInfoStruct> localResourceDic;	//本地资源列表/
	public static Dictionary<string,ResourceInfoStruct> outResourceDic;		//外部资源列表,更新目录/

	/// <summary>
	/// 加载资源列表.
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

				localResourceDic.Add(tStruct.resName , tStruct);
			}
		} catch(Exception e){
			DebugHelper.Log("Load local resource file list error ----> " + e.ToString());
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
					
					localResourceDic.Add(tStruct.resName , tStruct);
				}
			} catch(Exception e){
				DebugHelper.Log("Load out resource file list error ----> " + e.ToString());
			} finally {
				sr1.Close();
			}
		}
	}
}
