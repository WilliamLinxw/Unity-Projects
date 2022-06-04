using UnityEngine;
using System.Collections;
[System.Serializable]
[UnityEngine.AddComponentMenu("Camera-Control/MouseLook Omni 2")]
public partial class MouseLookOmni2 : MonoBehaviour{
	public float speed = 10;
	public bool lockMouse = true;
	public KeyCode key;

	public void Start(){
		if(this.lockMouse && (this.key == KeyCode.None)){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		speed *= (Application.platform == RuntimePlatform.WebGLPlayer ? 0.25f : 1f);
	}

	public void Update(){
		if(Input.GetKeyDown(key))	return;	// Prevents sudden "jerk" on touchscreen devices
		
		if((this.key == KeyCode.None) || Input.GetKey(this.key)){
			if(Input.GetKeyDown(key))	return;
			this.transform.Rotate(-Input.GetAxisRaw("Mouse Y") * this.speed, Input.GetAxisRaw("Mouse X") * this.speed, 0);
		}
	}
}