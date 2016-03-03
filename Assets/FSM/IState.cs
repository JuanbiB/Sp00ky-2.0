using UnityEngine;
using System.Collections;

public interface IState
{
	//Update function
	void UpdateState();

	//Transitions 
	void ToGuardState();

	void ToPatrolState();
	
	void ToAlertState();
	
	void ToChaseState();

	void ToDistractedState();

	void ToTrailState();

	//Collider business
	void OnTriggerEnter (Collider other);
}
