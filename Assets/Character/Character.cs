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
		colObject.tag = "Scent";

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
		this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		int speed = 3;

        // Movement control - Do not code anything that's not movement below here.

        // Down-right diagonal.
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            animator.Play("walking-side");
            transform.localPosition += new Vector3(1, 0, -1) * speed * Time.deltaTime;
            return;
        }

        // Up-right diagonal
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            animator.Play("walking-side");
            transform.localPosition += new Vector3(1, 0, 1) * speed * Time.deltaTime;
            return;
        }

        // Up-left diagonal
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            right = false;
            animator.Play("walking-side");
            transform.localPosition += new Vector3(-1, 0, 1) * speed * Time.deltaTime;
            return;
        }

        // Down-left diagonal
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            right = false;
            animator.Play("walking-side");
            transform.localPosition += new Vector3(-1, 0, -1) * speed * Time.deltaTime;
            return;
        }

        // Moving 'Upwards' by modifing z-value +
        if (Input.GetKey(KeyCode.UpArrow))
		{
            animator.Play("walking");
            transform.localPosition += new Vector3(0, 0, 1) * speed * Time.deltaTime;
		}
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.Play("idle");      // shouldn't be 'walking' if he's standing still.
        }

        // Moving 'Downwards' by modifing z-value - 
        if (Input.GetKey(KeyCode.DownArrow))
		{
            animator.Play("walking");   // Pretty self-explanatory
			transform.localPosition += new Vector3(0, 0, -1) * speed * Time.deltaTime;
		}

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.Play("idle");      // shouldn't be 'walking' if he's standing still.
        }

		// Moving Right 
		if (Input.GetKey(KeyCode.RightArrow))
		{
			right = true;
			transform.localPosition += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            animator.Play("walking-side");
			//this.GetComponent<Rigidbody>().velocity = transform.right * Time.deltaTime * 300;
		}
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.Play("idle");     
        }

        // Move Left 
        if (Input.GetKey(KeyCode.LeftArrow))
		{
			right = false;
			transform.localPosition += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            animator.Play("walking-side");
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.Play("idle");
        }

        
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
