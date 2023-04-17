using UnityEngine;

[CreateAssetMenu(menuName = "Ability/DashAbility")]
public class DashAbility : Ability
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        YbotController yb=parent.GetComponent<YbotController>();
        parent.transform.position+=Vector3.up*dashVelocity;
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
