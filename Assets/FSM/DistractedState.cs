using UnityEngine;
using System.Collections;

public class DistractedState : IState 
	
{
	public float start;
	private readonly Guard enemy;
	private float delayTime = 3.0f;
    public int currentTurn;

	public DistractedState (Guard guard)
	{
		enemy = guard;
	}
	
	public void UpdateState()
	{
		Busy();
	}
	
	public void OnTriggerEnter (Collider other)
	{
	
	}
	
	public void ToGuardState()
	{
		//enemy.currentState = enemy.guardState;
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
		//enemy.currentState = enemy.chaseState;
	}

	public void ToDistractedState()
	{
		//Can't access
	}

	public void ToTrailState()
	{

	}

	private void Look()
	{
		//TODO Max's code here. 
	}
	
	private void Busy()
	{
        GameObject manager = GameObject.Find("manager");
        if (manager.GetComponent<GameManager>().turnsPassed - currentTurn > 3)
        {
            ToPatrolState();
        }
		
	}
}