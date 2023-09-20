using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] float distanceBetween = .2f;
    public float speed = 200;
    public float turnSpeed = 18;
    [SerializeField] GameObject bodyPart;
    [SerializeField] GameObject headPart;
    public List<GameObject> snakeBody = new List<GameObject>();
    [SerializeField] Sprite snakeBodySkin;
    [SerializeField] Sprite snakeHeadSkin;
    public bool dead = false;
  
    


    public float countUp = 0;
    private void Start()
    {
        CreateBodyParts();
    }

    void SnakeMovement()
    {
        if (!dead)
        {
            snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * speed * Time.deltaTime;
            if (Input.GetAxis("Horizontal") != 0)
                snakeBody[0].transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal")));
            if (snakeBody.Count > 1)
            {
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    MarkerManager markM = snakeBody[i - 1].GetComponent<MarkerManager>();
                    snakeBody[i].transform.position = markM.markerList[0].position;
                    snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                    markM.markerList.RemoveAt(0);
                }
            }
        }
        
    }

    private void FixedUpdate()
    {

        SnakeMovement();
    }

    public void CreateBodyParts()
    {
        if (snakeBody.Count == 0)
        {
            GameObject temp1 = Instantiate(headPart, transform.position, transform.rotation, transform);
            if (!temp1.GetComponent<MarkerManager>())
                temp1.AddComponent<MarkerManager>();
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.layer = 9;
                temp1.GetComponent<SpriteRenderer>().sprite = snakeHeadSkin;
                temp1.GetComponent<SpriteRenderer>().sortingOrder = 100;
                temp1.tag = "Head";
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
                temp1.AddComponent<CircleCollider2D>();
                temp1.GetComponent<CircleCollider2D>().isTrigger = true;
            }
            
            snakeBody.Add(temp1);
            return;


        }
        
        
        
        StartCoroutine(waitFor());
        


    }

    IEnumerator waitFor()
    {
        MarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        markM.CleanMarkerList();
        yield return new WaitForSeconds(0.02f);
        GameObject temp = Instantiate(bodyPart, markM.markerList[markM.markerList.Count - 1].position, markM.markerList[markM.markerList.Count - 1].rotation, transform);
        
        if (!temp.GetComponent<MarkerManager>())
            temp.AddComponent<MarkerManager>();
        if (!temp.GetComponent<Rigidbody2D>())
        {
            temp.layer = 9;
            temp.GetComponent<SpriteRenderer>().sprite = snakeBodySkin;
            temp.GetComponent<SpriteRenderer>().sortingOrder = 50;
            temp.AddComponent<Rigidbody2D>();
            temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            temp.AddComponent<CircleCollider2D>();
            temp.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        snakeBody.Add(temp);
        temp.GetComponent<MarkerManager>().CleanMarkerList();
    }

    


}

