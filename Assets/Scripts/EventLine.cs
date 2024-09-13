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
    public BattleItem lineBattleItem;


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
        lineBattleItem = Utils.PickRandomWeaponItem();
        itemSprites[i].sprite = Resources.Load<Sprite>(lineBattleItem.outerSpritePath);
        weaponSprites[i].sprite = Resources.Load<Sprite>(lineBattleItem.innerSpritePath);
        itemTexts[i].text = $"{lineBattleItem.Element} {lineBattleItem.Weapon}";
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
        itemSprites[i].sprite = null;
        weaponSprites[i].sprite = null;
        itemTexts[i].text = "";

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
        Game.Battle.player.GetBattleItem(lineBattleItem);
        Debug.Log("SelectBattleItem");
    }
    private void SelectShop()
    {
        Debug.Log("SelectShop");
        Game.Battle.OpenShop();
    }
    private void SelectEvent()
    {
        Debug.Log("SelectEvent");
    }
    private void SelectPotion()
    {
        Game.Battle.player.Heal(100);
    }
    private void SelectGem()
    {
        Game.Battle.player.AddGem(15);
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
