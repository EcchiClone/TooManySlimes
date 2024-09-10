using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneBtn : MonoBehaviour
{
    public string loadingSceneName = "LoadingScene";
    public string battleSceneName = "BattleScene";

    public void OnLoadBattleSceneButtonClicked()
    {
        SceneManager.LoadScene(loadingSceneName);
        LoadingSceneManager.sceneToLoad = battleSceneName;
    }
}