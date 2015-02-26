/*****
************Add by 朱桂清

************创建于2015-01-10

************角色类,不等同于玩家类
*****/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Role : BaseRole{

	public Role(){
		equipDic = new Dictionary<int, Equipment>();
	}

#region 基本属性
	//血量/
	public AttitudeType attitude = AttitudeType.None;

	float hp;
	public override float Hp{
		get{
			return hp;
		}
		set{
			hp =  value;

			if(hp > maxHp){
				hp = maxHp;
			}
		}
	}

	//最大血量/
	float maxHp;
	public override float MaxHp{
		get{
			return maxHp;
		}
		set{
			maxHp = value;
		}
	}
	
	//攻击/
	float attack;
	public override float Attack{
		get{
			return attack;
		}
		set{
			attack = value;
		}
	}

	//防御/
	float defense;
	public override float Defense{
		get{
			return defense;
		}
		set{
			defense = value;
		}
	}
	
	//暴击率/
	float critRate;
	public override float CritRate{
		get{
			return critRate;
		}
		set{
			critRate = value;
		}
	}
	
	//暴击伤害/
	float critDamage;
	public override float CritDamage{
		get{
			return critDamage;
		}
		set{
			critDamage = value;
		}
	}
	
	//韧性/
	float tenacity;
	public override float Tenacity{
		get{
			return tenacity;
		}
		set{
			tenacity = value;
		}
	}
	
	//命中/
	float hit;
	public override float Hit{
		get{
			return hit;
		}
		set{
			hit = value;
		}
	}
	
	//闪避/
	float dodge;
	public override float Dodge{
		get{
			return dodge;
		}
		set{
			dodge = value;
		}
	}
	
	//火属性伤害减免系数/
	float fireDamageReduce;
	public override float FireDamageReduce{
		get{
			return fireDamageReduce;
		}
		set{
			fireDamageReduce = value;
		}
	}
	
	//水属性伤害减免系数/
	float waterDamageReduce;
	public override float WaterDamageReduce{
		get{
			return waterDamageReduce;
		}
		set{
			waterDamageReduce = value;
		}
	}
	
	//地属性伤害减免系数/
	float earthDamageReduce;
	public override float EarthDamageReduce{
		get{
			return earthDamageReduce;
		}
		set{
			earthDamageReduce = value;
		}
	}
	
	//光属性伤害减免系数/
	float lightDamageReduce;
	public override float LightDamageReduce{
		get{
			return lightDamageReduce;
		}
		set{
			lightDamageReduce = value;
		}
	}
	
	//暗属性伤害减免系数/
	float darkDamageReduce;
	public override float DarkDamageReduce{
		get{
			return darkDamageReduce;
		}
		set{
			darkDamageReduce = value;
		}
	}
	
	//治疗加成系数/
	float treatAdditionRate;
	public override float TtreatAdditionRate{
		get{
			return treatAdditionRate;
		}
		set{
			treatAdditionRate = value;
		}
	}
	
	//火属性伤害反弹系数/
	float fireDamageRebound;
	public override float FireDamageRebound{
		get{
			return fireDamageRebound;
		}
		set{
			fireDamageRebound = value;
		}
	}
	
	//水属性伤害反弹系数/
	float waterDamageRebound;
	public override float WaterDamageRebound{
		get{
			return waterDamageRebound;
		}
		set{
			waterDamageRebound = value;
		}
	}
	
	//地属性伤害反弹系数/
	float earthDamageRebound;
	public override float EarthDamageRebound{
		get{
			return earthDamageRebound;
		}
		set{
			earthDamageRebound = value;
		}
	}
	
	//光属性伤害反弹系数/
	float lightDamageRebound;
	public override float LightDamageRebound{
		get{
			return lightDamageRebound;
		}
		set{
			lightDamageRebound = value;
		}
	}
	
	//暗属性伤害反弹系数/
	float darkDamageRebound;
	public override float DarkDamageRebound{
		get{
			return darkDamageRebound;
		}
		set{
			darkDamageRebound = value;
		}
	}
	
	//晕眩减免系数/
	float xuanyunReduce;
	public override float XuanyunReduce{
		get{
			return xuanyunReduce;
		}
		set{
			xuanyunReduce = value;
		}
	}
	
	//减速减免系数/
	float jiansuReduce;
	public override float JiansuReduce{
		get{
			return jiansuReduce;
		}
		set{
			jiansuReduce = value;
		}
	}
	
	//禁足减免系数/
	float jinzuReduce;
	public override float JinzuReduce{
		get{
			return jinzuReduce;
		}
		set{
			jinzuReduce = value;
		}
	}
	
	//沉默减免系数/
	float chenmoReduce;
	public override float ChenmoReduce{
		get{
			return chenmoReduce;
		}
		set{
			chenmoReduce = value;
		}
	}
	
	//混乱减免系数/
	float hunluanReduce;
	public override float HunluanReduce{
		get{
			return hunluanReduce;
		}
		set{
			hunluanReduce = value;
		}
	}
	
	//魅惑减免系数/
	float meihuoReduce;
	public override float MeihuoReduce{
		get{
			return meihuoReduce;
		}
		set{
			meihuoReduce = value;
		}
	}
	
	//冰冻减免系数/
	float bingdongReduce;
	public override float BingdongReduce{
		get{
			return bingdongReduce;
		}
		set{
			bingdongReduce = value;
		}
	}
	
	//麻痹减免系数/
	float mabiReduce;
	public override float MabiReduce{
		get{
			return mabiReduce;
		}
		set{
			mabiReduce = value;
		}
	}
#endregion
#region 装备
	public Dictionary<int,Equipment> equipDic;

	/// <summary>
	/// 角色穿戴装备.
	/// </summary>
	/// <param name="pEquipPos">装备位置.</param>
	/// <param name="pEquip">装备对象.</param>
	public void WearEquipment(int pEquipPos,Equipment pEquip){
		if(equipDic.ContainsKey(pEquipPos)){
			//角色该部位有装备/
			ChangeEquipment(pEquipPos , pEquip);
		} else {
			equipDic.Add(pEquipPos , pEquip);
		}
	}

	/// <summary>
	/// 角色穿戴装备.
	/// </summary>
	/// <param name="pEquip">装备对象</param>
	public void WearEquipment(Equipment pEquip){
		WearEquipment(pEquip.equipPos,pEquip);
	}

	/// <summary>
	/// 角色卸下装备.
	/// </summary>
	/// <param name="pEquip">装备对象</param>
	public void UnWearEquipment(int pEquipPos){
		equipDic[pEquipPos].UnWear();
		if(equipDic.ContainsKey(pEquipPos)){
			equipDic.Remove(pEquipPos);
		} else {
			DebugHelper.Log("该角色身上没有穿戴该部位的装备!");
		}
	}

	/// <summary>
	/// 更换装备.
	/// </summary>
	public void ChangeEquipment(int pEquipPos,Equipment pEquip){
		UnWearEquipment(pEquipPos);
		WearEquipment(pEquipPos,pEquip);
	}
#endregion

#region 技能

#endregion
}
