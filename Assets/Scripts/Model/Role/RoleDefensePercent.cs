/*****
************Add by 朱桂清

************创建于2015-01-10

************
*****/

using UnityEngine;
using System.Collections;

public class RoleDefensePercent : BaseProperty {
	
	float mDefensePercent;
	
	//设置/
	public override void SetProperty(float pProperty){
		mDefensePercent = pProperty;
		ActiveProperty();
	}
	
	//改变/
	public override void ChangeProperty(float pProperty){
		RemoveProperty();
		mDefensePercent += pProperty;
		ActiveProperty();
	}
	
	//生效/
	public override void ActiveProperty(){
		roleInstance.Defense += roleInstance.Hp * mDefensePercent;
	}
	
	//移除/
	public override void RemoveProperty(){
		roleInstance.Defense -= roleInstance.Hp * mDefensePercent;
	}
}
