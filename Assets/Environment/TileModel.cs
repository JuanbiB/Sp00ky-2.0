using UnityEngine;
using System.Collections;

public class TileModel : MonoBehaviour {

	private Tile owner;
	private Material mat;

	// Use this for initialization
	public void init (Tile owner) {
		this.owner = owner;
		transform.parent = owner.transform;
		transform.localPosition = new Vector3 (0, 0, 0);
		transform.localEulerAngles = new Vector3 (90, 0, 0);
		name = "Tile Model";
    
		GetComponent<Renderer> ().material = (Material) Resources.Load ("Materials/tileBlank", typeof(Material));
        
		/*
		mat = GetComponent<Renderer>().material;
		mat.mainTexture = Resources.Load<Texture2D> ("Sprites/Steak-Max");
		mat.color = new Color (1, 1, 1);
		//mat.shader = Shader.Find ("Sprites/Default");
        */
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
