using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && (other.gameObject.GetComponent<Character>().hasBone == true))
        {
            //Go to next scene
            Debug.Log("You win!");
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
