using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public int range;
    public int delay;
    public int amount;

    public abstract void AttackLoop();
    public abstract void SpecialAttack();
}