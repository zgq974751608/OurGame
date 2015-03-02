/*****
************Add by 朱桂清

************创建于2015-02-28

************数据配置表加载器(单例)
*****/

using UnityEngine;
using System.Collections;

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

	}
}
