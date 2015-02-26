using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightGroup : MonoBehaviour {
    public Transform[] points;
    [HideInInspector]
    public List<FightUnit> fightUnits = new List<FightUnit>();
    [HideInInspector]
    public List<FightUnit> fightUnitsBak = new List<FightUnit>();
    public enum GroupType
    {
        Mine,
        Enemy,
    }
    public GroupType group;
    [HideInInspector]
    public FightGroup targetGroup;
    //排在最前面的单位
    public FightUnit FirstUnit
    {
        get { ResortByPos(); return fightUnitsSortByPos[0]; }
    }
    //排在最后的单位
    public FightUnit LastUnit
    {
        get { ResortByPos(); return fightUnitsSortByPos[fightUnitsSortByPos.Count - 1]; }
    }

    //按位置从前往后排列
    public List<FightUnit> fightUnitsSortByPos = new List<FightUnit>();
    public void ResortByPos()
    {
        fightUnitsSortByPos = new List<FightUnit>(fightUnits);
        fightUnitsSortByPos.Sort(delegate(FightUnit u1, FightUnit u2)
        { return group == GroupType.Mine ? -(u1.mTrans.localPosition.x.CompareTo(u2.mTrans.localPosition.x)) : u1.mTrans.localPosition.x.CompareTo(u2.mTrans.localPosition.x); });
    }

    //按血百分比从低到高的排列
    public List<FightUnit> fightUnitsSortByHP = new List<FightUnit>();
    public FightUnit WeakestUnit{ get{ResortByHP();return fightUnitsSortByHP[0];}}
    public void ResortByHP()
    {
        fightUnitsSortByHP = new List<FightUnit>(fightUnits);
        fightUnitsSortByHP.Sort(delegate(FightUnit u1,FightUnit u2){ return u1.healthValue.CompareTo(u2.healthValue); });
    }

    //阵营中心点
    public Vector3 Center
    {
        get
        {
            ResortByPos();
            return (fightUnitsSortByPos[0].mTrans.localPosition + fightUnitsSortByPos[fightUnitsSortByPos.Count - 1].mTrans.localPosition) * 0.5f;
        }
    }

    /// <summary>
    /// 战斗开始前向前移动
    /// </summary>
    public void MoveForward()
    {
        for (int i = 0; i < fightUnits.Count; i++)
        {
            fightUnits[i].state = FightUnit.UnitState.MoveForward;
        }
    }

    ////检查重叠
    //void CheckOverlay()
    //{
    //    for (int i = 0; i < fightUnits.Count; i++)
    //    {
    //        for (int j = i + 1; j < fightUnits.Count; j++)
    //        {
    //            Vector3 pos1 = fightUnits[i].transform.localPosition;
    //            if (fightUnits[i].modifiedY != 0)
    //                pos1.y = fightUnits[i].modifiedY;
    //            Vector3 pos2 = fightUnits[j].transform.localPosition;
    //            if (fightUnits[j].modifiedY != 0)
    //                pos2.y = fightUnits[j].modifiedY;
    //            if (IsInOverlay(pos1, pos2))
    //            {
    //                if (fightUnits[j].modifiedY != 0)
    //                    fightUnits[j].modifiedY -= Const.ModifyYStep;
    //                else
    //                    fightUnits[j].modifiedY = pos2.y - Const.ModifyYStep;
    //            }
    //        }
    //    }
    //}

    //bool IsInOverlay(Vector3 pos1,Vector3 pos2)
    //{
    //    return Mathf.Abs(pos1.x - pos2.x) <= Const.OverlayRule.x && Mathf.Abs(pos1.y - pos2.y) <= Const.OverlayRule.y;
    //}

    public virtual void Start()
    {
        //InvokeRepeating("CheckOverlay", 1f, 1f);
    }
}
