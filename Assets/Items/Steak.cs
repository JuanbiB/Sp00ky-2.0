using UnityEngine;
using System.Collections;

public class Steak : MonoBehaviour {

	private BoxCollider col;
	private SteakModel model;
	private Vector3 position;
    private GameObject manager;
	public float x;
	public float z;

    // Use this for initialization
    public void init(float x, float z, bool interactable)
    {
        print(x);
        print(z);
		this.x = x;
		this.z = z;
		this.gameObject.transform.position = new Vector3 (x, 0.5f, z);
        this.gameObject.transform.eulerAngles = new Vector3(45, 0);
        
       
            col = this.gameObject.AddComponent<BoxCollider>();
            col.size = new Vector2(2, 1);
            col.center = new Vector3(x, 0, z);
        

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
			if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Character>().canSteak)
            {
				print ("steak");
				other.gameObject.GetComponent<Character> ().canSteak = false;
                manager = GameObject.Find("manager");
                other.gameObject.GetComponent<Character>().hasSteak = true;
                manager.GetComponent<GameManager>().SendMessage("UpdateGUI", "Steak");
				this.gameObject.transform.position = new Vector3 (x, -2, z);
            }
        }
	}
}
