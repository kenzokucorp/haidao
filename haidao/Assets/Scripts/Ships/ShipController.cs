using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour {

	public static ShipController ship = null;

	public bool canMove = true;

	public float maxSpeed = 2000.0f;
	public float rotationSpeed = 2000.0f;

	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public string fireAxis = "Fire1";

	private Rigidbody theRigidbody = null;
	private Transform theTransform = null;

	public CameraController c;
	float horizontal;
	float vertical;

	public Transform[] weaponTransforms;
	private bool canFire = true;
	float reloadDelay = 2.0f;

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
		c.OnPlayerSpawn (gameObject);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private void ChangeDirection() {
		Vector3 localScale = theTransform.localScale;
		localScale.x *= -1.0f;
		theTransform.localScale = localScale;
	}

	void FixedUpdate () {
		if (!canMove || Health <= 0)
			return;

		horizontal = CrossPlatformInputManager.GetAxis (horizontalAxis);
		vertical = CrossPlatformInputManager.GetAxis (verticalAxis);

		theRigidbody.AddTorque(0f,horizontal*rotationSpeed*Time.deltaTime,0f);
		theRigidbody.AddForce(transform.forward*vertical*maxSpeed*Time.deltaTime);

		if (Input.GetButtonDown (fireAxis) && canFire) {
			foreach (Transform transform in weaponTransforms) {
				AmmoManager.SpawnAmmo (transform.position, transform.rotation);
			}
			canFire = false;
			Invoke ("EnableFire", reloadDelay);
		}
	}

	void EnableFire() {
		canFire = true;
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

	void UpdateCameraMovement(){
		if(c.canControl){
			//c.x += mouseX * mouseXSens;
			//c.y -= mouseY * mouseYSens;

			//c.y = V_LookAngle;

			//c.cameraRotationPoint_current = Vector3.MoveTowards(c.cameraRotationPoint_current, IS_AIMING ? (IS_CROUCHING ? c.cameraRotationPoint_crouching_aiming : c.cameraRotationPoint_aiming) : c.cameraRotationPoint_normal, Time.deltaTime * c.speed_offsetChange);
			//c.cameraPositionOffset_current = Vector3.MoveTowards(c.cameraPositionOffset_current, IS_AIMING ? (IS_CROUCHING ? c.camera_PositionOffset_crouching_aiming : c.camera_PositionOffset_aiming) : c.camera_PositionOffset_normal, Time.deltaTime * c.speed_offsetChange);

			c.cameraRotationPoint_current = Vector3.MoveTowards(c.cameraRotationPoint_current, c.cameraRotationPoint_normal, Time.deltaTime * c.speed_offsetChange);
			c.cameraPositionOffset_current = Vector3.MoveTowards(c.cameraPositionOffset_current, c.camera_PositionOffset_normal, Time.deltaTime * c.speed_offsetChange);

			c.cRotation = Quaternion.Euler(c.y, c.x, 0);
			c.cPosition = c.CalculateCameraPosition(c.cameraRotationPoint_current, c.cameraPositionOffset_current, c.cRotation);
			c.transform.position = c.cPosition;
			c.transform.rotation = c.cRotation;
		}
	}

	void LateUpdate(){
		UpdateCameraMovement();
	}
}
