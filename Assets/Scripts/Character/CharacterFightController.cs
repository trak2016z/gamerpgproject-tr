using UnityEngine;
using System.Collections;
using Items;

public class CharacterFightController : MonoBehaviour
{

	CharacterPanelWeaponSlot _weaponSlot;
	TrailRenderer _renderer;
	Weapon _leftHandWeapon;

	public bool IsFighting{ get; set; }

	public float TimeOfAttack{ get; set; }

	public Vector3 StartAttackPosition { get; set; }

	public float RotateSpeed{ get; set; }

	public float RotateDeaceleration = 0.5f;

	// Use this for initialization
	void Start ()
	{
		_weaponSlot = FindObjectOfType<CharacterPanelWeaponSlot> ();
		_renderer = GetComponentInChildren<TrailRenderer> ();
		_renderer.sortingOrder = 127;
		_renderer.gameObject.SetActive (false);
		RotateSpeed = 0;
		_leftHandWeapon = null;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_leftHandWeapon == null) {
			return;
		}
		
		if (IsFighting) {
			TimeOfAttack += Time.deltaTime;
		}
		if (Input.GetMouseButtonDown (0)) {
			StartAttackPosition = Input.mousePosition;
			TimeOfAttack = 0;
			IsFighting = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			var distanceVector = (StartAttackPosition - Input.mousePosition);
			var distance = ((distanceVector.x + distanceVector.y) - (-1)) / (1 - (-1));
			if (distance == 0 || TimeOfAttack == 0) { 
				return;
			}
			RotateSpeed = distance * 1 / TimeOfAttack * 0.02f;
			Debug.Log (distance.ToString () + "  " + TimeOfAttack.ToString () + "   " + RotateSpeed.ToString ());
			IsFighting = false;
			_renderer.gameObject.SetActive (true);
		}

		if (RotateSpeed != 0) {
			RotateSpeed *= RotateDeaceleration;
			if (RotateSpeed > -0.01 && RotateSpeed < 0.01) {
				RotateSpeed = 0;
				_renderer.gameObject.SetActive (false);
			}
			transform.Rotate (Vector3.forward * RotateSpeed);
		}
	}

	public void RequestUpdateOfWeapon ()
	{
		if (_weaponSlot != null) {
			if (_weaponSlot._weapon == null) {
				GetComponentInChildren<SpriteRenderer> ().sprite = null;
				return;
			}

			if (_leftHandWeapon == (_weaponSlot._weapon as Weapon)) {
				return;
			}
			Debug.Log ("Zmieniam bron");
			_leftHandWeapon = _weaponSlot._weapon as Weapon;
			Destroy (GetComponentInChildren<PolygonCollider2D> ());
			GetComponentInParent<SpriteRenderer> ().sprite = _leftHandWeapon.getImage ();
			gameObject.AddComponent<PolygonCollider2D> ();

		} else {
			Debug.Log ("Weapon Slot Null Exception");
		}
	}
}
