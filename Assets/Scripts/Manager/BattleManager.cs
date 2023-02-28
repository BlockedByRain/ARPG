using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    private CapsuleCollider defCol;

    // Start is called before the first frame update
    void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up*1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        //print(col.name);
        WeaponController targetWc =col.GetComponentInParent<WeaponController>();


        //攻击角度判定
        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.gameObject;

        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - receiver.transform.position;
        
        float attackingAngle = Vector3.Angle(attacker.transform.forward, attackingDir);
        
        //防御者面朝方向
        float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
        //和攻击者方向夹角,越接近180说明越朝向攻击者
        float counterAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);

        //攻击and盾反有效判断
        bool attackValid = (attackingAngle < 45);
        bool counterValid = (counterAngle1 < 30 && Mathf.Abs(counterAngle2 - 180) < 30);

        if (col.tag=="Weapon")
        {
            if (attackingAngle<=45)
            {
                am.TryDoDamage(targetWc,attackValid,counterValid);
            }
        }
    }
}
