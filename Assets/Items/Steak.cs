using UnityEngine;
using System.Collections;

public class Steak : MonoBehaviour {

	private BoxCollider col;
	private SteakModel model;
	private Vector3 position;
    private GameObject manager;

    // Use this for initialization
    public void init(float x, float z, bool interactable)
    {
        print(x);
        print(z);
		this.gameObject.transform.position = new Vector3 (x, 0.5f, z);
        this.gameObject.transform.eulerAngles = new Vector3(45, 0);
        if (interactable)
        {
            col = this.gameObject.AddComponent<BoxCollider>();
            col.size = new Vector2(1, 1);
            col.center = new Vector3(x, 0, z);
        }

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);  
		model = modelObject.AddComponent<SteakModel>();                  
        model.init(this, x, z);

		this.gameObject.name = "Steak";
        //steak.tag = "Item";
    }
		
	public void UpdateItem(){
		return;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
            if (other.gameObject.tag == "Player")
            {
                manager = GameObject.Find("manager");
                other.gameObject.GetComponent<Character>().hasSteak = true;
                manager.GetComponent<GameManager>().SendMessage("UpdateGUI", "Steak");
                Destroy(this.gameObject);
            }
        }
	}
}
