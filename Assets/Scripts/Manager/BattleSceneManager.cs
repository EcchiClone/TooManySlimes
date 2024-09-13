using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Move, Stop, Pause, Clear, Fail }

public class BattleSceneManager : MonoBehaviour
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

    private float baseMapSpeed = 4f;
    public float mapSpeed = 4f;
    public GameObject fastEffect;

    public float spawnLinePivotY = 10.0f;

    public int nowLine = 0;
    public int maxLine = 450;

    private float blockSize = 0.9f;

    private bool isEnd;
    private bool fastNow;



    public GameState gameState;

    private Coroutine battleCoroutine;

    public void InitSetting()
    {
        isEnd = false;
        fastNow = false;
        mapSpeed = baseMapSpeed;
        pauseButton.onClick.AddListener(OpenPause);
        gameState = GameState.Move;
        battleCoroutine = StartCoroutine(UpdateOnBattleScene());
        ProgressDisplayUpdate();
    }
    IEnumerator UpdateOnBattleScene()
    {
        while(SceneManager.GetActiveScene().name == "BattleScene")
        {
            Debug.Log("UpdateOnBattleScene");
            switch (gameState)
            {
                case GameState.Move:
                    if(!isEnd)
                        MoveMonstersAndMap();
                    break;
                case GameState.Stop:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Clear:
                    break;
            }
            yield return null;
        }
    }

    // 하강 처리
    private void MoveMonstersAndMap()
    {
        if (gameState == GameState.Move || gameState == GameState.Clear)
        {
            // 맵 이동
            if(loopBackground != null)
            {
                foreach (GameObject image in loopBackground.LoopImages)
                    image.transform.Translate(Vector3.down * mapSpeed * Time.deltaTime);
            }


            // 몬스터 이동
            foreach (var monster in monsters)
            {
                if (monster)
                {
                    monster.transform.Translate(Vector3.down * mapSpeed * Time.deltaTime);
                }
            }

            // 기타 오브젝트 이동
            foreach (var entity in entities)
            {
                if (entity)
                {
                    entity.transform.Translate(Vector3.down * mapSpeed * Time.deltaTime);
                }
            }

            // 몬스터 생성용 피봇 이동
            spawnLinePivotY -= mapSpeed * Time.deltaTime;

            if (spawnLinePivotY < 8.0f)
                SpawnLine();
        }
    }
    void SpawnLine()
    {
        if(nowLine == 0)
        {
        }
        else if ((nowLine+2) % 150 == 0)
        {
            SpawnBoss();
        }
        else if ((nowLine + 2) % 15 == 0)
        {
            SpawnEventLine();
        }
        else
        {
            SpawnMonsters();
        }
        nowLine += 1;
        ProgressDisplayUpdate();
        if (nowLine >= 450) GameClear();
    }
    void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(0, spawnLinePivotY, 0);
        GameObject bossObj = Instantiate(bossPrefab, spawnPosition+Vector3.up * 5f, Quaternion.identity, monstersParent.transform);
        monsters.Add(bossObj.GetComponent<Monster>());

        spawnLinePivotY += blockSize*20;
    }
    void SpawnEventLine()
    {
        Vector3 spawnPosition = new Vector3(0, spawnLinePivotY, 0);
        GameObject newObj = Instantiate(eventLinePrefab, spawnPosition + Vector3.up * 0.9f, Quaternion.identity);
        entities.Add(newObj);

        spawnLinePivotY += blockSize*3;
    }
    void SpawnMonsters()
    {
        List<int> mosnterPosIdx;

        float genCase = Random.Range(0f, 1f);

        if (genCase < 0.04f) // 4% 확률로 한 줄에 다섯
            foreach (int i in new List<int> {0,1,2,3,4})
            {
                SpawnGeneralMonster(i, true);
            }
        else if (genCase < 0.1f) // 6% 확률로 한 줄에 넷
        {
            mosnterPosIdx = Utils.PickRandomNumbers(5, 4);
            foreach (int i in mosnterPosIdx)
            {
                SpawnGeneralMonster(i, true);
            }
        }
        else if (genCase < 0.5f) // 40% 확률로 랜덤 수 몬스터
        {
            mosnterPosIdx = Utils.PickRandomNumbers(5, (int)Random.Range(0f, 4f));
            foreach (int i in mosnterPosIdx)
            {
                SpawnGeneralMonster(i, true);
            }
        }

        spawnLinePivotY += blockSize;
    }
    void SpawnGeneralMonster(int i, bool canSpawnGemAndTrap)
    {
        if (canSpawnGemAndTrap)
        {
            float genCase = Random.Range(0f, 1f);
            if (genCase < 0.3f) // 30% 확률로 몬스터 대신 Gem
            {
                Vector3 spawnPosition = new Vector3(i * blockSize - 1.8f, spawnLinePivotY, 0);
                GameObject newGemObj = Instantiate(gemPrefab, spawnPosition, Quaternion.identity, gemParent.transform);
                entities.Add(newGemObj);
            }
            else if (genCase < 0.4f) // 10% 확률로 트랩
            {
                Vector3 spawnPosition = new Vector3(i * blockSize - 1.8f, spawnLinePivotY, 0);
                GameObject newTrapObj = Instantiate(trapPrefabs[Random.Range(0, trapPrefabs.Length)], spawnPosition, Quaternion.identity, gemParent.transform);
                entities.Add(newTrapObj);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(i * blockSize - 1.8f, spawnLinePivotY, 0);
                GameObject newMonsterObj = Instantiate(monsterPrefabs[Random.Range(0,monsterPrefabs.Length)], spawnPosition, Quaternion.identity, monstersParent.transform);
                Monster newMonster = newMonsterObj.GetComponent<Monster>();
                monsters.Add(newMonster);
            }
        }
        else
        {
            Vector3 spawnPosition = new Vector3(i * blockSize - 1.8f, spawnLinePivotY, 0);
            GameObject newMonsterObj = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], spawnPosition, Quaternion.identity, monstersParent.transform);
            Monster newMonster = newMonsterObj.GetComponent<Monster>();
            monsters.Add(newMonster);
        }
    }
    
    public void ProgressDisplayUpdate()
    {
        meterBar.fillAmount = (float)nowLine / (float)maxLine;
        meterText.text = $"{Mathf.Min(nowLine,maxLine)}m"; // 카운트는 maxLine까지로 강제

        // MeterBar의 높이를 기준으로 fillAmount 값에 맞춰 meterImage의 y 위치 설정
        RectTransform meterBarRect = meterBar.GetComponent<RectTransform>();
        RectTransform meterImageRect = meterImage.GetComponent<RectTransform>();

        float barHeight = meterBarRect.rect.height;
        float newY = barHeight * meterBar.fillAmount;

        meterImageRect.anchoredPosition = new Vector2(meterImageRect.anchoredPosition.x, newY);
    }

    public void OpenShop()
    {
        Game.Battle.gameState = GameState.Pause;
        shop.InitializeShop();
    }
    public void CloseShop()
    {
        Game.Battle.gameState = GameState.Move;
    }
    public void OpenPause()
    {
        Game.Battle.gameState = GameState.Pause;
        pause.InitializePause();
    }
    public void ClosePause()
    {
        Game.Battle.gameState = GameState.Move;
    }
    public void MoveToLobbyScene()
    {
        SceneManager.LoadScene(Game.Data.loadingSceneName);
        LoadingSceneManager.sceneToLoad = Game.Data.lobbySceneName;
    }
    public void GameClear()
    {
        isEnd = true;
        Game.Battle.gameState = GameState.Clear;
        StartCoroutine(DelayedShowResult());
    }
    public void GameFail()
    {
        isEnd = true;
        Game.Battle.gameState = GameState.Fail;
        StartCoroutine(DelayedShowResult());
    }
    private IEnumerator DelayedShowResult()
    {
        yield return new WaitForSeconds(1f);
        ShowResult();
    }
    public void ShowResult()
    {
        result.InitializeResult();
    }
    private Coroutine fastCoroutine;
    public void FasterTrap() // 새로운 함정
    {
        if(fastNow)
            StopCoroutine(fastCoroutine);
        fastCoroutine = StartCoroutine(FasterTrapRoutine());
    }
    IEnumerator FasterTrapRoutine()
    {
        fastNow = true;
        fastEffect.SetActive(true);
        mapSpeed = baseMapSpeed * 1.5f;
        float timer = 10f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        fastEffect.SetActive(false);
        float lerpDuration = 3f;
        float elapsedTime = 0f;
        float startSpeed = mapSpeed;
        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            mapSpeed = Mathf.Lerp(mapSpeed, baseMapSpeed, elapsedTime / lerpDuration);
            yield return null;
        }
        mapSpeed = baseMapSpeed;
        fastNow = false;
    }


}