using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

    BoxCollider coll;
    float clock;
    bool triggered;
    Door door;
	
    // Use this for initialization
	void Start () {
        // Adding collider (rigidbody on character)
        coll = gameObject.AddComponent<BoxCollider>();
        coll.isTrigger = true;

        // "A component is returned only if it is found on an active GameObject."
        // GetComponent on it's own doesn't work for some reason. 
        door = GetComponentInChildren<Door>();

        triggered = false;
        clock = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        x++;
        x--;
        clock = clock + Time.deltaTime;

	    if (triggered == true)
        {
            if (y < -0.08f)
            {
                return;
            }

            this.transform.localPosition += new Vector3(0, -1) * 0.1f * Time.deltaTime;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        triggered = true;
        door.open_door = true;
    }
}
