using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadeGroup; // CanvasGroup для затемнения
    public float fadeDuration = 1f; // Длительность затемнения

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Плавное затемнение экрана (при запуске сцены)
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    // Плавное появление (Fade-in)
    IEnumerator FadeIn()
    {
        fadeGroup.alpha = 1f;
        while (fadeGroup.alpha > 0f)
        {
            fadeGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        fadeGroup.alpha = 0f;
    }

    // Плавное исчезновение (Fade-out) и загрузка сцены
    IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeGroup.blocksRaycasts = true; // Блокируем взаимодействие с UI во время затемнения
        while (fadeGroup.alpha < 1f)
        {
            fadeGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
        SceneManager.LoadScene(sceneName); // Загружаем сцену
    }
}