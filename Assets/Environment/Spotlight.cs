using UnityEngine;
using System.Collections;

public class Spotlight : MonoBehaviour {
    private float speed;
	// Use this for initialization
	void Start () {
        speed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.localPosition.y < 7)
        {
            return;
        }

        this.transform.localPosition += new Vector3(0, -1, 0) * speed * Time.deltaTime;
	}
}
