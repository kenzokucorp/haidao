using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

	public float damage = 100.0f;
	public float lifetime = 1.0f;

	void onEnable() {
		CancelInvoke ();
		Invoke ("Die", lifetime);
	}

	void OnTriggerEnter(Collider collider){
		Health health = collider.gameObject.GetComponent<Health> ();

		if (health == null) {
			return;
		}

		health.healthPoints -= damage;
	}

	void Die() {
		gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
