using System.Collections;
using UnityEngine;

public class Bow : Weapon
{
    GameObject arrowPrefab;

    public override void Initialize()
    {
        base.Initialize();
        arrowPrefab = Resources.Load<GameObject>("Prefabs/Arrow");
        base_damage = 30;
        base_delay = 1;
        base_amount = 1;
    }

    public override IEnumerator WeaponRoutine()
    {
        while(isActive)
        {
            if(Game.Battle.gameState == GameState.Move || Game.Battle.gameState == GameState.Stop)
            {
                float angleStep = 5f; // 화살끼리의 각도 차이 (하드코딩 5도)
                float startAngle = -(amount - 1) * angleStep / 2; // 첫 번째 화살

                for (int i = 0; i < amount; i++)
                {
                    float currentAngle = startAngle + i * angleStep;
                    Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * Vector2.up;

                    GameObject arrow = Instantiate(arrowPrefab, Game.Battle.player.transform.position, Quaternion.identity);
                    arrow.GetComponent<Projectile>().damage = damage;
                    arrow.GetComponent<Rigidbody2D>().velocity = direction * 10f;
                }
            }

            yield return new WaitForSeconds(delay);
        }
    }

    public override void UpdateStat()
    {
        damage = (int)(base_damage * (100 + (float)elements[ElementType.Fire] * 30) / 100f);
        delay = base_delay / ( (100 + (float)elements[ElementType.Wind] * 30) / 100f );
        amount = base_amount + elements[ElementType.Water];
    }
}
