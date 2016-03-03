using UnityEngine;
using System.Collections;

public class Pond : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
