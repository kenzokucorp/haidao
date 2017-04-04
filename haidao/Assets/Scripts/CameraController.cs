using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public bool canControl = true;
	public float reticuleSize = 15.0f;
	public Vector3 cameraRotationPoint_normal;
	public Vector3 cameraRotationPoint_aiming;
	public Vector3 cameraRotationPoint_crouching_aiming;

	public Vector3 camera_PositionOffset_normal;
	public Vector3 camera_PositionOffset_aiming;
	public Vector3 camera_PositionOffset_crouching_aiming;

	public float speed_offsetChange = 15.0f;

	[HideInInspector]	public float x = 0.0f, y = 0.0f;
	[HideInInspector]	public Quaternion cRotation;
	[HideInInspector]	public Vector3 cPosition;
	[HideInInspector]	public Vector3 cameraRotationPoint_current;
	[HideInInspector]	public Vector3 cameraPositionOffset_current;
	[HideInInspector]	public Transform target;

	ShipController owner;
	Texture2D reticule;

	void Awake() {
		reticule = (Texture2D)Resources.Load("GUITextures/ScreeenCenter_1");
	}

	public Vector3 CalculateCameraPosition(Vector3 _rotPoint, Vector3 _posOffset, Quaternion _rotation){
		return  target.position + _rotation * new Vector3(_posOffset.x, _posOffset.y, -_posOffset.z) + new Vector3(_rotPoint.x, _rotPoint.y, _rotPoint.z);
	}

	void OnGUI(){
		Rect position = new Rect((Screen.width - reticuleSize) * 0.5f, (Screen.height - reticuleSize) * 0.5f, reticuleSize, reticuleSize);
		//if(owner.aimingWeightControl > 0.95f){
		//GUI.DrawTexture(position, reticule);
		//}
	}

	public void OnPlayerSpawn(GameObject player){
		target = player.transform;
		owner = player.GetComponent<ShipController>();
		owner.c = gameObject.GetComponent<CameraController>();
		cameraRotationPoint_current = cameraRotationPoint_normal;
		cameraPositionOffset_current = camera_PositionOffset_normal;
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}
}
