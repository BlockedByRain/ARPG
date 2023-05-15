using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuffEffect/FlyBuff")]
public class FlyBuff : BuffEffect
{
    public float flyVelocity;
    public override void Apply(GameObject target)
    {
        target.transform.position+=Vector3.up* flyVelocity;
        target.transform.position += Vector3.forward * 1;
    }

    public override void Clear(GameObject target)
    {

    }
}
