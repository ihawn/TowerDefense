using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseController : MonoBehaviour
{
    public Button StartButton;
    public GameObject GameOverText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartGameUI()
    {
        Debug.Log("Game start");
        StartButton.gameObject.SetActive(false);
        GameOverText.SetActive(false);
        GlobalReferences.gm.StartGame();
    }

    public void GameOverUI()
    {
        StartButton.gameObject.SetActive(true);
        GameOverText.SetActive(true);
    }
}
