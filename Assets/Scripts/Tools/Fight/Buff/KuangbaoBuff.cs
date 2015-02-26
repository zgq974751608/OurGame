﻿using UnityEngine;
using System.Collections;

public class KuangbaoBuff : Buff {
    public override void BuffStart()
    {
        target.fightAttribute.allDamageRate += buffValue;
    }

    public override void BuffEnd()
    {
        target.fightAttribute.allDamageRate -= buffValue;
    }
}
