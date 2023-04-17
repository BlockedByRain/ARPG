using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuffEffect/SpeedBuff")]
public class SpeedBuff : BuffEffect
{
    public float amout;
    public override void Apply(GameObject target)
    {
        target.GetComponent<YbotController>().walkSpeed += amout;
    }

    public override void Clear(GameObject target)
    {
        
    }
}
