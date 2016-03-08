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

    public bool hasScent = false;

    private ScentModel scent_model;

    private float fade_counter = 5f;

    public GameObject colObject;

    public bool occupied = false;

    public bool paused = false;

    GameObject gameManager;

    // Use this for initialization
    public void init (int x, int y, GameManager manager) {
		this.manager = manager;
		this.x = x;
		this.y = y;

        // Scent colliders
        box_collider = this.gameObject.AddComponent<BoxCollider>();
        box_collider.isTrigger = true;
        box_collider.size = new Vector3(.8f, 1, 1);
		box_collider.tag = "Tile";

        var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		model = modelObject.AddComponent<TileModel> ();
		model.init (this);

        gameManager = GameObject.Find("manager");
        manager = gameManager.gameObject.GetComponent<GameManager>();
    }
		
	// Update is called once per frame
	void Update () {
 
        if (hasScent == true)
        {
            // Game Manager controls pausing and whatnot. Scent will only fade when player is in movement.
            if (manager.paused == false)
            {
                fade_counter -= Time.deltaTime;
                Color normal = model.gameObject.GetComponent<Renderer>().material.color;
                Color changing = new Color(normal.r, normal.g, normal.b, fade_counter * 0.5f);
                scent_model.gameObject.GetComponent<Renderer>().material.color = changing;

                if (fade_counter * 0.5f < 0)
                {
                    hasScent = false;
                    fade_counter = 3f;
					print ("count before " + manager.scentList.Count);
					manager.scentList.Remove (this.colObject);
					print ("count before " + manager.scentList.Count);
                    Destroy(colObject.gameObject);
                }
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
        col.size = new Vector3(.5f, 2f, .5f);
        col.isTrigger = true;
		col.tag = "Scent";

        var scentObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        scent_model = scentObject.AddComponent<ScentModel>();
        scent_model.init(this);
    }

        void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "wall") {
			occupied = true;
			//print("happening" + " at " + x + "," + y);
		}

		if (coll.tag == "Player" && occupied == true) {
			//print("cant come thru here!");
		} else if (coll.tag == "Player") {//|| coll.tag == "wall")
			//print("Hey, I'm tile at " + x + " and " + y + " and skelly just entered me.");
			if (hasScent == false) {
				spawnCollider ();
				manager.scentList.Add (this.colObject);
				hasScent = true;
			} else {
				manager.scentList.Remove (this.colObject);
				manager.scentList.Add (this.colObject);
				refresh_scent ();
			}
		}
        
	}

    // Scent shouldn't go away if you stand still on the tile you're on.
        void OnTriggerStay(Collider coll)
    {
       
     
        if (coll.tag == "Player")
        {
			refresh_scent();
           
            if (coll.gameObject.GetComponent<Character>().char_pause == true)
            {
                this.paused = true;
            }
            else
            {
                this.paused = false;
            }
        }
      
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            occupied = false;
        }
    }
}
