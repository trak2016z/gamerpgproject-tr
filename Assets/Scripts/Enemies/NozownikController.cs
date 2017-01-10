using UnityEngine;
using System.Collections;

public class NozownikController : MonsterController{

	public NozownikController(){
		Health=30f;
		MoveSpeed = 1f;
		Experience=30f;
		Damage=5f;
		agroRange = 8f;
		attackRange = 1f;
		actionTimer = 0f;
		attackTime = 0.5f;
	}

	public override void InitializeMonsterData ()
	{
		Health=30f;
		MoveSpeed = 1f;
		Experience=30f;
		Damage=5f;
		agroRange = 8f;
		attackRange = 1f;
		actionTimer = 0f;
		attackTime = 0.5f;

		transform.GetChild (0).gameObject.SetActive (false);
		GetComponent<Collider2D> ().enabled = true;
		_isDead = false;
	}
}
