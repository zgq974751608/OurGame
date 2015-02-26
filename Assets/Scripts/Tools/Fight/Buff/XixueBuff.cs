using UnityEngine;
using System.Collections;

public class XixueBuff : Buff {

    public override void BuffStart()
    {
        target.OnNoramlAttackHit += OnNormalAttackHit;
    }

    public override void BuffEnd()
    {
        target.OnNoramlAttackHit -= OnNormalAttackHit;
    }

    void OnNormalAttackHit(params object[] objs)
    {
        float hurtNum = (int)objs[0];
        int xixueNum = Mathf.Max(1, Mathf.RoundToInt(hurtNum * buffValue));
        FightRule.XixueCure(xixueNum,target);
    }
}
