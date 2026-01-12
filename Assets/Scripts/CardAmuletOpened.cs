using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardAmuletlOpened : MonoBehaviour
{
    public GameObject Planee;
    public void openPlanee()
    {
        if (Planee != null)
        {
            Planee.SetActive(true);
        }
    }
}

