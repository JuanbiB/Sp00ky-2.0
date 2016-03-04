using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Character : MonoBehaviour
{
    // Trial
    GameObject colFolder;
    private int index=1;
    private float trailTime; //For the length of trail and time trailColliders exist
	private bool item = false; 
	
    // Inventory
    public bool hasBone = false;

    // Animations
    public Animator animator;

    // Position
    float ylock;

    //Movement
    bool right = true;

	// Getting the manager
	GameObject managerObject;
	GameManager manager;

	// Rigibody
	Rigidbody rigidbody;

	// This is what dictates the distance you travel every kep press. Currently set to one down below. Initialized to 999 to avoid issues.
	int stop = 999;

    void Start()
    {
        // Lock at y at the start of the program.
        ylock = transform.localPosition.y;

        // Trial
        trailTime = 4.0f;
        TrailRenderer trail = this.gameObject.AddComponent<TrailRenderer>();
        trail.startWidth = 0.2f;
        trail.endWidth = 1f;
        trail.time = trailTime;
		//trail.material = AssetDatabase.GetBuiltinExtraResource<Material> ("Default-Particle.mat");
        colFolder = new GameObject();
        colFolder.name = "trailColliders";
        InvokeRepeating("spawnCollider", 0.01f, 0.1f);

        /*mat = GetComponent<Renderer>().material;
        mat.mainTexture = Resources.Load<Texture2D>("tileWall");   //in case we want to add a texture for testing
        mat.color = new Color(1, 1, 1);                                         
        mat.shader = Shader.Find("Sprites/Default");*/

        // Animation
        animator = this.gameObject.GetComponent<Animator>();

		managerObject = GameObject.Find ("manager");
		//GameManager manager = managerObject.GetComponent<GameManager> ();

		// Rigidbody stuff
		rigidbody = this.gameObject.GetComponent<Rigidbody>();

    }

	public void Move(){
		// Do stuff
		print("I'm doing shit!");
		if (Input.GetKey (KeyCode.RightArrow)) {

			// Setting distance to travel per key press to + 1 of your current location.
			stop = (int) transform.localPosition.x + 1;
			rigidbody.velocity = transform.right * Time.deltaTime * 300;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			stop = (int) transform.localPosition.x + 1;
			rigidbody.velocity = -transform.right * Time.deltaTime * 300;
		}
	}

	
    void spawnCollider()
    {
        GameObject colObject = new GameObject();
        colObject.transform.parent = colFolder.transform;
		colObject.name =  index.ToString();
        index++;
        colObject.transform.position = this.gameObject.transform.position;
        BoxCollider col = colObject.AddComponent<BoxCollider>();
        col.size = new Vector2(1f, 1f);
        col.isTrigger = true;
//		colObject.tag = "Scent";

        StartCoroutine(destroyCollider(colObject,trailTime));
    }
    IEnumerator destroyCollider(GameObject col, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(col);
    }
		

    // Update is called once per frame
    void Update()
    {

		if (transform.localPosition.x > stop) {
			rigidbody.velocity = Vector3.zero;
		}

		// Direction toon's facing
		if (right == true)
		{
			transform.eulerAngles = new Vector3(45, 0, 0);
		}
		else
		{
			transform.eulerAngles = new Vector3(-45, 180, 0);
		}



		transform.localPosition = new Vector3(transform.localPosition.x, ylock, transform.localPosition.z);
    }

	void gotSteak(){
		item = true;
		GameObject GameManager = GameObject.FindGameObjectWithTag ("Game Controller");
		GameManager.SendMessage("UpdateGUI", "Steak");
	}

	void useSteak(){
		item = false;
		GameObject GameManager = GameObject.FindGameObjectWithTag ("Game Controller");
		GameManager.SendMessage("UpdateGUI", "Nothing");
	}
}
