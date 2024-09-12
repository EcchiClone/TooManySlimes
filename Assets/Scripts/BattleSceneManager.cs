using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Move, Stop, Event, Clear }

public class BattleSceneManager : MonoBehaviour
{
    public Player player;

    public List<Monster> monsters;
    public List<GameObject> entities;
    public LoopBackground loopBackground;

    public GameObject monsterPrefab;
    public GameObject monstersParent;
    public GameObject eventLinePrefab;

    public GameObject gemPrefab;
    public GameObject gemParent;

    public GameObject meterImage;
    public TextMeshProUGUI meterText;
    public Image meterBar;
    public TextMeshProUGUI gemText;

    public float mapSpeed = 4f;

    public float spawnLinePivotY = 10.0f;

    public int nowLine = 0;

    private float blockSize = 0.9f;


    public GameState gameState;

    public void InitSetting()
    {
        gameState = GameState.Move;
        StartCoroutine(UpdateOnBattleScene());
        ProgressDisplayUpdate();
    }
    public void CancelUpdate()
    {
        StopCoroutine(UpdateOnBattleScene());
    }
    IEnumerator UpdateOnBattleScene()
    {
        while(true)
        {
            switch (gameState)
            {
                case GameState.Move:
                    MoveMonstersAndMap();
                    break;
                case GameState.Stop:
                    break;
                case GameState.Event:
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
        if (gameState == GameState.Move)
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
        else if (nowLine % 150 == 0)
        {
            SpawnBoss();
        }
        else if (nowLine % 6 == 0)
        {
            SpawnEventLine();
        }
        else
        {
            SpawnMonsters();
        }
        nowLine += 1;
        ProgressDisplayUpdate();
    }
    void SpawnBoss()
    {
        spawnLinePivotY += blockSize*10;
    }
    void SpawnEventLine()
    {
        Vector3 spawnPosition = new Vector3(0, spawnLinePivotY, 0);
        GameObject newObj = Instantiate(eventLinePrefab, spawnPosition, Quaternion.identity);
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
    void SpawnGeneralMonster(int i, bool canSpawnGem)
    {
        if (canSpawnGem)
        {
            float genCase = Random.Range(0f, 1f);
            if (genCase < 0.5f)
            {
                Vector3 spawnPosition = new Vector3(i * blockSize - 1.8f, spawnLinePivotY, 0);
                GameObject newGemObj = Instantiate(gemPrefab, spawnPosition, Quaternion.identity, gemParent.transform);
                entities.Add(newGemObj);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(i * blockSize - 1.8f, spawnLinePivotY, 0);
                GameObject newMonsterObj = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, monstersParent.transform);
                Monster newMonster = newMonsterObj.GetComponent<Monster>();
                monsters.Add(newMonster);
            }
        }
        else
        {
            Vector3 spawnPosition = new Vector3(i * blockSize - 1.8f, spawnLinePivotY, 0);
            GameObject newMonsterObj = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, monstersParent.transform);
            Monster newMonster = newMonsterObj.GetComponent<Monster>();
            monsters.Add(newMonster);
        }
    }
    
    public void ProgressDisplayUpdate()
    {
        meterBar.fillAmount = (float)nowLine / 450f;
        meterText.text = $"{nowLine}m";

        // MeterBar의 높이를 기준으로 fillAmount 값에 맞춰 meterImage의 y 위치 설정
        RectTransform meterBarRect = meterBar.GetComponent<RectTransform>();
        RectTransform meterImageRect = meterImage.GetComponent<RectTransform>();

        float barHeight = meterBarRect.rect.height;
        float newY = barHeight * meterBar.fillAmount;

        meterImageRect.anchoredPosition = new Vector2(meterImageRect.anchoredPosition.x, newY);
    }

}