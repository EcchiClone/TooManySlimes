using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Dictionary<WeaponType, Weapon> weapons;

    public void InitializeWeapons()
    {
        weapons = new Dictionary<WeaponType, Weapon>(); // 각 무기의 관리자
        weapons[WeaponType.Sword] = gameObject.AddComponent<Sword>();
        weapons[WeaponType.Bow] = gameObject.AddComponent<Bow>();
        weapons[WeaponType.Gear] = gameObject.AddComponent<Gear>();
        weapons[WeaponType.Bird] = gameObject.AddComponent<Bird>();
        weapons[WeaponType.Laser] = gameObject.AddComponent<Laser>();

        foreach (var weapon in weapons.Values)
        {
            weapon.Initialize();
        }
    }
    public void AddWeapon(WeaponType weapon, ElementType element)
    {
        weapons[weapon].elements[element] += 1;
        weapons[weapon].UpdateStat();
        weapons[weapon].setActive();
    }
    public void RemoveWeapon(WeaponType weapon, ElementType element)
    {
        weapons[weapon].elements[element] -= 1;
        weapons[weapon].UpdateStat();
        weapons[weapon].setActive();
    }
}

