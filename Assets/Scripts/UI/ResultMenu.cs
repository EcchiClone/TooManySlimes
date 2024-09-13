using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultMenu : MonoBehaviour
{
    public GameObject resultCanvas;
    public Image orangeBar;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    public Button ToLobbyButton;

    int currentScore;

    public void InitializeResult()
    {
        resultCanvas.SetActive(true);
        SetTexts();
        SetBar();
        SetOtherButtons();
    }
    void SetTexts()
    {
        currentScore = Mathf.Min(Game.Battle.nowLine, Game.Battle.maxLine);
        bestScoreText.text = $"Best ???m";
    }
    void SetBar()
    {
        StartCoroutine(AnimateBarAndText());
    }

    IEnumerator AnimateBarAndText()
    {
        float targetFill = (float)currentScore / Game.Battle.maxLine;
        float currentFill = 0f;
        float duration = 1f; // 1초 진행
        float elapsedTime = 0f;

        int displayedScore = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentFill = Mathf.Lerp(0f, targetFill, elapsedTime / duration);
            orangeBar.fillAmount = currentFill;

            // 텍스트에도 반영
            displayedScore = Mathf.RoundToInt(Mathf.Lerp(0, currentScore, elapsedTime / duration));
            currentScoreText.text = $"{displayedScore}m";

            yield return null;
        }

        orangeBar.fillAmount = targetFill;
        currentScoreText.text = $"{currentScore}m";
    }
    void SetOtherButtons()
    {
        ToLobbyButton.onClick.RemoveAllListeners();
        ToLobbyButton.onClick.AddListener(ToLobbyButtonClicked);
    }
    public void ToLobbyButtonClicked()
    {
        Game.Battle.MoveToLobbyScene();
    }

}
