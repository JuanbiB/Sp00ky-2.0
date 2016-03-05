using UnityEngine;
using System.Collections;


public class Dog : MonoBehaviour {
    public int balls = 999;
    public Transform[] wayPoints;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move()
    {
        print("This is my move!");
    }
}
