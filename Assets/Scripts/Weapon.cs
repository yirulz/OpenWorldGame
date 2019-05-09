using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Stats")]
    public int damage = 10;
    public int maxReserve = 500, maxClip = 20;
    public float spread = 2f, recoil = 1f, range = 10f, shootRate = .2f;

    [Header("References")]
    public Transform shotOrigin;
    public GameObject bulletPrefab;

    [HideInInspector] public bool canShoot = false;

    private float shootTimer = 0f;
    private int currentReserve = 0, currentClip = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentReserve = maxReserve;
        currentClip = maxClip;

    }

    // Update is called once per frame
    void Update()
    {
        // Count up shoot timer
        shootTimer += Time.deltaTime;
        //If timer is less than shoot rate
        if (shootTimer >= shootRate)
        {
            //Player can shoot
            canShoot = true;
        }
    }

    public void Reload()
    {
        //Task:
        // - Ammo math
        // - Auto-reload when firing
        if (currentReserve > 0)
        {
            if (currentReserve >= maxClip)
            {
                currentReserve -= maxClip - currentClip;
                currentClip = maxClip;
            }
            if (currentClip < maxClip)
            {
                // Note (Manny): Look into this
                int tempMag = currentReserve;
                currentClip = tempMag;
                currentReserve -= tempMag;
            }
        }
    }

    public void Shoot()
    {
        //Reset timer & can shoot to false
        shootTimer = 0f;
        canShoot = false;

        //Get some values
        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;
        Vector3 bulletOrigin = camTransform.position;
        Quaternion bulletRotation = camTransform.transform.rotation;
        Vector3 lineOrigin = shotOrigin.position;
        Vector3 direction = camTransform.forward;

        //Spawn bullet
        GameObject clone = Instantiate(bulletPrefab, bulletOrigin, bulletRotation);
        Bullet bullet = clone.GetComponent<Bullet>();
        bullet.Fire(lineOrigin, direction);
    }
}
