using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalInteraction : MonoBehaviour
{
    public GameObject panelUI;         // Панель UI с заданиями
    public Text feedbackText;          // Текст реакции портала
    public Button hintButton;
    public Button attackButton;
    public Button findPortalButton;

  
    public AudioSource portalAudio;    // Для звука

    public Color hintColor = Color.cyan;
    public Color attackColor = Color.red;
    public Color findPortalColor = Color.magenta;
    public Color defaultColor = Color.white;

    private bool playerInRange = false;

    private bool hintDone = false;
    private bool attackDone = false;
    private bool findPortalDone = false;

    void Start()
    {
        panelUI.SetActive(false);

        hintButton.onClick.AddListener(OnSubmitHint);
        attackButton.onClick.AddListener(OnSubmitAttack);
        findPortalButton.onClick.AddListener(OnSubmitFindPortal);

        ResetButtonsColor();
        feedbackText.text = "";
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = !panelUI.activeSelf;
            panelUI.SetActive(isActive);

            if (isActive)
            {
                feedbackText.text = "";
                ResetButtonsColor();

                // Показываем курсор и разблокируем его
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                feedbackText.text = "";
                ResetButtonsColor();

                // Скрываем курсор и блокируем его (возвращаемся в режим FPS)
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            // Ставим флаг, что игрок нашёл портал
            if (QuestManager.Instance != null)
            {
                QuestManager.Instance.foundPortal = true;
                Debug.Log("Задание 'Найти портал' выполнено!");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            panelUI.SetActive(false);
            feedbackText.text = "";
            ResetButtonsColor();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnSubmitHint()
    {
        if (QuestManager.Instance == null)
            return;

        Image btnImage = hintButton.GetComponent<Image>();

        if (QuestManager.Instance.gotHint)
        {
            if (!hintDone)
            {
                hintDone = true;
                GiveFeedback("Задание выполнено.", hintButton, hintColor);
            }
            else
            {
                GiveFeedback("Задание уже выполнено.", hintButton, hintColor);
            }
        }
        else
        {
            // Задание не выполнено — меняем цвет кнопки обратно в defaultColor (белый)
            if (btnImage != null)
                btnImage.color = defaultColor;

            feedbackText.text = "Ты ещё не получил подсказку.";
        }
    }


    void OnSubmitAttack()
    {
        if (QuestManager.Instance == null)
            return;

        Image btnImage = attackButton.GetComponent<Image>();

        if (QuestManager.Instance.attackedCharacter)
        {
            if (!attackDone)
            {
                attackDone = true;
                GiveFeedback("Задание выполнено.", attackButton, attackColor);
            }
            else
            {
                GiveFeedback("Задание уже выполнено.", attackButton, attackColor);
            }
        }
        else
        {
            // Задание не выполнено — меняем цвет кнопки обратно в defaultColor (белый)
            if (btnImage != null)
                btnImage.color = defaultColor;

            feedbackText.text = "Ты ещё не атаковал персонажа.";
        }
    }

    void OnSubmitFindPortal()
    {
        if (QuestManager.Instance == null)
            return;

        Image btnImage = findPortalButton.GetComponent<Image>();

        if (QuestManager.Instance.foundPortal)
        {
            if (!findPortalDone)
            {
                findPortalDone = true;
                GiveFeedback("Задание выполнено.", findPortalButton, findPortalColor);
            }
            else
            {
                GiveFeedback("Задание уже выполнено.", findPortalButton, findPortalColor);
            }
        }
        else
        {
            // Задание не выполнено — меняем цвет кнопки обратно в defaultColor (белый)
            if (btnImage != null)
                btnImage.color = defaultColor;

            feedbackText.text = "Ты ещё не нашёл портал.";
        }
    }



    void GiveFeedback(string message, Button button, Color color)
    {
        feedbackText.text = message;

        // Меняем цвет кнопки
        Image btnImage = button.GetComponent<Image>();
        if (btnImage != null)
            btnImage.color = color;

        if (portalAudio != null)
            portalAudio.Play();

        CheckAllTasksDone();
    }

    void ResetButtonsColor()
    {
        hintButton.GetComponent<Image>().color = defaultColor;
        attackButton.GetComponent<Image>().color = defaultColor;
        findPortalButton.GetComponent<Image>().color = defaultColor;
    }

    void CheckAllTasksDone()
    {
        if (hintDone && attackDone && findPortalDone)
        {
            feedbackText.text = "Все задания сданы!";
            Debug.Log("Все задания выполнены!");



            // Например, изменить цвет всех кнопок на зелёный:
            Color allDoneColor = Color.green;

            hintButton.GetComponent<Image>().color = allDoneColor;
            attackButton.GetComponent<Image>().color = allDoneColor;
            findPortalButton.GetComponent<Image>().color = allDoneColor;
        }
    }
}
