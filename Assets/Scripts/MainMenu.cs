using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void NewGame()
    {
        SceneManager.LoadScene("FirstGameScene");
    }


    public void ExitGame()
    {
        Debug.Log("Qutting Game");
        Application.Quit();
    }
}
