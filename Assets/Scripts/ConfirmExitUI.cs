using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ConfirmExitUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;
    public Button yesButton;
    public Button noButton;


    [SerializeField] private SC_FPSController fpsController;
    [SerializeField] private MouseMovementTwo mouseMovementTwo;

    private string sceneToLoad;
    private bool isPanelActive = false;

    public void Show(string message)
    {
        panel.SetActive(true);
        messageText.text = message;
        isPanelActive = true;



        // Блокируем движение камеры и скрываем курсор
        if (fpsController != null)
        {
            fpsController.canMove = false;
        }
        if (mouseMovementTwo != null)
        {
            mouseMovementTwo.enabled = false;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;



        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);

        // По желанию — автофокус на кнопку "Нет" или "Да"
        noButton.Select();
    }

    public void SetSceneToLoad(string sceneName)
    {
        sceneToLoad = sceneName;
    }

    private void Update()
    {
        if (isPanelActive)
        {
            // Нажатие Y или Enter = "Да"
            if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return))
            {
                OnYesClicked();
            }

            // Нажатие N или Escape = "Нет"
            if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Escape))
            {
                OnNoClicked();
            }
        }
    }

    private void OnYesClicked()
    {
        isPanelActive = false;
        panel.SetActive(false);

        // Восстанавливаем движение камеры и скрываем курсор
        if (fpsController != null)
        {
            fpsController.canMove = true;
        }
        if (mouseMovementTwo != null)
        {
            mouseMovementTwo.enabled = true;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnNoClicked()
    {
        isPanelActive = false;
        panel.SetActive(false);

        // Восстанавливаем движение камеры и скрываем курсор
        if (fpsController != null)
        {
            fpsController.canMove = true;
        }
        if (mouseMovementTwo != null)
        {
            mouseMovementTwo.enabled = true;
        }
       
    }
}