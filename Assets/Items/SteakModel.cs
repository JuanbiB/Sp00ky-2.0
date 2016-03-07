using UnityEngine;
using System.Collections;

public class SteakModel : MonoBehaviour {

    private Steak owner;
    private Material mat;

    // Use this for initialization
    public void init(Steak owner, float x, float z)
    {
        this.owner = owner;
		transform.parent = this.owner.transform;
        transform.localPosition = new Vector3(0, 0, 0);     
        name = "Item Model";                               

		mat = GetComponent<Renderer>().material;  
		mat.shader = Shader.Find("Sprites/Default");                             
        mat.mainTexture = Resources.Load<Texture2D>("Materials/Steak-Max"); 
    }
}
