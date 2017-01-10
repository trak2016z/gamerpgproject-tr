using System;
using System.Collections;
using UnityEngine;

public interface IEnemies
{
	bool HaveActualySomethingToDo{ get; set; }

	bool IsAlive{ get; set; }

	void UpdateEnemiesBehaviour ();

	IEnumerator DeadAnimation ();

	IEnumerator SpawnAnimation ();

	void Initialize (Transform playerTransform);
}


