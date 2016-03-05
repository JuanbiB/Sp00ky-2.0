using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private TileModel model;
	private GameManager manager;
	public int x;
	public int y;

    BoxCollider collider;

	// Use this for initialization
	public void init (int x, int y, GameManager manager) {
		this.manager = manager;
		this.x = x;
		this.y = y;

        collider = this.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		model = modelObject.AddComponent<TileModel> ();
		model.init (this);
	}
		
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        print("Hey, I'm tile at " + x + " and " + y + " and skelly just entered me.");
    }
}
