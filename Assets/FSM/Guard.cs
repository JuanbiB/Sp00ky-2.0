using UnityEngine;
using System.Collections;

/*
This is the script you actually attach to a guard game object.
Creates all the states.
*/
public class Guard : MonoBehaviour 
{
	public float searchingTurnSpeed = 120f;
	public float searchingDuration = 4f;
	public float speed = 4.5f; 
	public GameObject[] wayPoints;

	public GameObject managerObject;
	public GameManager manager;

	public Animator animation;

	public Rigidbody rigid_body;
	public BoxCollider our_collider;


	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public IState currentState;
	[HideInInspector] public PatrolState patrolState;
//	[HideInInspector] public AlertState alertState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public DistractedState distractedState;
	[HideInInspector] public TrailState trailState;

	private void Awake()
	{
		//guardState = new GuardState (this);
		patrolState = new PatrolState (this);
		//alertState = new AlertState (this);
		chaseState = new ChaseState (this);
		distractedState = new DistractedState (this);
		trailState = new TrailState (this);
	}
	
	// Use this for initialization
	void Start () 
	{
		this.gameObject.AddComponent<Rigidbody> ();
		rigid_body = this.gameObject.GetComponent<Rigidbody> ();
		rigid_body.useGravity = false;

		our_collider = this.gameObject.GetComponent<BoxCollider> ();
		our_collider.size = new Vector3 (0.1f, 0.1f, 0.1f);

		animation = this.gameObject.GetComponent<Animator> ();
		managerObject = GameObject.Find("manager");
		Debug.Log (managerObject);
		//managerObject = GameObject.Find("manager");
		manager = managerObject.gameObject.GetComponent<GameManager> ();
	
		currentState = patrolState;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		currentState.UpdateState ();
    }
	
	private void OnTriggerEnter(Collider other)
	{
		print ("ballsack");
		currentState.OnTriggerEnter (other);
	}

}