using UnityEngine;
using System.Collections;

public class Steak : MonoBehaviour {

	public GameObject steak;
	private BoxCollider col;
	private SteakModel model;
	private Vector3 position;

    // Use this for initialization
    public void init(int x, int z)
    {
		steak.transform.position = new Vector3 (x, 5, z);

		col = steak.AddComponent<BoxCollider>();
		col.size = new Vector2 (1, 1);
		col.center = new Vector3 (x, 0, z);

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);  
		model = modelObject.AddComponent<SteakModel>();                  
        model.init(this, x, z);

		steak.name = "Steak";
        steak.tag = "Item";
    }
		
	public void UpdateItem(){
		return;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			pickup();
		}
	}
		
	public void pickup(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		player.SendMessage ("GotSteak");
	}

	public void destroy(){
		this.destroy();
	}
}
