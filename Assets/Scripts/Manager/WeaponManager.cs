using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{

    private Collider weaponColL;
    private Collider weaponColR;
    public GameObject whL;
    public GameObject whR;
    public WeaponController wcL;
    public WeaponController wcR;

    private void Start()
    {
        whL = transform.DeepFind("weaponHandleL").gameObject;
        whR = transform.DeepFind("weaponHandleR").gameObject;
        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();

        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);
    
    
    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc= targetObj.GetComponent<WeaponController>();
        if (tempWc==null)
        {
            tempWc=targetObj.AddComponent<WeaponController>();
        }
        tempWc.wm = this;

        return tempWc;
    }



    public void WeaponEnable()
    {
        //print("weapon enable");
        if (am.yc.CheckStateTag("attackL"))
        {
            weaponColL.enabled = true;
        }
        else
        {
            weaponColR.enabled = true;
        }

    }

    public void WeaponDisable()
    {
        //print("weapon disable");
        weaponColL.enabled = false;
        weaponColR.enabled = false;
    }

}
