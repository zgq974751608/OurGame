/*****
************Add by 朱桂清

************创建于2015-01-10

************基础属性类
*****/

public class BaseProperty {
	//角色实例/
	public Role roleInstance;

	//是否每回合叠加生效/
	public bool affectPerBout = false;

	//创建/
	public virtual void CreateProperty(float pProperty){
		
	}

	//设置/
	public virtual void SetProperty(float pProperty){

	}

	//改变/
	public virtual void ChangeProperty(float pProperty){

	}

	//生效/
	public virtual void ActiveProperty(){

	}

	//移除/
	public virtual void RemoveProperty(){

	}
}
