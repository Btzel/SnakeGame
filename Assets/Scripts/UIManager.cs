using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Snake snake;
    SpawnManager spawnManager;
    [SerializeField] GameObject pane;
    [SerializeField] Image ReplayButton1;
    [SerializeField] Image ReplayButton2;
    int counter = 0;
    public float score = 0;
    [SerializeField] TMP_Text scoreText;

    
    void Start()
    {
        snake = GameObject.FindWithTag("Snake").GetComponent<Snake>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (snake.dead && counter == 0)
        {
            pane.SetActive(true);
            counter++;
        }

        int scoreX = (int)score;
        scoreText.text = "Score: "+scoreX.ToString();

        
    }
    
    
    
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
