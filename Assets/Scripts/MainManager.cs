using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public List<string> questNames = new();
    public static MainManager mainManager;
    private void Awake()
    {
        if (mainManager == null)
        {
            Destroy(gameObject);
        }
        mainManager = this;
        DontDestroyOnLoad(gameObject);
    }
}
