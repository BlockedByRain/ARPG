using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public YbotController yc;

    [Header("-----Auto Generate if Null-----")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InteractionManager im;
    public EventCasterManager ecm;

    private void Awake()
    {
        yc = GetComponent<YbotController>();

        GameObject model = yc.model;
        GameObject sensor = transform.Find("sensor").gameObject;

        bm = Bind<BattleManager>(sensor);

        wm = Bind<WeaponManager>(model);

        sm = Bind<StateManager>(gameObject);

        dm = Bind<DirectorManager>(gameObject);

        im = Bind<InteractionManager>(sensor);

        ecm= Bind<EventCasterManager>(model);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if (tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;

        return tempInstance;
    }

    public void TryDoDamage(WeaponController targetWc,bool attackValid,bool counterValid)
    {   
        if (sm.isCounterBackSuccess)
        {
            if (counterValid)
            {
                targetWc.wm.am.Stunned();
            }
        }
        else if(sm.isCounterBackFailure){
            if (attackValid)
            {
                HitOrDie(true);
            }
        }
        else if (sm.isImmortal)
        {
            //do nothing
        }
        else if (sm.isBlocked)
        {

            Blocked();
        }
        else
        {
            if (attackValid)
            {
                HitOrDie(true);
            }
        }


    }

    public void Hit()
    {
        yc.IssueTrigger("Hit");
    }

    public void Stunned()
    {
        yc.IssueTrigger("Stunned");
    }

    public void HitOrDie(bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {

        }
        else
        {
            sm.AddHP(-5);
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                }
            }
            else
            {
                Die();
            }
        }
    }

    public void Blocked()
    {
        yc.IssueTrigger("Blocked");
    }

    public void Die()
    {
        yc.IssueTrigger("Die");
        yc.pi.InputEnabled = false;
        if (yc.camctl.lockState==true)
        {
            yc.camctl.SwitchLock();
        }
        yc.camctl.enabled = false;
    }


    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    public void LockUnlockActorController(bool value)
    {
        yc.SetBool("Lock", value);
    }



}
