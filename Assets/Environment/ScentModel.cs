using UnityEngine;
using System.Collections;

public class ScentModel : MonoBehaviour {
    private Tile owner;
    private Material mat;

    // Use this for initialization
    public void init (Tile owner) {
        this.owner = owner;
        transform.parent = owner.transform;
        transform.localPosition = new Vector3(0, .02f, 0);
        transform.localEulerAngles = new Vector3(90, 0, 0);
        name = "Scent Model";

        GetComponent<Renderer>().material = (Material)Resources.Load("Materials/stank", typeof(Material));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
