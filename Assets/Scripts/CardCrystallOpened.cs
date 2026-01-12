using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardCrystallOpened : MonoBehaviour
{
    public GameObject Plane;
    public void openPlane()
    {
        if(Plane != null)
        {
            Plane.SetActive(true);
        }
    }
}


