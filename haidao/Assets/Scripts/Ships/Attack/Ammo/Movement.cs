using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	private Transform theTransform = null;
	public float maxSpeed = 2.0f;

	void Awake() {
		theTransform = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		theTransform.position += theTransform.forward * maxSpeed * Time.deltaTime;
	}
}
