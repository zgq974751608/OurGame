/*****
************Add by 朱桂清

************创建于2015-01-10

************
*****/

using UnityEngine;
using System.Collections;

public class RoleDefense : BaseProperty {
	float mDefense;
	
	//设置/
	public override void SetProperty(float pProperty){
		mDefense = pProperty;
		ActiveProperty();
	}
	
	//改变/
	public override void ChangeProperty(float pProperty){
		RemoveProperty();
		mDefense += pProperty;
		ActiveProperty();
	}
	
	//生效/
	public override void ActiveProperty(){
		roleInstance.Defense += mDefense;
	}
	
	//移除/
	public override void RemoveProperty(){
		roleInstance.Defense -= mDefense;
	}
}
