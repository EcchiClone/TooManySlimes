using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitializerLobbyScene : MonoBehaviour
{
    public Button GameStartButton;

    public Button HpUpButton;
    public Button HpDownButton;
    public Button DamageUpButton;
    public Button DamageDownButton;
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI DamageText;

    void Start()
    {
        initScene();
    }
    void initScene()
    {
        Game.Lobby.GameStartButton = GameStartButton;
        Game.Lobby.HpUpButton = HpUpButton;
        Game.Lobby.HpDownButton = HpDownButton;
        Game.Lobby.DamageUpButton = DamageUpButton;
        Game.Lobby.DamageDownButton = DamageDownButton;
        Game.Lobby.HpText = HpText;
        Game.Lobby.DamageText = DamageText;
        Game.Lobby.InitSetting();
    }
    private void OnDisable()
    {
        Game.Battle.CancelUpdate();
    }
}
