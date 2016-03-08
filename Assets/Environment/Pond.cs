using UnityEngine;
using System.Collections;

public class Pond : MonoBehaviour {

	BoxCollider box_collider;

	// Use this for initialization
	void Start () {
      //  Renderer rend = GetComponent<Renderer>();
      //  rend.material.shader = Shader.Find("Specular");
      //  rend.material.SetColor("_SpecColor", Color.red);

		box_collider = this.gameObject.AddComponent<BoxCollider>();
		box_collider.isTrigger = true;
		box_collider.size = new Vector3(.8f, 1, 1.4f);
		box_collider.center = new Vector3 (0, 0, .5f);
		box_collider.tag = "Pond";


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
