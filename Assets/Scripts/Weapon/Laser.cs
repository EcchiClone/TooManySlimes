using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override IEnumerator WeaponRoutine()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(delay);
        }
    }

    public override void UpdateStat()
    {
        damage = (int)(30f * (100 + (float)elements[ElementType.Fire] * 30) / 100f);
        delay = (int)(100f * (100 + (float)elements[ElementType.Wind] * 30) / 100f);
        amount = 1 + elements[ElementType.Water];
        dump = elements[ElementType.Earth];
    }

}
