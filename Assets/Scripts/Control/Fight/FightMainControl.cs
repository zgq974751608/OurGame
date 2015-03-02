/*****
************Add by 朱桂清

************创建于2015-01-17

************
*****/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightMainControl {
	static FightMainControl instance = new FightMainControl();

	public static FightMainControl Instance(){
		if(instance == null){
			instance = new FightMainControl();
		}

		return instance;
	}

	FightMainControl(){

	}



	/// <summary>
	/// 初始化战斗数据.
	/// </summary>
	public void InitFightData(){

	}

	/// <summary>
	/// 开始战斗.
	/// </summary>
	public void FightStart(){

	}

	/// <summary>
	/// 战斗暂停.
	/// </summary>
	public void FightPause(){

	}

	/// <summary>
	/// 战斗结束.
	/// </summary>
	public void FightEnd(){

	}
}
