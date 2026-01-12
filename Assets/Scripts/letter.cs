using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class letter : MonoBehaviour
{
    public GameObject letterUI;
    bool toggle;
    public SC_FPSController player;
    public Renderer letterMesh;
    public void openCloseLetter()
    {
        toggle = !toggle;
        if (toggle == false)
        {
            letterUI.SetActive(false);
            letterMesh.enabled = true;
            player.enabled = true;
        }
        if (toggle == true)
        {
            letterUI.SetActive(true);
            letterMesh.enabled = false;    
            player.enabled = false;
        }
    }
}
