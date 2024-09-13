using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopItemPanel
{
    public BattleItem battleItem;

    public Button button;
    public Image outerImage;
    public Image innerImage;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
}

public class ShopMenu : MonoBehaviour
{
    public GameObject shopCanvas;
    public ShopItemPanel[] itemPanels;

    public Button ResumeButton;
    public Button RerollButton;

    public TextMeshProUGUI gemText;

    public void InitializeShop()
    {
        shopCanvas.SetActive(true);
        SetRandomItem();
        SetOtherButtons();
        UpdateGemText();
    }
    void UpdateGemText()
    {
        gemText.text = Game.Battle.player.gem.ToString();
    }
    void SetRandomItem()
    {
        for (int i = 0; i < itemPanels.Length; i++)
        {
            itemPanels[i].battleItem = Utils.PickRandomWeaponItem();

            itemPanels[i].outerImage.sprite = Resources.Load<Sprite>(itemPanels[i].battleItem.outerSpritePath);
            itemPanels[i].innerImage.sprite = Resources.Load<Sprite>(itemPanels[i].battleItem.innerSpritePath);
            itemPanels[i].outerImage.SetNativeSize();
            itemPanels[i].innerImage.SetNativeSize();
            itemPanels[i].priceText.text = itemPanels[i].battleItem.price.ToString();
            itemPanels[i].nameText.text = itemPanels[i].battleItem.Name;
            itemPanels[i].descText.text = itemPanels[i].battleItem.Description;

            // 손상된 패널 초기화
            itemPanels[i].button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            itemPanels[i].outerImage.color = new Color(1f, 1f, 1f, 1f);
            itemPanels[i].innerImage.color = new Color(1f, 1f, 1f, 1f);

            itemPanels[i].button.interactable = true;

            // 버튼 이벤트 추가
            int panelIndex = i;
            int price = itemPanels[i].battleItem.price;
            itemPanels[i].button.onClick.RemoveAllListeners();
            itemPanels[i].button.onClick.AddListener(() => {
                if (Game.Battle.player.gem >= price)
                {
                    // 구매, 패널 어둡게, 버튼 비활성화
                    Game.Battle.player.RemoveGem(price);
                    Game.Battle.player.GetBattleItem(itemPanels[panelIndex].battleItem);

                    itemPanels[panelIndex].button.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    itemPanels[panelIndex].outerImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    itemPanels[panelIndex].innerImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);

                    itemPanels[panelIndex].button.interactable = false;

                    UpdateGemText();
                }
                else
                {
                    Debug.Log("젬 부족"); // 보석이 부족할 경우 처리
                }
            });
        }
    }
    void SetOtherButtons()
    {
        ResumeButton.onClick.RemoveAllListeners();
        ResumeButton.onClick.AddListener(ResumeButtonClicked);
        RerollButton.onClick.RemoveAllListeners();
        RerollButton.onClick.AddListener(RerollButtonClicked);
    }

    public void ResumeButtonClicked()
    {
        Game.Battle.CloseShop();
        shopCanvas.SetActive(false);
    }
    public void RerollButtonClicked()
    {
        if (Game.Battle.player.gem < 2)
            return;
        Game.Battle.player.RemoveGem(2);
        SetRandomItem();
        UpdateGemText();
    }
}
