/*****
************Add by 朱桂清

************创建于2015-02-27

************buf
*****/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleBuffer{
	public RoleBuffer(){
		
	}
	
	public RoleBuffer(List<BaseProperty> pList){
		for(int i = 0;i < pList.Count;i++){
			pList[i].roleInstance = this.roleInstance;
			this.propertyList.Add(pList[i]);
		}
	}
	
	public Role roleInstance;
	
	List<BaseProperty> propertyList = new List<BaseProperty>();
	
	/// <summary>
	/// 激活buf.
	/// </summary>
	public void ActivateBuffer(){
		for(int i = 0;i < propertyList.Count;i++){
			propertyList[i].ActiveProperty();
		}
	}
	
	/// <summary>
	/// 移除buf.
	/// </summary>
	public void UnactivateBuffer(){
		for(int i = 0;i < propertyList.Count;i++){
			propertyList[i].RemoveProperty();
		}
	}
}
