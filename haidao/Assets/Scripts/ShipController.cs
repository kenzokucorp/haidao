using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour {

	public static ShipController ship = null;

	public bool canMove = true;
	public bool isOnTheGround = false;

	public float maxSpeed = 2000.0f;
	public float rotationSpeed = 2000.0f;

	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";

	public CapsuleCollider feetCollider = null;
	public LayerMask groundLayer;

	private Rigidbody theRigidbody = null;
	private Transform theTransform = null;

	public static float Health {
		get { 
			return _health;
		}
		set { 
			_health = value;
			if (_health <= 0) {
				Die ();
			}
		}
	}

	[SerializeField]
	private static float _health = 1.0f;

	void Awake () {
		ship = this;
		theRigidbody = GetComponent<Rigidbody> ();
		theTransform = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private void ChangeDirection() {
		//direction = (FACE_DIRECTION)((int)direction * -1.0f);
		Vector3 localScale = theTransform.localScale;
		localScale.x *= -1.0f;
		theTransform.localScale = localScale;
	}

	void FixedUpdate () {
		if (!canMove || Health <= 0)
			return;

		float horizontal = CrossPlatformInputManager.GetAxis (horizontalAxis);
		float vertical = CrossPlatformInputManager.GetAxis (verticalAxis);

		theRigidbody.AddTorque(0f,horizontal*rotationSpeed*Time.deltaTime,0f);
		theRigidbody.AddForce(transform.forward*vertical*maxSpeed*Time.deltaTime);
	}

	void OnDestroy() {
		ship = null;
	}

	static void Die() {
		Destroy (ShipController.ship.gameObject);
	}

	public static void Reset() {
		Health = 100.0f;
	}
}
