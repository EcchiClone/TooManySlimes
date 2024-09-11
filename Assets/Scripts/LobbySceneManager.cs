using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneManager : MonoBehaviour
{
    public string loadingSceneName = "LoadingScene";
    public string battleSceneName = "BattleScene";

    public Button GameStartButton;

    public Button HpUpButton;
    public Button HpDownButton;
    public Button DamageUpButton;
    public Button DamageDownButton;
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI DamageText;


    public void InitSetting()
    {
        GameStartButton.onClick.AddListener(GameStartButtonClicked);
        HpUpButton.onClick.AddListener(HpUpButtonClicked);
        HpDownButton.onClick.AddListener(HpDownButtonClicked);
        DamageUpButton.onClick.AddListener(DamageUpButtonClicked);
        DamageDownButton.onClick.AddListener(DamageDownButtonClicked);

        DisplayUpdate();
    }
    public void GameStartButtonClicked()
    {
        SceneManager.LoadScene(loadingSceneName);
        LoadingSceneManager.sceneToLoad = battleSceneName;
    }
    public void HpUpButtonClicked()
    {
        Game.Data.EditPlayerStat(PlayerStatOperator.HealthPlus, 10);
        DisplayUpdate();
    }
    public void HpDownButtonClicked()
    {
        Game.Data.EditPlayerStat(PlayerStatOperator.HealthMinus, 10);
        DisplayUpdate();
    }
    public void DamageUpButtonClicked()
    {
        Game.Data.EditPlayerStat(PlayerStatOperator.DamagePlus, 1);
        DisplayUpdate();
    }
    public void DamageDownButtonClicked()
    {
        Game.Data.EditPlayerStat(PlayerStatOperator.DamageMinus, 1);
        DisplayUpdate();
    }
    private void DisplayUpdate()
    {
        HpText.text = (Game.Data.playerStat.HealthBase + Game.Data.playerStat.HealthPlus).ToString();
        DamageText.text = (Game.Data.playerStat.DamageBase + Game.Data.playerStat.DamagePlus).ToString();
    }
}
