using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Projectiles;

public class Gun : Weapon
{
    [Header("Variables")]
    public int maxReserve = 500, maxClip = 20;
    public float spread = 2f, recoil = 1f;

    [Header("References")]
    public Transform shotOrigin;
    public GameObject ProjectilePrefab;

    private int currentReserve = 0, currentClip = 0;

    private CameraLook camLook;

    private void Awake()
    {
        camLook = FindObjectOfType<CameraLook>();
    }

    public void Reload()
    {
        // Task:
        // - Ammo Math
        // - Auto-Reload when firing
        if (currentReserve > 0)
        {
            if (currentClip >= 0)
            {
                currentReserve += currentClip;
                currentClip = 0;

                if (currentReserve >= maxClip)
                {
                    currentReserve -= maxClip - currentClip;

                    currentClip = maxClip;
                }
                else if (currentReserve < maxClip)
                {
                    currentClip = currentReserve;
                    currentReserve -= currentReserve;
                }
            }
        }
    }

    public override void Attack()

    {
        // Reset timer & canShoot to false
        currentClip--;
        // Auto-Reload
        if (currentClip == 0)
        {
            Reload();
        }

        //Get some values
        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;
        Vector3 bulletOrigin = camTransform.position;
        Quaternion bulletRotation = camTransform.transform.rotation;
        Vector3 lineOrigin = shotOrigin.position;
        Vector3 direction = camTransform.forward;

        //Spawn bullet
        GameObject clone = Instantiate(ProjectilePrefab, lineOrigin, bulletRotation);
        Projectile projectile = clone.GetComponent<Projectile>();
        projectile.damage += damage;
        projectile.Fire(lineOrigin, direction);

        //Apply weapon recoil
        Vector3 euler = -Vector3.up * 2f;
        //Randomise the pitch
        euler.x = Random.Range(-1f, 1f);
        //Apply offset ot camera using weapon recoil
        camLook.SetTargetOffset(euler * recoil);

        base.Attack();
    }
}
