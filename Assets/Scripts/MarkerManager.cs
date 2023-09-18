 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    Snake snake;
    SpawnManager spawnManager;
    public bool deadA;
    Animator anim;
    int counter = 0;
    public class Marker
    {
        public Vector3 position;
        public Quaternion rotation;

        public Marker(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
    public List<Marker> markerList = new List<Marker>();

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        snake = GameObject.FindWithTag("Snake").GetComponent<Snake>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (snake.snakeBody.Count < 2)
        {
            snake.CreateBodyParts();
        }
    }

    private void FixedUpdate()
    {

        UpdateMarkerList();
    }

    private void Update()
    {
        DeadAnimation();
    }
    public void UpdateMarkerList()
    {
        markerList.Add(new Marker(transform.position, transform.rotation));
    }
    public void CleanMarkerList()
    {
        markerList.Clear();
        markerList.Add(new Marker(transform.position, transform.rotation));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Word" && transform.tag == "Head")
        {

            Destroy(collision.gameObject);
            spawnManager.wordCounter--;
            snake.CreateBodyParts();

        }

        if (collision.tag == "Border")
        {
            snake.dead = true;
        }
    }

    public void DeadAnimation()
    {
        if(snake.dead == true && counter == 0)
        {
            markerList.Clear();
            anim.SetTrigger("dead");
            snake.speed = 0;
            snake.turnSpeed = 0;
            snake.snakeBody[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            counter++;
            Debug.Log("Works");
        }
        
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    
}


