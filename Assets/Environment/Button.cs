using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    private GameObject manager;
    private Vector3 gatePos;
    private GameObject gate;

	// Use this for initialization
	void Start () {
        gate = GameObject.Find("Gate");
        gatePos = gate.transform.position;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Dog")
        {
            //manager = GameObject.Find("manager");
            gate.transform.Translate(0, -3, 0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Dog")
        {
            
            gate.transform.Translate(0, 3, 0);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
