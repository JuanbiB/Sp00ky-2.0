using UnityEngine;
using System.Collections;

public class TrailState : IState 

{
	private Transform target;
	private bool start = true; 
	private readonly Guard enemy;

	public GameObject nextTile;
	public GameObject curTile;
	public bool onEnter = true;


	GameObject playerObject;
	Character player;

	public bool locked = false;

	public int index;

	public int counter = -4;

	public TrailState (Guard guard)
	{
		//managerObject = GameObject.Find ("manager");
 		//manager = managerObject.gameObject.GetComponent<GameManager> ();
		enemy = guard;


		playerObject = GameObject.FindGameObjectWithTag ("Player");
		player = playerObject.GetComponent<Character> ();
	}

	public void UpdateState()
	{
		//Look ();
		Follow ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Cover") {
			enemy.distractedState.start = Time.time;
			ToDistractedState ();
		}
		//Debug.Log ("Test");

	}

	public void ToGuardState()
	{
		//Can't access - Have to go through alert.
	}


	public void ToPatrolState()
	{
		enemy.currentState = enemy.patrolState;
	}

	public void ToAlertState()
	{
		//enemy.currentState = enemy.alertState;
	}

	public void ToChaseState()
	{
		enemy.currentState = enemy.chaseState;
	}

	public void ToDistractedState ()
	{
        GameObject manager = GameObject.Find("manager");
        enemy.distractedState.currentTurn = manager.GetComponent<GameManager>().turnsPassed;
        enemy.currentState = enemy.distractedState;
	}

	public void ToTrailState ()
	{

	}

	private void Look()
	{
		//TODO Max's code here.
	}

	private void Follow()
	{
		//enemy.transform.position = new Vector3(enemy.transform.position.x, .1f, enemy.transform.position.z);

		//index = 0;

		if(onEnter){
			Debug.Log ("on enter");

		
			for (int i = 0; i < enemy.manager.scentList.Count; i++) {
				if (enemy.manager.scentList [i].Equals (curTile)) {
					Debug.Log ("Equals!");
					index = i;
					Debug.Log ("index" + i);
				}
			}
			//int index = enemy.manager.scentList. (curTile);
			nextTile = curTile;
			nextTile.transform.position = new Vector3 (nextTile.transform.position.x, nextTile.transform.position.y + .3f, nextTile.transform.position.z);
			onEnter = false;
		}


		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)
			|| Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (locked == false) {
				counter++;
				Debug.Log ("index " + index);

				//	Debug.Log ("stuff " + index);
				if (counter > 1) {
					index = index + 2;
				} else {
					index++;
				}

				if (index > enemy.manager.scentList.Count) {
					nextTile = enemy.manager.playerObject;
				} else {
					nextTile = enemy.manager.scentList [index];
					nextTile.transform.position = new Vector3 (nextTile.transform.position.x, nextTile.transform.position.y + .3f, nextTile.transform.position.z);
				}
				locked = true;
			}

		}

		Vector3 calculations = enemy.transform.position - nextTile.transform.position;

		if (calculations.magnitude == 0f) {
			locked = false;
		}


		enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, nextTile.transform.position, 0.01f);
	}
		
}