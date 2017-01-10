using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemiesSpawner : MonoBehaviour {

	int MAX_MONSTERS_ON_MAP = 10;
	float AfterDeadSpawn = 2.5f;
	protected List<NozownikController> _monstersPulling = new List<NozownikController>();
	float _radius = 10.0f;
	float idleSpawnTime = 1.5f;
	float ActualTime = 0;
	public int actualNumberOfMonsters = 0;

	Transform _playerPosition;

	public NozownikController _monster;

	// Use this for initialization
	void Start () {
		for (int a = 0; a < MAX_MONSTERS_ON_MAP + 10; a++) {
			var tmp = Instantiate (_monster);
			tmp._isDead = true;
			_monstersPulling.Add (tmp);
		}
		_playerPosition = Transform.FindObjectOfType<CharacterControll> ().transform;
		StartCoroutine (checkMonsterForDistance ());
		ActualTime = idleSpawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (actualNumberOfMonsters < MAX_MONSTERS_ON_MAP) {
			if (ActualTime <= 0) {
				StartCoroutine (spawnNewMonster ());
				ActualTime = idleSpawnTime;
			}
			ActualTime -= Time.deltaTime;
		}
	}

	protected IEnumerator spawnNewMonster(){
		yield return new WaitForSeconds (AfterDeadSpawn);
		Debug.Log ("Monster Spawning");
		float angle = Random.value * Mathf.PI * 2;
		Vector3 position = new Vector3(Mathf.Cos(angle) * _radius, Mathf.Sin(angle) * _radius, 0) + _playerPosition.position;
		var lastGameObject = FindFirstUnActiveObject ();
		if (lastGameObject != null) {
			lastGameObject.InitializeMonsterData ();
			lastGameObject.transform.position = position;
			lastGameObject.gameObject.SetActive (true);
			actualNumberOfMonsters++;
		}
	}

	protected NozownikController FindFirstUnActiveObject(){
		return _monstersPulling.FirstOrDefault (x => x._isDead == true);
	}

	protected IEnumerator checkMonsterForDistance(){
		while (true) {
			yield return new WaitForSeconds (1.2f);

			for (int a = 0; a < _monstersPulling.Count; a++) {
				if (Vector3.Distance (_monstersPulling[a].transform.position,_playerPosition.position) > 30.0f) {
					_monstersPulling [a]._isDead = true;
					_monstersPulling [a].gameObject.SetActive (false);
					actualNumberOfMonsters = Mathf.Clamp (actualNumberOfMonsters--, 0, 100000);
				}
			}

		}
	}

}
