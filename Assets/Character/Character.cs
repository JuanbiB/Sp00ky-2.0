﻿using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Character : MonoBehaviour
{
    // Trial
    

    private float trailTime; //For the length of trail and time trailColliders exist
    private bool item = false;

    // Inventory
    public bool hasBone = false;

    // Animations
    public Animator animator;

    // Position
    float ylock;

    //Movement
    int direction = 1; // 1 = down, 2 = right, 3 = up, 4 = left
    public bool turnLock = false;
    int speed = 150;

    // Getting the manager
    GameObject managerObject;
    GameManager manager;

    // Rigibody
    public Rigidbody rigidbody;

    // This is what dictates the distance you travel every kep press. Currently set to one down below. Initialized to 999 to avoid issues.
    float stop = 999f;

    void Start()
    {
        // Lock at y at the start of the program.
        ylock = transform.localPosition.y;
        

        /*
        // Trial
        trailTime = 4.0f;
        TrailRenderer trail = this.gameObject.AddComponent<TrailRenderer>();
        trail.startWidth = 0.2f;
        trail.endWidth = 1f;
        trail.time = trailTime;
        //trail.material = AssetDatabase.GetBuiltinExtraResource<Material> ("Default-Particle.mat");
        
        InvokeRepeating("spawnCollider", 0.01f, 0.1f);
        */

        /*mat = GetComponent<Renderer>().material;
        mat.mainTexture = Resources.Load<Texture2D>("tileWall");   //in case we want to add a texture for testing
        mat.color = new Color(1, 1, 1);                                         
        mat.shader = Shader.Find("Sprites/Default");*/

        // Animation
        animator = this.gameObject.GetComponent<Animator>();

        // Rigidbody stuff
        rigidbody = this.gameObject.GetComponent<Rigidbody>();


        managerObject = GameObject.Find("manager");
        manager = managerObject.gameObject.GetComponent<GameManager>();
    }

    public bool isOccupied(int x, int y)
    {
        print(manager.tile_matrix[y, x]);
        bool hold = manager.tile_matrix[y, x].occupied;
        return hold;
    }

    public void Move()
    {
        // Do stuff
        turnLock = true;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // This statement is part of the system I built for collision detection to walls and objects.
            // It operates on the grid basis. You can either go to a grid or not, there's no in between. 
            if (isOccupied((int)Mathf.Round(transform.localPosition.x + 1), (int)Mathf.Round(transform.localPosition.z)) == false)
            {
                //manager.unPause();
                direction = 2;
                // Setting distance to travel per key press to + 1 of your current location.
                this.transform.localEulerAngles = new Vector3(45, 0, 0);
                stop = Mathf.Round(transform.localPosition.x + 1);
                this.transform.localScale = new Vector3(2, transform.localScale.y, transform.localScale.z);
                rigidbody.velocity = transform.right * Time.deltaTime * speed;

                animator.Play("walking-side");
                
            }
        }


        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isOccupied((int)Mathf.Round(transform.localPosition.x - 1), (int)Mathf.Round(transform.localPosition.z)) == false)
            {
                direction = 4;
                stop = Mathf.Round(transform.localPosition.x - 1);
                // Flipping horizontally
                this.transform.localScale = new Vector3(-2, transform.localScale.y, transform.localScale.z);
                rigidbody.velocity = -transform.right * Time.deltaTime * speed;
                animator.Play("walking-side");
            }
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (isOccupied((int)Mathf.Round(transform.localPosition.x), (int)Mathf.Round(transform.localPosition.z + 1)) == false)
            {
                direction = 3;
                stop = Mathf.Round(transform.localPosition.z + 1);
                rigidbody.velocity = transform.forward * Time.deltaTime * speed;
                animator.Play("walking-front");
            }
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (isOccupied((int)Mathf.Round(transform.localPosition.x), (int)Mathf.Round(transform.localPosition.z - 1)) == false)
            {
                direction = 1;
                stop = Mathf.Round(transform.localPosition.z - 1);
                rigidbody.velocity = -transform.forward * Time.deltaTime * speed;
                animator.Play("walking-front");
            }
        }
    }

    
    IEnumerator destroyCollider(GameObject col, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(col);
    }


    // Update is called once per frame
    void Update()
    {
        // To the right
        if (direction == 2)
        {
            if (transform.localPosition.x > stop)
            {
                rigidbody.velocity = Vector3.zero;
                turnLock = false;   // Do not accept input from the player until the move is done.
                if (!Input.GetKey(KeyCode.RightArrow))
                {
                    animator.Play("idle");
                    //manager.Pause();
                }
            }
        }
        // To the left
        else if (direction == 4)
        {
            if (transform.localPosition.x < stop)
            {
                rigidbody.velocity = Vector3.zero;
                transform.localEulerAngles = new Vector3(45, 0, 0);
                turnLock = false;
                if (!Input.GetKey(KeyCode.LeftArrow))
                {
                    
                    animator.Play("idle");
                    //manager.Pause();
                }
            }
        }
        // Upwards
        else if (direction == 3)
        {
            if (transform.localPosition.z > stop)
            {
                rigidbody.velocity = Vector3.zero;
                turnLock = false;
                if (!Input.GetKey(KeyCode.UpArrow))
                {
                    animator.Play("idle");
                    //manager.Pause();
                }
            }
        }

        //Downards
        else if (direction == 1)
        {
            if (transform.localPosition.z < stop)
            {
                rigidbody.velocity = Vector3.zero;
                turnLock = false;
                if (!Input.GetKey(KeyCode.DownArrow))
                {
                    animator.Play("idle");
                   //    manager.Pause();
                }
            }
        }

        // Direction toon's facing
        if (true == true)
        {
            //transform.eulerAngles = new Vector3(45, 0, 0);
        }
        else
        {
            //transform.eulerAngles = new Vector3(-45, 180, 0);
        }


        // Locking y, it should never change.
        transform.localPosition = new Vector3(transform.localPosition.x, ylock, transform.localPosition.z);
    }

    void gotSteak()
    {
        item = true;
        GameObject GameManager = GameObject.FindGameObjectWithTag("Game Controller");
        GameManager.SendMessage("UpdateGUI", "Steak");
    }

    void useSteak()
    {
        item = false;
        GameObject GameManager = GameObject.FindGameObjectWithTag("Game Controller");
        GameManager.SendMessage("UpdateGUI", "Nothing");
    }
}
