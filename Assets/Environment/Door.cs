using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool open_door;
	// Use this for initialization
	void Start () {
        open_door = false;
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (open_door == true)
        {
            if (transform.localPosition.y > 8)
            {
                return;
            }
            this.transform.localPosition += new Vector3(0, 1, 0) * 3 * Time.deltaTime;
        }
	}

}
