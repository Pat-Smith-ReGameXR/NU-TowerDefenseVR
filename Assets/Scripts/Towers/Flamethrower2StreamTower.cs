using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower2StreamTower : TowerBehaviour
{
    [SerializeField] float rotateSpeed;

    [SerializeField] Transform stream1Pivot;
    [SerializeField] Transform stream2Pivot;
    [SerializeField] float rotate2Speed;

    protected IDamageMethod secondDamageMethodClass;

    protected override void Start()
    {
        base.Start();

        IDamageMethod[] damageMethodClasses = GetComponents<IDamageMethod>();

        currentDamageMethodClass = damageMethodClasses[0];
        secondDamageMethodClass = damageMethodClasses[1];

        if (secondDamageMethodClass == null)
        {
            Debug.LogError("Tower " + this.gameObject.name + " has no SECOND damage class attached!");
        }
        else
        {
            secondDamageMethodClass.Init(damage, firerate);
        }
    }

    public override void Tick()
    {
        if (healthBar != null && shieldBar != null)
        {
            healthBar.value = health;
            shieldBar.value = shield;
        }

        currentDamageMethodClass.DamageTick(target);
        secondDamageMethodClass.DamageTick(target);

        if (towerPivot != null)
        {
            if (target != null)
            {
                Vector3 posDifference = target.transform.position - transform.position;
                posDifference.y = 0;

                float pingpong = Mathf.PingPong(Time.time * rotateSpeed * 5f, 90) - 45;
                stream1Pivot.transform.rotation = Quaternion.LookRotation(posDifference, Vector3.up) * Quaternion.Euler(0f, pingpong, 0f);

                float pingpong2 = Mathf.PingPong(Time.time * rotate2Speed * 5f, 90) - 45;
                stream2Pivot.transform.rotation = Quaternion.LookRotation(posDifference, Vector3.up) * Quaternion.Euler(0f, pingpong2, 0f);
            }
        }
    }
}
