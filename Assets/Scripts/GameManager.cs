using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject levelhandler;
    //for when boss is killed

    private void Start()
    {
        Boss.OnEndLevel += EndLevel;
    }

    private void OnDestroy()
    {
        Boss.OnEndLevel -= EndLevel;
    }

    private void EndLevel()
    {
        levelhandler.SetActive(true);
    }
}