/*****
************Add by 朱桂清

************创建于2015-01-10

************
*****/

using UnityEngine;
using System.Collections;

public class RoleHpMaxPercent : BaseProperty {
	float mMaxHpPercent;
	float mHpPercent;
	
	//设置/
	public override void SetProperty(float pProperty){
		mMaxHpPercent = pProperty;
		mHpPercent = pProperty;
		ActiveProperty();
	}
	
	//改变/
	public override void ChangeProperty(float pProperty){
		RemoveProperty();
		mMaxHpPercent += pProperty;
		mHpPercent += pProperty;
		ActiveProperty();
	}
	
	//生效/
	public override void ActiveProperty(){
		roleInstance.MaxHp *= mMaxHpPercent;
		roleInstance.Hp *= mHpPercent;
	}
	
	//移除/
	public override void RemoveProperty(){
		roleInstance.MaxHp /= mMaxHpPercent;
		roleInstance.Hp /= mHpPercent;
	}
}
