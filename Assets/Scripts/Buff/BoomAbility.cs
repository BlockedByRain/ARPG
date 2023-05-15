using UnityEngine;

[CreateAssetMenu(menuName = "Ability/BoomAbility")]
public class BoomAbility : Ability
{
    private Vector3 pos;
    public float radius = 6.0f;
    public float firepower = 12;
    public float upwardpower = 7;

    public ForceMode boomForcemode = ForceMode.Impulse;

    public override void Activate(GameObject parent)
    {
        pos = parent.transform.position;

        Debug.Log("Boom");
        Collider[] colliders = Physics.OverlapSphere(pos, radius);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == "Player")
            {
                break;
            }

            if (col.GetComponent<Rigidbody>())
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(firepower, pos, radius, upwardpower, boomForcemode);
            }
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}

