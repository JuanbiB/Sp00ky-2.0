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

		managerObject = GameObject.FindGameObjectWithTag ("GameController");
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
		currentState.OnTriggerEnter (other);
	}

}