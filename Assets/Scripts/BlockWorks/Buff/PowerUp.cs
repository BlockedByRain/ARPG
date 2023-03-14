using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public List<BuffEffect> effects = new List<BuffEffect>();

    private void OnCollisionEnter(Collision collision)
    {
        //检查触发者是否是玩家

        Destroy(gameObject);
        foreach (BuffEffect effect in effects)
        {
            effect.Apply(collision.gameObject);
        }

    }
}
