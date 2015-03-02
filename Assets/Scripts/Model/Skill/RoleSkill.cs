/*****
************Add by 朱桂清

************创建于2015-02-27

************技能
*****/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleSkill {
	public RoleSkill(){

	}

	public RoleSkill(List<BaseProperty> pList){
		for(int i = 0;i < pList.Count;i++){
			pList[i].roleInstance = this.roleInstance;
			this.propertyList.Add(pList[i]);
		}
	}

	public Role roleInstance;

	List<BaseProperty> propertyList = new List<BaseProperty>();

	/// <summary>
	/// 激活技能.
	/// </summary>
	public void ActivateSkill(){
		for(int i = 0;i < propertyList.Count;i++){
			propertyList[i].ActiveProperty();
		}
	}

	/// <summary>
	/// 移除技能.
	/// </summary>
	public void UnactivateSkill(){
		for(int i = 0;i < propertyList.Count;i++){
			propertyList[i].RemoveProperty();
		}
	}
}
