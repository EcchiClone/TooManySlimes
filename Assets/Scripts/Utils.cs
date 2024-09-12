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
        return item;
    }

}
