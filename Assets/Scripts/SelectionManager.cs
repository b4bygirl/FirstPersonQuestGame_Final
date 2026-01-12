using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{


    public static SelectionManager Instance { get; set; }
    public bool onTarget;

    public GameObject selectedObject;

    public GameObject interaction_info_UI;
    Text interaction_text;

    public Image centerDotImage;
    public Image handIcon;

    private void Start()
    {
        onTarget = true;
        interaction_text = interaction_info_UI.GetComponent<Text>();
    }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        // Блокируем взаимодействие с миром, если меню открыто или клик по UI
        if ((MenuManager.Instance != null && MenuManager.Instance.isMenuOpen) || EventSystem.current.IsPointerOverGameObject())
        {
            interaction_info_UI.SetActive(false);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            NPC npc = selectionTransform.GetComponent<NPC>();
            if (npc && npc.playerInRange)
            {
                interaction_text.text = "Получить подсказку (ЛКМ)";
                interaction_info_UI.SetActive(true);

                if (Input.GetMouseButtonDown(0) && npc.isTalkingWithPlayer == false)
                {
                    npc.StartConversation();
                }
                if (DialogSystem.Instance.dialogUIActive)
                {
                    interaction_info_UI.SetActive(false);

                }
            }
            else
            {
                interaction_text.text = "";
                interaction_info_UI.SetActive(false);
            }

            if (interactable && interactable.playerInRange)
            {

                if (interactable && interactable.playerInRange)
                {

                    onTarget = true;

                    interaction_text.text = interactable.GetItemName();
                    interaction_info_UI.SetActive(true);
                }
                else //if there a hit, but without an Interactable Script.
                {
                    //interaction_info_UI.SetActive(false);
                }

            }

            else //if there a hit, but without an Interactable Script.
            {
                //interaction_info_UI.SetActive(false);
            }

        }
        else //if there is not hit at all.
        {
            onTarget = false;
            interaction_info_UI.SetActive(false);
        }
    }
    public void DisableSelection()
    {
      
       
        interaction_info_UI.SetActive(false);

        selectedObject = null;
    }
    public void EnableSelection()
    {
       
        interaction_info_UI.SetActive(true);
    }
}