using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Snake snake;
    SpawnManager spawnManager;
    public GameObject pane;
    [SerializeField] Image ReplayButton1;
    [SerializeField] Image ReplayButton2;
    int counter = 0;
    public float score = 0;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text showWord;

    
    void Start()
    {
        snake = GameObject.FindWithTag("Snake").GetComponent<Snake>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

        int scoreX = (int)score;
        scoreText.text = "Score: "+scoreX.ToString();


        if(spawnManager.current == 1)
        {
            showWord.text = "";
        }
        else if(spawnManager.current == 2)
        {
            showWord.text = "e";
        }
        else if(spawnManager.current == 3)
        {
            showWord.text = "el";
        }
        else if(spawnManager.current == 4)
        {
            showWord.text = "l";
        }
        else if(spawnManager.current == 5)
        {
            showWord.text = "le";
        }
        
    }


    public void RestartGame()
    {
        
        counter = 0;
        spawnManager.nextScore = 0;
        score = 0;
        pane.SetActive(false);
        snake.dead = false;
        snake.speed = 280;
        snake.turnSpeed = 360;
        snake.snakeBody.Clear();
        snake.CreateBodyParts();
        spawnManager.allowed = false;
        GameObject curWord = GameObject.FindGameObjectWithTag("Word");
        Destroy(curWord);
        spawnManager.wordCounter = 0;
        spawnManager.current = 0;
        spawnManager.SpawnWordAtRandomPoint();
        spawnManager.allowed = true;
    }
}
