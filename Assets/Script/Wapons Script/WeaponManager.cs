using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    WaponsHandeler[] weapons;

    private int current_weapon_index;

    void Start()
    {
        current_weapon_index = 0;

        weapons[current_weapon_index].gameObject.SetActive(true);
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapons(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapons(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapons(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapons(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapons(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapons(5);
        }

    }

    void TurnOnSelectedWeapons(int weaponIndex)
    {
        if (current_weapon_index == weaponIndex)
            return;

        weapons[current_weapon_index].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        current_weapon_index = weaponIndex;
    }

    public WaponsHandeler GetCurrrentSelectedWeapon()
    {
        return weapons[current_weapon_index];
    }
}
