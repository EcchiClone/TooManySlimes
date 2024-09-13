using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public List<BattleItem> playerBattleItems;

    private HashSet<Collider2D> enemieCollisions = new HashSet<Collider2D>();

    public int maxHp;
    public int hp;
    public int damage;

    public int gem;

    public bool isInCombat = false;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    private float playerColliderY;

    private void Start()
    {
        maxHp = (int)((Game.Data.playerStat.HpBase + Game.Data.playerStat.HpPlus) * ((float)Game.Data.playerStat.HpPerc / 100f));
        damage = (int)((Game.Data.playerStat.DamageBase + Game.Data.playerStat.DamagePlus) * ((float)Game.Data.playerStat.DamagePerc / 100f));

        hp = maxHp;
        gem = 0;

        playerColliderY = GetComponent<CapsuleCollider2D>().size.y;

        playerWeapon.InitializeWeapons();
        playerBattleItems = new List<BattleItem>();

        UpdateDisplay();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            float collisionColY = collision.transform.GetComponent<BoxCollider2D>().size.y;
            float collisionPosY = collision.transform.position.y;
            float delta = (playerColliderY + collisionColY) / 2 - (collisionPosY - transform.position.y);
            if (delta < 0.1)
            {
                enemieCollisions.Add(collision);

                Game.Battle.gameState = GameState.Stop;
                Debug.Log("적 접촉, GameState: Stop");
            }
        }
        if (collision.transform.CompareTag("EventLine"))
        {
            if (transform.position.x < 0)
            {
                Debug.Log("왼쪽 선택");
            }
            else
            {
                Debug.Log("오른쪽 선택");
            }
        }
        if (collision.tag == "Gem")
        {
            if (collision.TryGetComponent(out DropedObject obj))
            {
                if (obj.canGet)
                {
                    AddGem(1);
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                AddGem(1);
                Destroy(collision.gameObject);
            }
        }
        if (collision.tag == "Trap")
        {
            Debug.Log("collision.tag trap");
            if (collision.TryGetComponent(out Trap trap))
            {
                Debug.Log("TryGetComponent");
                if (trap.Type==TrapType.Damager)
                {
                    Debug.Log("Damager");
                    TakeDamage(trap.Damage);
                }
                if (trap.Type == TrapType.Faster)
                {
                    Debug.Log("Faster");
                    Game.Battle.FasterTrap();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (!enemieCollisions.Contains(collision))
            {
                enemieCollisions.Add(collision);
            }
            if (collision.transform.GetComponent<Monster>().AttackDelay <= 0)
            {
                collision.transform.GetComponent<Monster>().PerformAttack();
                collision.transform.GetComponent<Monster>().TakeDamage(damage);
            }
        }
        if (collision.tag == "Gem")
        {
            if (collision.TryGetComponent(out DropedObject obj))
            {
                if (obj.canGet)
                {
                    AddGem(1);
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                AddGem(1);
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            enemieCollisions.Remove(collision);

            if (enemieCollisions.Count == 0)
            {
                Game.Battle.gameState = GameState.Move;  // 더 이상 적과 충돌이 없으면 Move 상태로 변경
                Debug.Log("접촉 적 없음, GameState: Move");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hpSlider.gameObject.SetActive(true);
        hp -= damage;
        UpdateDisplay();
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isInCombat = false;
        Game.Battle.GameFail();
        gameObject.SetActive(false);
    }

    public void Heal(int value)
    {
        hp = Mathf.Min(hp + value, maxHp);
        UpdateDisplay();
    }
    public void AddGem(int value)
    {
        gem += value;
        UpdateDisplay();
    }
    public void RemoveGem(int value)
    {
        gem -= value;
        UpdateDisplay();
    }
    public void GetBattleItem(BattleItem item)
    {
        if(item.Weapon!=WeaponType.None)
            playerWeapon.AddWeapon(item.Weapon, item.Element);
        playerBattleItems.Add(item);
    }
    void UpdateDisplay()
    {
        hpSlider.value = (float)hp / (float)maxHp;
        hpText.text = hp.ToString("F0");
        Game.Battle.gemText.text = gem.ToString();

    }
    private void Update()
    {
        // 계속 Move를 못하는 버그가 생겨 임시 작성
        if(Game.Battle.gameState == GameState.Stop)
        {
            if (enemieCollisions.Count == 0)
            {
                Game.Battle.gameState = GameState.Move;
                Debug.Log("Update문, 접촉 적 없음. GameState: Move");
            }
        }

    }
}