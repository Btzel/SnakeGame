using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform spawnCenter;
    [SerializeField] Vector3 size;
    [SerializeField] GameObject[] wordsToSpawn;
    public int wordCounter = 0;
    public int current = 0;
    [SerializeField] float score = 0;
    float nextScore = 0;
    UIManager manager;

    private void Start()
    {
        manager = GameObject.Find("DeadCanvas").GetComponent<UIManager>();
    }
    private void Update()
    {
        if (wordCounter == 0)
            SpawnWordAtRandomPoint();
    }

    void SpawnWordAtRandomPoint()
    {
        
        wordCounter++;
        float randomX = Random.Range(-size.x / 2, size.x / 2);
        float randomY = Random.Range(-size.y / 2, size.y / 2);
        Vector2 distance = new Vector2(randomX, randomY) - new Vector2(GameObject.FindWithTag("Head").transform.position.x, GameObject.FindWithTag("Head").transform.position.y);
        
        if(distance.magnitude > 5)
        {
            manager.score += nextScore;
            nextScore = distance.magnitude;
            Vector3 spawnPos = spawnCenter.position + new Vector3(randomX, randomY, 1);
            if(current < 4)
            {
                GameObject Word = Instantiate(wordsToSpawn[current], spawnPos, Quaternion.identity, transform);
            }
            else
            {
                Debug.Log("You Won");
            }
            

        }
        else
        {
            wordCounter--;
            Debug.Log("Baþarýsýz");
            return;
        }

        current++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(spawnCenter.position, size);
    }
}
