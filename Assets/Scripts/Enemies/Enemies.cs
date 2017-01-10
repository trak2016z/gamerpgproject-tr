using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(Collider2D))]
public class Enemies : MonoBehaviour, IEnemies
{

	protected Transform _playerTransform;

	#region IEnemies implementation

	public void UpdateEnemiesBehaviour ()
	{
		
	}

	public void Initialize (Transform playerTransform){
		_playerTransform = playerTransform;
	}

	public IEnumerator DeadAnimation ()
	{
		var spriteRenderer = GetComponent<SpriteRenderer> ();
		for (float t = 1.0f; t > 0.0f; t += Time.deltaTime/3.0f) {
			var newColour = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp (0.0f, 1.0f, t));
			spriteRenderer.color = newColour;
			if (spriteRenderer.color.a >= 1.0)
				break;
			yield return new WaitForFixedUpdate ();

		}
		Destroy (gameObject);
	}

	public IEnumerator SpawnAnimation ()
	{
		float spawnSpeed = 1.0f;
		var spriteRenderer = GetComponent<SpriteRenderer> ();
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime) {
			var newColour = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp (0.0f, 1.0f, t));
			spriteRenderer.color = newColour;
			if (spriteRenderer.color.a >= 1.0)
				break;
			yield return new WaitForFixedUpdate ();
			
		}

		IsAlive = true;
	}

	public bool HaveActualySomethingToDo { get; set; }

	public bool IsAlive { get; set; }

	#endregion
}
