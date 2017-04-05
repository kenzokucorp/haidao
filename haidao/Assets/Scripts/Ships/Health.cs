using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public GameObject deathParticlesPrefab = null;
	private Transform theTransform = null;
	public bool shouldBeDestroyOnDeath = true;
	public bool shouldShowGameOverOnDeath = false;

	public float healthPoints{
		get {
			return _healthPoints;
		}

		set {
			_healthPoints = value;

			if (_healthPoints <= 0) {
				SendMessage ("Die", SendMessageOptions.DontRequireReceiver);

				if (deathParticlesPrefab != null) {
					Instantiate (deathParticlesPrefab, theTransform);
					if(shouldBeDestroyOnDeath) {
						Destroy (gameObject);
					}
				}

				if (shouldShowGameOverOnDeath) {
					GameController.GameOver ();
				}
			}
		}

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[SerializeField]
	private float _healthPoints = 100.0f;
}
