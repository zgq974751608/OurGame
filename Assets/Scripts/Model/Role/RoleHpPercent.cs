/*****
************Add by 朱桂清

************创建于2015-01-10

************操作百分比增加当前血量(加血效果，不操作血上限)
*****/

using UnityEngine;
using System.Collections;

public class RoleHpPercent : BaseProperty {

	float mHpPercent;
	
	//设置/
	public override void SetProperty(float pProperty){
		mHpPercent = pProperty;
		ActiveProperty();
	}
	
	//改变/
	public override void ChangeProperty(float pProperty){
		RemoveProperty();
		mHpPercent += pProperty;
		ActiveProperty();
	}
	
	//生效/
	public override void ActiveProperty(){
		roleInstance.Hp += roleInstance.Hp * mHpPercent;
	}
	
	//移除/
	public override void RemoveProperty(){
		roleInstance.Hp -= roleInstance.Hp * mHpPercent;
	}
}
