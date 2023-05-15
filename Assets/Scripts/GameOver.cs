using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void EndGame()
    {
        gameObject.SetActive(true);
    }

    public void playAgain()
    {
        SceneManager.LoadScene(1);

    }


    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
