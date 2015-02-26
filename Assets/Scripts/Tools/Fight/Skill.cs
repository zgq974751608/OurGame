using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoTblConfig;
/// <summary>
/// 控制技能释放的表现
/// </summary>
public class Skill : MonoBehaviour {  
    public enum SkillState
    {
        None,//无
        Before,//攻击前摇
        Ing,//攻击中
        After,//攻击后摇
    }

    public class buffConfig
    {
        public int id;//buffid
        public float percent;//攻击力的百分比
        public float value;//绝对值
        public float time;//持续时间
        public int level; //命中等级(控制类的debuff有命中计算) 

        public UniqueSkill bindSkill;//绑定的绝技     
    }

    public SkillState state =  SkillState.None;
    /// <summary>
    /// 己方
    /// </summary>
    public FightUnit mineUnit;
    /// <summary>
    /// 敌方
    /// </summary>
    public FightUnit targetUnit;
    /// <summary>
    /// 魔法特效
    /// </summary>
    public int dmgEffectId;
    public SpecialEffect dmgEffect;
    public string dmgEffectName;
    /// <summary>
    /// 施法特效
    /// </summary>
    public int castEffectId;
    public SpecialEffect castEffect;
    public string castEffectName;
    public Transform castEffectTrans;
    /// <summary>
    /// 飞行特效
    /// </summary>
    public int flyEffectId;
    public FlyingEffect flyEffect;
    public string flyEffectName;
    public Transform flyEffectTrans;
    public Vector3 flyEffectFromPos 
    { 
        get 
        {
            Vector3 offset = flyEffectTrans.localPosition;
            if (mineUnit.mTrans.localRotation.eulerAngles.y != 0)
                offset.x *= -1;
            return mineUnit.mTrans.localPosition + offset;
        } 
    }

    /// <summary>
    /// 命中特效
    /// </summary>
    public int hitEffectId;
    public SpecialEffect hitEffect;
    public string hitEffectName;  

    public virtual void Init()
    {
        BindAnim();//绑定动画事件
        //初始化技能特效
        Dictionary<int, SpecialEffect> effectDic = Util.GetDic<MsgSpecialEffect, SpecialEffect>();
        if (dmgEffectId != 0)
        {
            dmgEffect = effectDic[dmgEffectId];
            dmgEffectName = Util.GetConfigString(dmgEffect.name);
        }
        if (castEffectId != 0)
        {
            castEffect = effectDic[castEffectId];
            castEffectName = Util.GetConfigString(castEffect.name);
        }
        if (flyEffectId != 0)
        {
            flyEffect = Util.GetDic<MsgFlyingEffect,FlyingEffect>()[flyEffectId];
            flyEffectName = Util.GetConfigString(flyEffect.name);
            flyEffectTrans = mineUnit.GetEffectPoint((EffectPoint)flyEffect.bone);
        }
        if (hitEffectId != 0)
        {
            hitEffect = effectDic[hitEffectId];
            hitEffectName = Util.GetConfigString(hitEffect.name);
        }
        StartCoroutine(LoadEffect());
    }

    IEnumerator LoadEffect()
    {
        if (!string.IsNullOrEmpty(dmgEffectName))
            yield return StartCoroutine(AssetManager.LoadAsset(dmgEffectName, AssetManager.AssetType.Effect, false));
        if (!string.IsNullOrEmpty(castEffectName))
            yield return StartCoroutine(AssetManager.LoadAsset(castEffectName, AssetManager.AssetType.Effect, false));
        if (!string.IsNullOrEmpty(flyEffectName))
            yield return StartCoroutine(AssetManager.LoadAsset(flyEffectName, AssetManager.AssetType.Effect, false));
        if (!string.IsNullOrEmpty(hitEffectName))
            yield return StartCoroutine(AssetManager.LoadAsset(hitEffectName, AssetManager.AssetType.Effect, false));
    }

    public void Use(FightUnit target)
    {
        if (targetUnit == null || targetUnit != target)
            targetUnit = target;
        DoSkill();
    }

    public virtual void Stop()
    {
        OnNone();
        StopAllCoroutines();
    }
    /// <summary>
    /// 打断正在前摇的技能
    /// </summary>
    public void StopBefore()
    {
        if(state == SkillState.None || state == SkillState.Before)
            StopAllCoroutines();
    }

