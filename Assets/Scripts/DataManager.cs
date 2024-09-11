using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public PlayerStat playerStat;
    public MonsterStat slimeStat;

    private void Start()
    {
        InitPlayerStat();
    }

    private void InitPlayerStat()
    {
        playerStat = new PlayerStat();
    }

    public void EditPlayerStat(PlayerStatOperator op, int value)
    {
        switch (op)
        {
            case (PlayerStatOperator.HealthPlus):
                playerStat.HealthPlus += value;
                break;
            case (PlayerStatOperator.HealthMinus):
                playerStat.HealthPlus -= value;
                break;
            case (PlayerStatOperator.HealthPercPlus):
                playerStat.HealthPerc += value;
                break;
            case (PlayerStatOperator.HealthPercMinus):
                playerStat.HealthPerc -= value;
                break;
            case (PlayerStatOperator.DamagePlus):
                playerStat.DamagePlus += value;
                break;
            case (PlayerStatOperator.DamageMinus):
                playerStat.DamagePlus -= value;
                break;
            case (PlayerStatOperator.DamagePercPlus):
                playerStat.DamagePerc += value;
                break;
            case (PlayerStatOperator.DamagePercMinus):
                playerStat.DamagePerc -= value;
                break;
        }
    }
}

public enum PlayerStatOperator
{
    HealthPlus,
    HealthMinus,
    HealthPercPlus,
    HealthPercMinus,
    DamagePlus,
    DamageMinus,
    DamagePercPlus,
    DamagePercMinus,
}
public class PlayerStat
{
    public int HealthBase { get; set; }
    public int DamageBase { get; set; }
    public int HealthPlus { get; set; }
    public int DamagePlus { get; set; }
    public int HealthPerc { get; set; }
    public int DamagePerc { get; set; }
}
public class PlayerDynamicStat
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int Gem { get; set; }
    //public Dictionary<string, Dictionary<string, int>> Skills { get; set; } // { 이름: { 'level':int, 'sp1':false, 'sp2':false, 'sp3':false } }
    public SkillDynamic SkillDynamic { get; set; }
}

public class SkillData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int DamageBase { get; set; }
    public int DamagePlus { get; set; }
    public int DamagePerc { get; set; }
}

public class SkillDynamic
{
    public int Level { get; set; }
    public bool[] Sp { get; set; }
}

public class MonsterStat
{
    public int HealthBase { get; set; }
    public int DamageBase { get; set; }
}