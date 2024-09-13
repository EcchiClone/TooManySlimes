using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Weapon : MonoBehaviour
{
    public Dictionary<ElementType, int> elements;
    public string[] specialAbility = new string[0];

    public int base_damage;
    public float base_range;
    public float base_delay;
    public float base_speed;
    public int base_amount;
    public int base_dump;

    public int damage;
    public float range;
    public float delay;
    public float speed;
    public int amount;
    public int dump; // Earth 속성

    public bool isActive = false;
    Coroutine weaponRoutine;

    public virtual void Initialize()
    {
        elements = new Dictionary<ElementType, int>();

        foreach (ElementType element in System.Enum.GetValues(typeof(ElementType)))
        {
            elements[element] = 0;
            print("element");
        }

        setActive();
    }

    public void setActive()
    {
        if (elements.Values.Sum() + specialAbility.Length == 0)
        {
            isActive = false;
            if (weaponRoutine != null)
            {
                StopCoroutine(weaponRoutine);
                weaponRoutine = null;
            }
        }
        else
        {
            isActive = true;
            if (weaponRoutine == null)
            {
                weaponRoutine = StartCoroutine(WeaponRoutine());
            }
        }
    }
    public abstract void UpdateStat();
    public abstract IEnumerator WeaponRoutine();
    //public abstract void SpecialAttack();
}