    /// <summary>
    /// 展现魔法特效
    /// </summary>
    public virtual IEnumerator DisplaydmgEffect(FightUnit dmgEffectTarget)
    {
        if (dmgEffect != null && !string.IsNullOrEmpty(dmgEffectName))
        {
            EffectTarget effectTarget = (EffectTarget)dmgEffect.target;
            Transform dmgEffectTrans = null;
            Vector3 effectLocalPos = Vector3.zero;
            if (effectTarget == EffectTarget.Screen)
                dmgEffectTrans = FightEffectManager.instance.transform;
            else if(effectTarget == EffectTarget.MineCenter)
            {
                dmgEffectTrans = mineUnit.mTrans.parent;
                effectLocalPos = mineUnit.parentGroup.Center;
            }
            else if(effectTarget == EffectTarget.EnemyCenter)
            {
                dmgEffectTrans = mineUnit.mTrans.parent;
                effectLocalPos = targetUnit.parentGroup.Center;
            }
            else if(dmgEffectTarget != null)
                dmgEffectTrans = dmgEffectTarget.GetEffectPoint((EffectPoint)dmgEffect.bone);

            yield return StartCoroutine(AssetManager.LoadAsset(dmgEffectName, AssetManager.AssetType.Effect, false));
            GameObject obj = AssetManager.GetGameObject(dmgEffectName, dmgEffectTrans);
            obj.transform.localPosition = effectLocalPos;
        }
    }

    /// <summary>
    /// 展现施法特效
    /// </summary>
    public virtual IEnumerator DisplayPreEffect()
    {
        if (castEffect != null)
        {
            EffectTarget effectTarget = (EffectTarget)castEffect.target;
            if (effectTarget == EffectTarget.Enemy)
                castEffectTrans = targetUnit.GetEffectPoint((EffectPoint)castEffect.bone);
            else if (effectTarget == EffectTarget.Self)
                castEffectTrans = mineUnit.GetEffectPoint((EffectPoint)castEffect.bone);
            else if (effectTarget == EffectTarget.Screen)
                castEffectTrans = FightEffectManager.instance.transform;           
            if (!string.IsNullOrEmpty(castEffectName))
            {
                yield return StartCoroutine(AssetManager.LoadAsset(castEffectName, AssetManager.AssetType.Effect, false));
                GameObject obj = AssetManager.GetGameObject(castEffectName, castEffectTrans);
            }
        }
    }

    /// <summary>
    /// 展现飞行特效(挂接到飞行物下)
    /// </summary>
    public virtual IEnumerator DisplayFlyEffect(Transform point)
    {
        if (flyEffect != null)
        {
            if (!string.IsNullOrEmpty(flyEffectName))
            {
                yield return StartCoroutine(AssetManager.LoadAsset(flyEffectName, AssetManager.AssetType.Effect, false));
                if(point != null)
                {
                    GameObject obj = AssetManager.GetGameObject(flyEffectName, point);
                }
            }
        }
    }

    /// <summary>
    /// 展现受击特效
    /// </summary>
    public virtual IEnumerator DisplayHitEffect(FightUnit tg)
    {
        if (hitEffect != null)
        {
            Transform hitEffectTrans = tg.GetEffectPoint((EffectPoint)hitEffect.bone);
            Vector3 pos = hitEffectTrans.position;
            if (!string.IsNullOrEmpty(hitEffectName))
            {
                yield return StartCoroutine(AssetManager.LoadAsset(hitEffectName, AssetManager.AssetType.Effect, false));
                GameObject obj = AssetManager.GetGameObject(hitEffectName);
                obj.transform.parent = mineUnit.mTrans.parent;
                obj.transform.localScale = Vector3.one;
                obj.transform.position = pos;                
            }
        }
    }

    public virtual void BindAnim() { }

    public virtual void DoSkill() { OnBefore();/*-- 开启携程 --*/ }

    public virtual void OnBefore() { state = SkillState.Before; }
    public virtual void OnIng() { if (state == SkillState.Before) state = SkillState.Ing; }
    public virtual void OnAfter() { if (state == SkillState.Ing) state = SkillState.After; }
    public virtual void OnNone() 
    {
        state = SkillState.None; 
        if(mineUnit.attack.currentSkill == this) 
            mineUnit.anim.Play(Const.IdleAction, true);
    }
}
