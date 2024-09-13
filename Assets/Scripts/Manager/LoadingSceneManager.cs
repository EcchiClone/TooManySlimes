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
    private float progress = 0f;
    private float lerpedValue = 0f;
    private float speed = 5f;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private void Update()
    {
        lerpedValue = Mathf.Lerp(lerpedValue, progress, speed*Time.deltaTime);
        loadingBar.value = lerpedValue;

        percentageText.text = (lerpedValue * 100).ToString("F0") + "%";
    }

    IEnumerator LoadSceneAsync()
    {
        // 씬 비동기 로드
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        // 진행도 표시
        while (!asyncOperation.isDone)
        {
            progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2f);
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
