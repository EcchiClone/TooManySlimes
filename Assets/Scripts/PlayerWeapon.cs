using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Dictionary<WeaponType, Dictionary<ElementType, int>> weapons;
    public Dictionary<WeaponType, bool> weaponUsing;
    public Dictionary<WeaponType, WeaponStat> weaponStats;
    public Dictionary<WeaponType, GameObject> weaponPrefab;

    private Dictionary<WeaponType, Coroutine> activeWeaponCoroutines = new Dictionary<WeaponType, Coroutine>();

    public void InitializeWeapons()
    {
        weapons = new Dictionary<WeaponType, Dictionary<ElementType, int>>();
        weaponUsing = new Dictionary<WeaponType, bool>();
        weaponStats = new Dictionary<WeaponType, WeaponStat>();
        weaponPrefab = new Dictionary<WeaponType, GameObject>();

        foreach (WeaponType weapon in System.Enum.GetValues(typeof(WeaponType)))
        {
            weapons[weapon] = new Dictionary<ElementType, int>();

            foreach (ElementType element in System.Enum.GetValues(typeof(ElementType)))
            {
                weapons[weapon][element] = 0;
            }
            weaponUsing[weapon] = false;
            weaponStats[weapon] = new WeaponStat();
            weaponPrefab[weapon] = null;
        }
        weaponPrefab[WeaponType.Bow] = Resources.Load<GameObject>("Prefabs/Arrow");
    }
    public void AddWeapon(WeaponType weapon, ElementType element)
    {
        weapons[weapon][element] += 1;
        weaponUsing[weapon] = true;
        UpdateWeaponStat(weapon);

        // 무기 활성화
        if (!activeWeaponCoroutines.ContainsKey(weapon))
        {
            Coroutine weaponRoutine = StartCoroutine(WeaponRoutine(weapon));
            activeWeaponCoroutines[weapon] = weaponRoutine;
        }
    }
    public void RemoveWeapon(WeaponType weapon, ElementType element)
    {
        weapons[weapon][element] -= 1;
        if (weapons[weapon].Values.Sum() == 0)
        {
            weaponUsing[weapon] = false;

            // 무기 비활성화
            if (activeWeaponCoroutines.ContainsKey(weapon))
            {
                StopCoroutine(activeWeaponCoroutines[weapon]);
                activeWeaponCoroutines.Remove(weapon);
            }
        }
    }
    private void UpdateWeaponStat(WeaponType weapon)
    {
        weaponStats[weapon].damage = (int)(30f * (100 + (float)weapons[weapon][ElementType.Fire] * 30)/100f); // base데미지는 30f
        weaponStats[weapon].speed = (int)(100f * (100 + (float)weapons[weapon][ElementType.Wind] * 30) / 100f); // base속도는 100f
        weaponStats[weapon].amount = 1 + weapons[weapon][ElementType.Water];
        weaponStats[weapon].dump = weapons[weapon][ElementType.Earth];
    }
    private IEnumerator WeaponRoutine(WeaponType weapon)
    {
        while (weaponUsing[weapon])
        {
            UseWeapon(weapon);
            yield return new WaitForSeconds(100f / weaponStats[weapon].speed); // 무기의 속도에 따라 주기적으로 호출
        }
    }

    private void Update()
    {
        //foreach(WeaponType weapon in System.Enum.GetValues(typeof(WeaponType)))
        //{
        //    if (weaponUsing[weapon])
        //    {
        //        UseWeapon(weapon);
        //    }
        //}
    }

    private void UseWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Sword:
                break;
            case WeaponType.Bow:
                ShootArrow();
                break;
            case WeaponType.Gear:
                break;
            case WeaponType.Bird:
                break;
            case WeaponType.Laser:
                break;

        }
    }
    private void ShootArrow()
    {
        for (int i = 0; i < weaponStats[WeaponType.Bow].amount; i++) // amount에 따라 여러 개 발사
        {
            GameObject arrow = Instantiate(weaponPrefab[WeaponType.Bow], Game.Battle.player.transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
        }
    }

}


public class WeaponStat
{
    public int damage;
    public int speed;
    public int amount;
    public int dump;
}