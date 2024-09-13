using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class Utils
{
    // K개의 숫자 중에서 N개의 숫자를 중복 없이 랜덤 선택
    public static List<int> PickRandomNumbers(int K, int N)
    {
        if (N > K)
        {
            throw new ArgumentException("N은 K보다 클 수 없음");
        }

        List<int> numbers = new List<int>();
        for (int i = 0; i < K; i++)
        {
            numbers.Add(i);
        }

        Random random = new Random();

        List<int> chosenNumbers = new List<int>();

        for (int i = 0; i < N; i++)
        {
            int randomIndex = random.Next(numbers.Count);
            chosenNumbers.Add(numbers[randomIndex]);
            numbers.RemoveAt(randomIndex);
        }

        return chosenNumbers;
    }

    public static BattleItem PickRandomWeaponItem()
    {
        BattleItem item = new BattleItem();

        WeaponType[] allWeaponType = (WeaponType[])Enum.GetValues(typeof(WeaponType));
        item.Weapon = allWeaponType[UnityEngine.Random.Range(1, allWeaponType.Length)];
        ElementType[] allElementType = (ElementType[])Enum.GetValues(typeof(ElementType));
        item.Element = allElementType[UnityEngine.Random.Range(1, allElementType.Length)];

        item.Name = $"{item.Element}의 {item.Weapon}";
        Dictionary<ElementType, string> map = new Dictionary<ElementType, string>() // 임시 작성
            {
                { ElementType.Fire, "데미지 증가" },
                { ElementType.Water, "갯수 증가" },
                { ElementType.Wind, "속도 증가" },
                { ElementType.Earth, "특수능력 추가" },
            };
        item.Description = map[item.Element];

        item.price = 5; // 임시 하드코딩 5원 고정

        item.outerSpritePath = $"Images/{item.Element.ToString().ToLower()}_outer"; // fire_outer, water_outer, ...
        item.innerSpritePath = $"Images/{item.Weapon.ToString().ToLower()}"; // sword, bow, ...

        return item;
    }

    public static (Dictionary<WeaponType,int>, Dictionary<ElementType, int>) CountAllBattleItemType(Player player)
    {
        Dictionary<WeaponType, int> weaponCount = new Dictionary<WeaponType, int>();
        Dictionary<ElementType, int> elementCount = new Dictionary<ElementType, int>();

        WeaponType[] allWeaponType = (WeaponType[])Enum.GetValues(typeof(WeaponType));
        ElementType[] allElementType = (ElementType[])Enum.GetValues(typeof(ElementType));

        for (int i = 1; i < allWeaponType.Length; i++)
            weaponCount[allWeaponType[i]] = 0;

        for (int j = 1; j < allElementType.Length; j++)
            elementCount[allElementType[j]] = 0;

        for (int i = 1; i < allWeaponType.Length; i++)
        {
            for (int j = 1; j < allElementType.Length; j++)
            {
                int value = player.playerWeapon.weapons[allWeaponType[i]].elements[allElementType[j]];
                weaponCount[allWeaponType[i]] += value;
                elementCount[allElementType[j]] += value;
            }
        }

        return (weaponCount, elementCount);
    }

}