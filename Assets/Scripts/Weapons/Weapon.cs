using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Projectiles;

public abstract class Weapon : MonoBehaviour
{
    [Header("Stats")]
    public int damage = 10;
    public float range = 10f, attackRate = .2f;
    private float attackTimer = 50f;



    [HideInInspector] public bool canShoot = false;

    void Update()
    {
        // Count up shoot timer
        attackTimer += Time.deltaTime;
        // If shoot timer reaches shoot rate
        if (attackTimer >= 1f / attackRate)
        {
            // Can shoot!
            canShoot = true;
        }
    }


public virtual void Attack()
{
    //Reset attack timer
    attackTimer = 0f;
    //Reset can shoot
    canShoot = false;
}
    
}
