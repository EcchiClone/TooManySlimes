using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitializerBattleScene : MonoBehaviour
{
    public Player player;

    public List<Monster> monsters;
    public List<GameObject> entities;
    public LoopBackground loopBackground;

    public GameObject[] monsterPrefabs;
    public GameObject monstersParent;
    public GameObject eventLinePrefab;
    public GameObject[] trapPrefabs;

    public GameObject bossPrefab;

    public GameObject gemPrefab;
    public GameObject gemParent;

    public GameObject meterImage;
    public TextMeshProUGUI meterText;
    public Image meterBar;
    public TextMeshProUGUI gemText;

    public ShopMenu shop;
    public PauseMenu pause;
    public ResultMenu result;

    public Button pauseButton;
    public GameObject fastEffect;

    void Awake()
    {
        initScene();
    }
    void initScene()
    {
        // 더 나은 방법 고민중 ; v ;
        Game.Battle.player = player;
        Game.Battle.monsters = monsters;
        Game.Battle.entities = entities;
        Game.Battle.loopBackground = loopBackground;
        Game.Battle.monsterPrefabs = monsterPrefabs;
        Game.Battle.monstersParent = monstersParent;
        Game.Battle.eventLinePrefab = eventLinePrefab;
        Game.Battle.trapPrefabs = trapPrefabs;
        Game.Battle.bossPrefab = bossPrefab;
        Game.Battle.gemPrefab = gemPrefab;
        Game.Battle.gemParent = gemParent;
        Game.Battle.meterImage = meterImage;
        Game.Battle.meterText = meterText;
        Game.Battle.meterBar = meterBar;
        Game.Battle.gemText = gemText;
        Game.Battle.shop = shop;
        Game.Battle.pause = pause;
        Game.Battle.result = result;
        Game.Battle.pauseButton = pauseButton;
        Game.Battle.fastEffect = fastEffect;
        Game.Battle.mapSpeed = 4f;
        Game.Battle.spawnLinePivotY = 10.0f;
        Game.Battle.nowLine = 0;
        Game.Battle.InitSetting();
    }
    private void OnDisable()
    {
        // 제대로 작동하지 않아 주석처리
        //Game.Battle.CancelUpdate();
    }

}
