using UnityEngine;
using System.Collections;
using Character;
public abstract class MonsterController : MonoBehaviour {

	protected Transform _characterPosition;
	//protected GameObject actionController;
	protected  CharacterDataController _characterDataController;

	protected float Health=50f;
	protected float MoveSpeed = 5f;
	protected float Experience=15f;
	protected float Damage=20f;

	protected float agroRange = 8f;
	protected float attackRange = 1.5f;

	protected float actionTimer;
	protected float attackTime = 0.5f;
	protected bool isAggresive=false;
	public bool _isDead = false;
	protected Rigidbody2D _monsterBody;
	protected Animator _monsterAnimator;
	// Use this for initialization
	protected void Start () {
		//actionController = GameObject.Find ("ActionController");
		_characterPosition = Transform.FindObjectOfType<CharacterControll>().transform;
		_monsterBody = GetComponent<Rigidbody2D> ();
		_characterDataController = FindObjectOfType<CharacterDataController> ();
		_monsterAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	protected void FixedUpdate () {
		if (!_isDead) {
			var distance = Vector2.Distance (transform.position, _characterPosition.position);
			if (distance < attackRange && actionTimer <= 0) {
				actionTimer = attackTime;
				_monsterBody.velocity = new Vector2 ();
				_characterDataController.SetDamage (Damage);
				Debug.Log ("DAMAGING");
				//_actionController.AddDamage (Damage);
				_monsterAnimator.SetBool ("IsMoving", false);
			} else if (((distance < agroRange) || isAggresive) && actionTimer <= 0) {
				Vector3 dir = _characterPosition.position - transform.position; 
				float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg; 
				Quaternion q = Quaternion.AngleAxis (angle - 90, Vector3.forward); 
				transform.rotation = Quaternion.Slerp (transform.rotation, q, 0.5f);
				_monsterBody.velocity = transform.up * MoveSpeed;
				_monsterAnimator.SetBool ("IsMoving", true);

			} else {
				_monsterAnimator.SetBool ("IsMoving", false);
				_monsterBody.freezeRotation = true;
			}
		} else {
			_monsterBody.velocity = _monsterBody.velocity * 0.1f;
			_monsterAnimator.SetBool ("IsMoving", false);
		}
	}

	protected void Update(){
		if (actionTimer > 0) {
			actionTimer -= Time.deltaTime;
		}
		if (Health <= 0&&!_isDead) {
			_isDead = true;
			//_actionController.AddExperience (Experience);
			//Destroy (gameObject, 1.5f);
			transform.GetChild (0).gameObject.SetActive (true);
			GetComponent<Collider2D> ().enabled = false;
			OnDead ();
		}
	}

	public void setDamage(float value){
		value = Mathf.Abs (value);
		Health -= value;
		isAggresive = true;
		Health = Mathf.Clamp(Health,0f,float.MaxValue);
		Debug.Log (Health.ToString ());
	}

	abstract public void InitializeMonsterData ();

	public void OnDead(){
		var obj = GameObject.FindObjectOfType<EnemiesSpawner> ();
		if (obj != null) {
			obj.actualNumberOfMonsters--;
		}
	}
}
