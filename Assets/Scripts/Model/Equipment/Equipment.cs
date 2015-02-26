/*****
************Add by 朱桂清

************创建于2015-01-10

************装备
*****/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment {
	public Equipment(){

	}

	public Equipment(List<BaseProperty> pList){
		for(int i = 0;i < pList.Count;i++){
			pList[i].roleInstance = this.roleInstance;
			this.propertyList.Add(pList[i]);
		}
	}

	public Role roleInstance;
	//装备部位/
	public int equipPos;

	List<BaseProperty> propertyList = new List<BaseProperty>();

	/// <summary>
	/// 穿戴装备.
	/// </summary>
	public void Wear(){
		for(int i = 0;i < propertyList.Count;i++){
			propertyList[i].ActiveProperty();
		}
	}

	/// <summary>
	/// 卸下装备.
	/// </summary>
	public void UnWear(){
		for(int i = 0;i < propertyList.Count;i++){
			propertyList[i].RemoveProperty();
		}
	}
}