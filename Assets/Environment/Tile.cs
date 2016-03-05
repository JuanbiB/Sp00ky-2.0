using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private TileModel model;
	private GameManager manager;
	public int x;
	public int y;

    BoxCollider box_collider;

    GameObject colFolder;
    private int index = 1;

    bool hasScent = false;

    private ScentModel scent_model;

    private float fade_counter = 3f;

    GameObject colObject;

    public bool occupied = false;

    // Use this for initialization
    public void init (int x, int y, GameManager manager) {
		this.manager = manager;
		this.x = x;
		this.y = y;

        box_collider = this.gameObject.AddComponent<BoxCollider>();
        box_collider.isTrigger = true;
        box_collider.size = new Vector3(.8f, 1, 1);

        //colFolder = new GameObject();
       // colFolder.name = "trailColliders";

        var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		model = modelObject.AddComponent<TileModel> ();
		model.init (this);
	}
		
	// Update is called once per frame
	void Update () {
	    if (hasScent == true)
        {
            fade_counter -= Time.deltaTime;            
            Color normal = model.gameObject.GetComponent<Renderer>().material.color;
            Color changing = new Color(normal.r, normal.g, normal.b, fade_counter * 0.5f );
            scent_model.gameObject.GetComponent<Renderer>().material.color = changing;

            if (fade_counter * 0.5f < 0)
            {
                hasScent = false;
                fade_counter = 3f;
                Destroy(colObject.gameObject);
            }
        }
	}

    void refresh_scent()
    {
        fade_counter = 3f;
    }

    public void spawnCollider()
    {
        colObject = new GameObject();
        colObject.transform.parent = manager.scent_colliders.transform;
        colObject.name = manager.scent_number.ToString();
        manager.scent_number++;

        colObject.transform.position = this.gameObject.transform.position;
        BoxCollider col = colObject.AddComponent<BoxCollider>();
        col.size = new Vector3(.5f, .5f, .5f);
        col.isTrigger = true;

        var scentObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        scent_model = scentObject.AddComponent<ScentModel>();
        scent_model.init(this);
    }

        void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "wall")
        {
            occupied = true;
            print("happening" + " at " + x + "," + y);
        }

        if (coll.tag == "Player" && occupied == true)
        {
            print("cant come thru here!");
            //coll.gameObject.GetComponent<Character>().rigidbody.velocity = Vector3.zero;
        }

        else if (coll.tag == "Player" || coll.tag == "wall")
        {
            print("Hey, I'm tile at " + x + " and " + y + " and skelly just entered me.");
            if (hasScent == false)
            {
                spawnCollider();
                hasScent = true;
            }
            else
            {
                refresh_scent();
            }
        }
        
    }

    // Scent shouldn't go away if you stand still on the tile you're on.
        void OnTriggerStay(Collider coll)
    {
        refresh_scent();
    }
}
