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


        //�����Ƕ��ж�
        GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.gameObject;

        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        Vector3 counterDir = attacker.transform.position - receiver.transform.position;
        
        float attackingAngle = Vector3.Angle(attacker.transform.forward, attackingDir);
        
        //�������泯����
        float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
        //�͹����߷���н�,Խ�ӽ�180˵��Խ���򹥻���
        float counterAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);

        //����and�ܷ���Ч�ж�
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
