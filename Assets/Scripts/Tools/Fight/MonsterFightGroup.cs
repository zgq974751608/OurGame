﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoTblConfig;
using LitJson;

public class MonsterFightGroup : FightGroup
{
    public int wave = 0;//波数
    Dictionary<int, MonsterData> monsterDataDic = new Dictionary<int, MonsterData>();
    void Awake()
    {
        group = GroupType.Enemy;
        FightManager.GetInstance().enemyGroup = this;
        monsterDataDic = Util.GetDic<MsgMonsterData, MonsterData>();
    }

    public override void Start()
    {
        base.Start();       
        targetGroup = FightManager.GetInstance().mineGroup;
    }

    /// <summary>
    /// 实例化下一波作战单位
    /// </summary>
    public void InstUnits()
    {
        HealthUIManager.instance.ResetDepth();
        int[] ids = DungeonManager.enemyWave[wave];
        wave++;
        for (int i = 0; i < ids.Length; i++)
        {
            MonsterData monsterData = monsterDataDic[ids[i]];
            string modelName = Util.GetConfigString(monsterData.model);
            GameObject child = AssetManager.GetGameObject(modelName,points[i]);
            if (monsterData.direction == 1)
                child.transform.Rotate(new Vector3(0,180,0));
            child.layer = gameObject.layer;
            child.transform.parent = transform.parent;
            MonsterFightUnit unit = child.AddComponent<MonsterFightUnit>();
            fightUnits.Add(unit);
            unit.parentGroup = this;
            unit.orinPoint = points[i];
            //赋值战斗属性
            unit.monsterData = monsterData;
            unit.InitFightAttribute();
            //赋值技能id
            MonsterAttack monsterAttack = child.GetComponent<MonsterAttack>();
            monsterAttack.normalAttackId = (int)monsterData.attackID;
            JsonData data = JsonMapper.ToObject(Util.GetConfigString(monsterData.skill));
            monsterAttack.autoSkillsId = new int[data.Count];
            for (int num = 0; num < monsterAttack.autoSkillsId.Length; num++)
            {
                int id;
                int.TryParse(data[num].ToString(), out id);
                monsterAttack.autoSkillsId[num] = id;
            }
            //血条UI
            Transform healthBarPos = child.transform.Find("blood");
            HealthUIManager.instance.AddNewHealthBar(fightUnits[i], healthBarPos, group == GroupType.Enemy);
        }
    }
}
