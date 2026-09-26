using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KioskSceneController : MonoBehaviour
{
    [Header("Transition Overlay")]
    public CanvasGroup fadeOverlay;
    public float fadeDuration = 0.25f;

    private void Awake()
    {
        if (fadeOverlay != null)
        {
            fadeOverlay.alpha = 1f;
            fadeOverlay.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        if (fadeOverlay != null)
        {
            StartCoroutine(FadeScreen(0f, () => fadeOverlay.gameObject.SetActive(false)));
        }
    }

    public void GoToMainMenu() => TriggerSceneLoad("Scene_MainMenu");
    public void GoToLeonScene() => TriggerSceneLoad("Scene_Leon");
    public void GoToChrisScene() => TriggerSceneLoad("Scene_Chris");
    public void GoToEthanScene() => TriggerSceneLoad("Scene_Ethan");

    private void TriggerSceneLoad(string targetSceneName)
    {
        if (fadeOverlay != null)
        {
            fadeOverlay.gameObject.SetActive(true);
            StartCoroutine(FadeScreen(1f, () => StartCoroutine(LoadSceneAsync(targetSceneName))));
        }
        else
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    private IEnumerator FadeScreen(float targetAlpha, System.Action onComplete)
    {
        float speed = 1f / fadeDuration;
        float currentAlpha = fadeOverlay.alpha;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            fadeOverlay.alpha = Mathf.Lerp(currentAlpha, targetAlpha, time);
            yield return null;
        }

        fadeOverlay.alpha = targetAlpha;
        onComplete?.Invoke();
    }

    private IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

