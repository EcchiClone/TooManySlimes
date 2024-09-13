using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Weapon
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override IEnumerator WeaponRoutine()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateStat()
    {
        damage = (int)(30f * (100 + (float)elements[ElementType.Fire] * 30) / 100f); // base데미지는 30f
        delay = (int)(100f * (100 + (float)elements[ElementType.Wind] * 30) / 100f); // base속도는 100f
        amount = 1 + elements[ElementType.Water];
        dump = elements[ElementType.Earth];
    }

}
