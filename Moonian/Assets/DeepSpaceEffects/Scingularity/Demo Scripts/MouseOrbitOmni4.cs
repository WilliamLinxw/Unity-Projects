using UnityEngine;
using System.Collections;
namespace Scingularity{

[System.Serializable]
[UnityEngine.AddComponentMenu("Camera-Control/Mouse Orbit Omni v4")]
public partial class MouseOrbitOmni4 : MonoBehaviour{

	public Transform target;
	public float speed = 1f;
	public float distance = 10f;
	public Vector3 offset = Vector3.zero;
	public KeyCode key = KeyCode.None;
	public bool follow = true;
	[UnityEngine.Tooltip("Change rotation while orbiting as opposed to only changing position. This should usually be true.")]
	public bool rotate = true;
	[UnityEngine.Tooltip("Camera up direction. Set to (0, 0, 0) to disable.")]
	public Vector3 up = Vector3.up;
	private Quaternion offsetRotation = Quaternion.identity;
	private Quaternion delta = Quaternion.identity;
	private float oldDistance = 0f;

	public virtual void Update(){
		if(!follow && key != KeyCode.None && !Input.GetKey(key) && oldDistance == distance)	return;
		if(Input.GetKeyDown(key))	return;
		
		oldDistance = distance;
		Vector3 targetPosition = (target == null) ? Vector3.zero : target.position;
		targetPosition += offset;
		float s = speed * (Application.platform == RuntimePlatform.WebGLPlayer ? 0.25f : 1f);
		delta = Quaternion.Euler(-Input.GetAxis("Mouse Y") * s, Input.GetAxis("Mouse X") * s, 0f);
		
		if(follow)	transform.position = (offsetRotation * new Vector3(0, 0, -distance)) + targetPosition;
		if(rotate)	transform.rotation = offsetRotation;

		if(targetPosition != transform.position){
			offsetRotation = Quaternion.LookRotation(targetPosition - transform.position, (up == Vector3.zero ? transform.up : up));
			if(Input.GetKey(key))	offsetRotation *= delta;
		}
	}

	public void Zoom(float distance){
		this.distance = distance;
	}

	public virtual void Orient(Vector3 up){
		this.up = up;
		transform.rotation = Quaternion.LookRotation(transform.forward, up);
	}

	public virtual void Orient(Transform gravitySource){
		Orient(transform.position - gravitySource.position);
	}
}
}