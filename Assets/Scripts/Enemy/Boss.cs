using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Monster
{
    public int minGem;
    public int maxGem;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
    }

    public override void DropGems()
    {
        for (int i = 0; i < Random.Range(minGem, maxGem); i++)
        {
            GameObject newGem = Instantiate(gemPrefab, gameObject.transform.position, Quaternion.identity);
            newGem.AddComponent<DropedObject>();
        }
    }

}
