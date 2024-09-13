using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public TrapType Type = TrapType.Damager;
    public int Damage = 0;
}
public enum TrapType
{
    Damager,
    Faster,
}