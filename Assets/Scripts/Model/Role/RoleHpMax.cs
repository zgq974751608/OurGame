/*****
************Add by 朱桂清

************创建于2015-01-10

************操作当前血量上限
*****/

using UnityEngine;
using System.Collections;

public class RoleHpMax : BaseProperty {
	float mMaxHp;
	float mHp;
	
	//设置/
	public override void SetProperty(float pProperty){
		mMaxHp = pProperty;
		mHp = pProperty;
		ActiveProperty();
	}
	
	//改变/
	public override void ChangeProperty(float pProperty){
		RemoveProperty();
		mMaxHp += pProperty;
		mHp += pProperty;
		ActiveProperty();
	}
	
	//生效/
	public override void ActiveProperty(){
		roleInstance.MaxHp += mMaxHp;
		roleInstance.Hp += mHp;
	}
	
	//移除/
	public override void RemoveProperty(){
		roleInstance.MaxHp -= mMaxHp;
		roleInstance.Hp -= mHp;
	}
}
