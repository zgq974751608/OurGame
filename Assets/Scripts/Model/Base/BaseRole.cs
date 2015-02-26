/*****
************Add by 朱桂清

************创建于2015-01-24

************父角色类
*****/

using UnityEngine;
using System.Collections;

public class BaseRole {
	#region 基本属性
	public AttitudeType attitude;

	//血量/
	float hp;
	public virtual float Hp{
		get;
		set;
	}
	
	//最大血量/
	float maxHp;
	public virtual float MaxHp{
		get;
		set;
	}

	//攻击/
	float attack;
	public virtual float Attack{
		get;
		set;
	}
	
	//防御/
	float defense;
	public virtual float Defense{
		get;
		set;
	}

	//暴击率/
	float critRate;
	public virtual float CritRate{
		get;
		set;
	}

	//暴击伤害/
	float critDamage;
	public virtual float CritDamage{
		get;
		set;
	}

	//韧性/
	float tenacity;
	public virtual float Tenacity{
		get;
		set;
	}

	//命中/
	float hit;
	public virtual float Hit{
		get;
		set;
	}

	//闪避/
	float dodge;
	public virtual float Dodge{
		get;
		set;
	}

	//火属性伤害减免系数/
	float fireDamageReduce;
	public virtual float FireDamageReduce{
		get;
		set;
	}

	//水属性伤害减免系数/
	float waterDamageReduce;
	public virtual float WaterDamageReduce{
		get;
		set;
	}

	//地属性伤害减免系数/
	float earthDamageReduce;
	public virtual float EarthDamageReduce{
		get;
		set;
	}

	//光属性伤害减免系数/
	float lightDamageReduce;
	public virtual float LightDamageReduce{
		get;
		set;
	}

	//暗属性伤害减免系数/
	float darkDamageReduce;
	public virtual float DarkDamageReduce{
		get;
		set;
	}

	//治疗加成系数/
	float treatAdditionRate;
	public virtual float TtreatAdditionRate{
		get;
		set;
	}
	
	//火属性伤害反弹系数/
	float fireDamageRebound;
	public virtual float FireDamageRebound{
		get;
		set;
	}
	
	//水属性伤害反弹系数/
	float waterDamageRebound;
	public virtual float WaterDamageRebound{
		get;
		set;
	}
	
	//地属性伤害反弹系数/
	float earthDamageRebound;
	public virtual float EarthDamageRebound{
		get;
		set;
	}
	
	//光属性伤害反弹系数/
	float lightDamageRebound;
	public virtual float LightDamageRebound{
		get;
		set;
	}
	
	//暗属性伤害反弹系数/
	float darkDamageRebound;
	public virtual float DarkDamageRebound{
		get;
		set;
	}

	//晕眩减免系数/
	float xuanyunReduce;
	public virtual float XuanyunReduce{
		get;
		set;
	}

	//减速减免系数/
	float jiansuReduce;
	public virtual float JiansuReduce{
		get;
		set;
	}

	//禁足减免系数/
	float jinzuReduce;
	public virtual float JinzuReduce{
		get;
		set;
	}

	//沉默减免系数/
	float chenmoReduce;
	public virtual float ChenmoReduce{
		get;
		set;
	}

	//混乱减免系数/
	float hunluanReduce;
	public virtual float HunluanReduce{
		get;
		set;
	}

	//魅惑减免系数/
	float meihuoReduce;
	public virtual float MeihuoReduce{
		get;
		set;
	}

	//冰冻减免系数/
	float bingdongReduce;
	public virtual float BingdongReduce{
		get;
		set;
	}

	//麻痹减免系数/
	float mabiReduce;
	public virtual float MabiReduce{
		get;
		set;
	}


	#endregion
}
