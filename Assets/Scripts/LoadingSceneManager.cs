using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static string sceneToLoad;

    // 진행도 UI
    public Slider loadingBar;
    public TextMeshProUGUI percentageText;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // 씬 비동기 로드
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        // 진행도 표시
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;

            percentageText.text = (progress * 100).ToString("F0") + "%";

            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
