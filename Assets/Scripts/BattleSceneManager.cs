using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GameState { Move, Stop, Event, Clear }

public class BattleSceneManager : MonoBehaviour
{
    public Player player;

    public List<Monster> monsters;
    public LoopBackground loopBackground;

    public GameObject monsterPrefab;
    public GameObject monstersParent;

    public float mapSpeed = 2f;

    private float monsterGenPivotY = 10.0f;


    public GameState gameState;

    private void Start()
    {
        gameState = GameState.Move;
    }

    private void Update()
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
                    image.transform.Translate(Vector3.down * mapSpeed * Time.deltaTime*2);
            }


            // 몬스터 이동
            List<Monster> disappearMonster = new List<Monster>();
            foreach (var monster in monsters)
            {
                if (monster)
                {
                    monster.transform.Translate(Vector3.down * mapSpeed * Time.deltaTime);
                }
            }
            foreach (var monster in disappearMonster)
            {
                monsters.Remove(monster);
                monster.Disappear();
            }

            // 몬스터 생성용 피봇 이동
            monsterGenPivotY -= mapSpeed * Time.deltaTime;

            if (monsterGenPivotY < 8.0f)
                SpawnMonsters();
        }
    }

    void SpawnMonsters()
    {
        // 몬스터 간격 x 0.9, y 0.9

        List<int> mosnterPosIdx;

        float genCase = Random.Range(0f, 1f);

        if (genCase < 0.1f) // 10% 확률로 한 줄에 다섯
            foreach (int i in new List<int> {0,1,2,3,4})
            {
                Vector3 spawnPosition = new Vector3(i * 0.9f - 1.8f, monsterGenPivotY, 0);
                GameObject newMonsterObj = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, monstersParent.transform);
                Monster newMonster = newMonsterObj.GetComponent<Monster>();
                monsters.Add(newMonster);
            }
        else if (genCase < 0.2f) // 10% 확률로 한 줄에 넷
        {
            mosnterPosIdx = Utils.PickRandomNumbers(5, 4);
            foreach (int i in mosnterPosIdx)
            {
                Vector3 spawnPosition = new Vector3(i * 0.9f - 1.8f, monsterGenPivotY, 0);
                GameObject newMonsterObj = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, monstersParent.transform);
                Monster newMonster = newMonsterObj.GetComponent<Monster>();
                monsters.Add(newMonster);
            }
        }
        else if (genCase < 0.5f) // 30% 확률로 랜덤 수 몬스터
        {
            mosnterPosIdx = Utils.PickRandomNumbers(5, (int)Random.Range(0f, 6f));
            foreach (int i in mosnterPosIdx)
            {
                Vector3 spawnPosition = new Vector3(i * 0.9f - 1.8f, monsterGenPivotY, 0);
                GameObject newMonsterObj = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, monstersParent.transform);
                Monster newMonster = newMonsterObj.GetComponent<Monster>();
                monsters.Add(newMonster);
            }
        }

        monsterGenPivotY += 0.9f;
    }
}