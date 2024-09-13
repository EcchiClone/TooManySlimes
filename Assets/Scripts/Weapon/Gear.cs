using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : Weapon
{
    GameObject gearPrefab;

    public List<GameObject> gears = new List<GameObject>();

    private float base_radius;
    private float radius;

    public override void Initialize()
    {
        base.Initialize();

        gearPrefab = Resources.Load<GameObject>("Prefabs/Gear");

        base_damage = 30;
        base_speed = 0.5f;
        base_amount = 1;
        base_radius = 1f;
        radius = 1f;
    }

    private void UpdateGears()
    {
        while (gears.Count < amount) // 기어가 부족하면 추가
        {
            GameObject gear = Instantiate(gearPrefab, transform.position, Quaternion.identity);
            gears.Add(gear);
        }

        while (gears.Count > amount) // 기어가 많으면 제거
        {
            GameObject gearToRemove = gears[gears.Count - 1];
            Destroy(gearToRemove);
            gears.RemoveAt(gears.Count - 1);
        }
        foreach (GameObject gear in gears)
        {
            gear.GetComponent<Projectile>().damage = damage;
        }
    }

    public override IEnumerator WeaponRoutine()
    {
        while (isActive)
        {
            if (Game.Battle.gameState == GameState.Move || Game.Battle.gameState == GameState.Stop)
            {
                MoveGear();
            }

            yield return null;
        }
    }
    void MoveGear()
    {
        float angleStep = 360f / amount;
        float currentAngle = ( ( (Time.time * speed) % 360) * 360) % 360;
        for(int i = 0; i < gears.Count; i++)
        {
            Vector3 newGearIPosition = Game.Battle.player.transform.position + new Vector3(
                Mathf.Cos((currentAngle + angleStep * i) * Mathf.Deg2Rad) * radius,
                Mathf.Sin((currentAngle + angleStep * i) * Mathf.Deg2Rad) * radius,
                0f
            );
            gears[i].GetComponent<Rigidbody2D>().MovePosition(newGearIPosition);
        }
    }

    public override void UpdateStat()
    {
        damage = (int)(base_damage * (100 + elements[ElementType.Fire] * 30f) / 100f);
        speed = base_speed + elements[ElementType.Wind] * 0.25f;
        amount = base_amount + elements[ElementType.Water];
        radius = Mathf.Min((base_radius + elements[ElementType.Earth]*0.2f),2f);

        UpdateGears();
    }
}
