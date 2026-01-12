using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{

    public ConfirmExitUI confirmUI; // UI для подтверждения выхода
    public string sceneToLoad = "GameScene"; // Название сцены для загрузки

    private bool isPlayerNear = false; // Флаг, чтобы проверить, рядом ли игрок

    // Проверяем, находится ли игрок рядом с дверью
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Проверяем, покинул ли игрок зону двери
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    // Обрабатываем нажатие клавиши
    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.K)) // Нажимаем 'K' для взаимодействия
        {
            if (confirmUI != null)
            {
                confirmUI.Show("Ты уверен, что хочешь выйти в лес?");
                confirmUI.SetSceneToLoad(sceneToLoad); // Устанавливаем сцену для загрузки
            }
        }
    }
}
