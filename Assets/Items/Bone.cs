using UnityEngine;
using System.Collections;

public class Bone : MonoBehaviour {
    public GameObject manager;


	// Use this for initialization
	void Start () {
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager = GameObject.Find("manager");
            other.gameObject.GetComponent<Character>().hasBone = true;
            manager.GetComponent<GameManager>().SendMessage("UpdateGUI", "Bone");
            Destroy(this.gameObject);
        }
    }

 

    // Update is called once per frame
    void Update () {
	
	}
}
