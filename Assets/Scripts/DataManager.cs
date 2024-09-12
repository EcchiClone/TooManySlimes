using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public PlayerStat playerStat;
    public MonsterStat slimeStat;
    public List<string> specialSkills;

    public void InitSetting()
    {
        InitPlayerStat();
        specialSkills = new List<string>()
        {
            "스페셜1",
            "스페셜2",
            "스페셜3",
            "스페셜4",
        };
    }

    private void InitPlayerStat()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerStat.json");

        // 파일 존재 여부
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                playerStat = JsonConvert.DeserializeObject<PlayerStat>(json);
                Debug.Log("PlayerStat 로드");
            }
            catch { InitNewPlayer(); }
        }
        else InitNewPlayer();
    }
    private void InitNewPlayer()
    {
        playerStat = new PlayerStat()
        {
            HpBase = 100,
            DamageBase = 50,
            HpPlus = 0,
            DamagePlus = 0,
            HpPerc = 100,
            DamagePerc = 100
        };
        Debug.Log("PlayerStat 초기화");
        SavePlayerStat();
    }

    public void SavePlayerStat()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerStat.json");

        string json = JsonConvert.SerializeObject(playerStat, Formatting.Indented);

        File.WriteAllText(path, json);
        Debug.Log($"PlayerStat의 JSON 저장 : [{path}]");
    }

    public void EditPlayerStat(PlayerStatOperator op, int value)
    {
        switch (op)
        {
            case (PlayerStatOperator.HealthPlus):
                playerStat.HpPlus += value;
                break;
            case (PlayerStatOperator.HealthMinus):
                playerStat.HpPlus -= value;
                break;
            case (PlayerStatOperator.HealthPercPlus):
                playerStat.HpPerc += value;
                break;
            case (PlayerStatOperator.HealthPercMinus):
                playerStat.HpPerc -= value;
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
        SavePlayerStat();
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
[Serializable]
public class PlayerStat
{
    public int HpBase;
    public int DamageBase;
    public int HpPlus;
    public int DamagePlus;
    public int HpPerc;
    public int DamagePerc;
}
[Serializable]
public class PlayerDynamicStat
{
    public int MaxHealth;
    public int Health;
    public int Gem;
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

public class BattleItem
{
    public string Name;
    public string Description;

    public ElementType Element;
    public WeaponType Weapon;

    public int price;
}

public enum ElementType
{
    None,
    Fire,
    Water,
    Wind,
    Earth,
}
public enum WeaponType
{
    None,
    Sword,
    Bow,
    Gear,
    Bird,
    Laser,
}
public enum EventLineType
{
    BattleItem,
    Shop,
    Event,
    Potion,
    Gem,
}
