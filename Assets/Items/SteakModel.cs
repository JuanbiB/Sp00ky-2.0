using UnityEngine;
using System.Collections;

public class SteakModel : MonoBehaviour {

    private Steak owner;
    private Material mat;

    // Use this for initialization
    public void init(Steak owner, int x, int z)
    {
        this.owner = owner;
		transform.parent = this.owner.steak.transform;
        transform.localPosition = new Vector3(0, 0, 0);     
        name = "Item Model";                               

		mat = GetComponent<Renderer>().material;  
		mat.shader = Shader.Find("Sprites/Default");                             
        mat.mainTexture = Resources.Load<Texture2D>("Sprites/steak"); 
    }
}
