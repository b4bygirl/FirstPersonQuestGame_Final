using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject interactionText;
    public LayerMask interactionLayers;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayers))
        {
            if (hit.collider.gameObject.GetComponent<letter>())
            {
                interactionText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.K))
                {
                    hit.collider.gameObject.GetComponent<letter>().openCloseLetter();
                }
                else
                {
                    interactionText.SetActive(false);
                }
            }
            else
            {
                interactionText.SetActive(false);
            }
        }
    }
}
