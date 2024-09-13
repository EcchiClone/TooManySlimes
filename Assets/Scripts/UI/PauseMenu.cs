using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public TextMeshProUGUI[] ItemCountText;

    public Button ToLobbyButton;
    public Button ResumeButton;

    public void InitializePause()
    {
        pauseCanvas.SetActive(true);
        SetCountTexts();
        SetOtherButtons();
    }
    void SetCountTexts()
    {
        Dictionary<WeaponType, int> weaponCount = new Dictionary<WeaponType, int>();
        Dictionary<ElementType, int> elementCount = new Dictionary<ElementType, int>();
        (weaponCount, elementCount) = Utils.CountAllBattleItemType(Game.Battle.player);
        ItemCountText[0].text = weaponCount[WeaponType.Sword].ToString();
        ItemCountText[1].text = weaponCount[WeaponType.Bow].ToString();
        ItemCountText[2].text = weaponCount[WeaponType.Gear].ToString();
        ItemCountText[3].text = weaponCount[WeaponType.Bird].ToString();
        ItemCountText[4].text = weaponCount[WeaponType.Laser].ToString();

        ItemCountText[5].text = elementCount[ElementType.Fire].ToString();
        ItemCountText[6].text = elementCount[ElementType.Water].ToString();
        ItemCountText[7].text = elementCount[ElementType.Wind].ToString();
        ItemCountText[8].text = elementCount[ElementType.Earth].ToString();
    }
    void SetOtherButtons()
    {
        ToLobbyButton.onClick.RemoveAllListeners();
        ToLobbyButton.onClick.AddListener(ToLobbyButtonClicked);
        ResumeButton.onClick.RemoveAllListeners();
        ResumeButton.onClick.AddListener(ResumeButtonClicked);
    }
    public void ToLobbyButtonClicked()
    {
        Game.Battle.MoveToLobbyScene();
    }
    public void ResumeButtonClicked()
    {
        Game.Battle.ClosePause();
        pauseCanvas.SetActive(false);
    }
}
