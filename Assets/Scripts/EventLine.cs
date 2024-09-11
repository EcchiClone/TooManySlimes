using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EventLine : MonoBehaviour
{
    public EventLineType[] eventElements;
    public SpriteRenderer[] itemSprites;
    public SpriteRenderer[] weaponSprites;
    public TextMeshProUGUI[] itemTexts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            Debug.Log("오오..");
    }

    private void OnEnable()
    {
        EventLineType[] allTypes = (EventLineType[])Enum.GetValues(typeof(EventLineType));
        eventElements = allTypes.OrderBy(x => Guid.NewGuid()).Take(2).ToArray();
        for (int i=0;i<eventElements.Length; i++)
        {
            switch (eventElements[i])
            {
                case EventLineType.BattleItem:
                    InitBattleItem(i);
                    break;
                case EventLineType.Shop:
                    InitShop(i);
                    break;
                case EventLineType.Event:
                    InitEvent(i);
                    break;
                case EventLineType.Potion:
                    InitPotion(i);
                    break;
                case EventLineType.Gem:
                    InitGem(i);
                    break;
            }
        }
    }
    private void InitBattleItem(int i)
    {
        string spritePath = "Images/BattleItemExample";
        itemSprites[i].sprite = Resources.Load<Sprite>(spritePath);
        itemTexts[i].text = "BattleItem";
    }
    private void InitShop(int i)
    {
        string spritePath = "Images/EventLineShop";
        itemSprites[i].sprite = Resources.Load<Sprite>(spritePath);
        itemTexts[i].text = "상점";
    }
    private void InitEvent(int i)
    {
        string spritePath = "Images/EventLineEvent";
        itemSprites[i].sprite = Resources.Load<Sprite>(spritePath);
        itemTexts[i].text = "???";
    }
    private void InitPotion(int i)
    {
        string spritePath = "Images/EventLinePotion";
        itemSprites[i].sprite = Resources.Load<Sprite>(spritePath);
        itemTexts[i].text = "회복 100";
    }
    private void InitGem(int i)
    {
        string spritePath = "Images/EventLineGem";
        itemSprites[i].sprite = Resources.Load<Sprite>(spritePath);
        itemTexts[i].text = "젬 15";
    }

    public void EventLineTouch(int i)
    {
        switch (eventElements[i])
        {
            case EventLineType.BattleItem:
                SelectBattleItem();
                break;
            case EventLineType.Shop:
                SelectShop();
                break;
            case EventLineType.Event:
                SelectEvent();
                break;
            case EventLineType.Potion:
                SelectPotion();
                break;
            case EventLineType.Gem:
                SelectGem();
                break;
        }
    }
    private void SelectBattleItem()
    {
        Debug.Log("SelectBattleItem");
    }
    private void SelectShop()
    {
        Debug.Log("SelectShop");
    }
    private void SelectEvent()
    {
        Debug.Log("SelectEvent");
    }
    private void SelectPotion()
    {
        Debug.Log("SelectPotion");
        Game.Battle.player.Heal(100);
    }
    private void SelectGem()
    {
        Debug.Log("SelectGem");
    }

    private void Update()
    {
        if (transform.position.y < -8f)
        {
            Game.Battle.entities.Remove(gameObject);
            Disappear();
        }
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }
}
