using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public void playNextLevel()
    {
        SceneManager.LoadScene(2);
    }


    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
