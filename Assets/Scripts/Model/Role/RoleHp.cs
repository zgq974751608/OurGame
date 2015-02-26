/*****
************Add by 朱桂清

************创建于2015-01-10

************操作当前血量(加血效果，不影响血上限)
*****/

using UnityEngine;
using System.Collections;

public class RoleHp : BaseProperty {
	float mHp;

	//创建,不会立即生效/
	public override void CreateProperty(float pProperty){
		mHp = pProperty;
	}

	//设置,立即生效/
	public override void SetProperty(float pProperty){
		mHp = pProperty;
		ActiveProperty();
	}
	
	//改变/
	public override void ChangeProperty(float pProperty){
		RemoveProperty();
		mHp += pProperty;
		ActiveProperty();
	}
	
	//生效/
	public override void ActiveProperty(){
		roleInstance.Hp += mHp;
	}
	
	//移除/
	public override void RemoveProperty(){
		roleInstance.Hp -= mHp;
	}
}
