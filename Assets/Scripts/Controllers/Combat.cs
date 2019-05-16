using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

[RequireComponent(typeof(Player))]
public class Combat : MonoBehaviour, IHealth
{
    public int health = 100;

    public Weapon currentWeapon;
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0;

    private Player player;
    private CameraLook cameraLook;

    void Awake()
    {
        player = GetComponent<Player>();
        cameraLook = GetComponent<CameraLook>();
    }


    // Start is called before the first frame update
    void Start()
    {
        weapons = GetComponentsInChildren<Weapon>().ToList();

        SelectWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWeapon)
        {
            bool fire1 = Input.GetButton("Fire1");
            if(fire1)
            {
                //Check if weapon can shoot
                if(currentWeapon.canShoot)
                {
                    currentWeapon.Attack();

                }

            }
        }
    }

    void DisableAllWeapons()
    {
        foreach (var item in weapons)
        {
            item.gameObject.SetActive(false);
        }
    }

    void SelectWeapon(int index)
    {
        //Check if index within bounds
        if(index >= 0 && index < weapons.Count)
        {
            //Diable all weapons
            DisableAllWeapons();
            //Selecte current weapon
            currentWeapon = weapons[index];
            //Enable current weapon
            currentWeapon.gameObject.SetActive(true);
            //Update current weapon index
            currentWeaponIndex = index;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            print("Dead");
        }

    }

    public void Heal(int heal)
    {
        health += heal;
    }
}
