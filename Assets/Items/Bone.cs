using UnityEngine;
using System.Collections;

public class Bone : MonoBehaviour {
    public GameObject manager;


	// Use this for initialization
	void Start () {
        manager = GameObject.Find("manager");
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().hasBone = true;
            manager.SendMessage("UpdateGUI", "Bone");
            Destroy(this.gameObject);
        }
    }

 

    // Update is called once per frame
    void Update () {
	
	}
}
