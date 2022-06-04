using UnityEngine;
using System.Collections;
namespace Scingularity{

[System.Serializable]
[UnityEngine.ExecuteInEditMode]
public partial class DisableAllFrustumCulling : MonoBehaviour{

	new public bool enabled = true;
	public float distance = 500f;
	public bool executeInEditMode;
	private Camera target;
	private bool wasOrthographic;
	private float oldSize = 1f;
	private Vector3 oldPosition;
	private float oldFarClipPlane = 500f;

	public virtual void OnPreCull(){
		if(!enabled)	return;
		if(!executeInEditMode && !Application.isPlaying)	return;
		if(!target)	target = GetComponent<Camera>();
		if(!target){
			Debug.LogWarning("The Disable All Frustum Culling script must be attached to a Camera.");
			enabled = false;
			return;
		}
		wasOrthographic = target.orthographic;
		oldSize = target.orthographicSize;
		oldPosition = transform.position;
		oldFarClipPlane = target.farClipPlane;
		if((Time.frameCount < 1) && Application.isPlaying)	return;
		target.orthographic = true;
		target.orthographicSize = distance;
		target.farClipPlane = distance * 2f;
		transform.position = transform.position - (transform.forward * (distance + target.nearClipPlane));
	}

	public virtual void OnPreRender(){
		if(!enabled)	return;
		if(!target)	return;
		if(!executeInEditMode && !Application.isPlaying)	return;
		if((Time.frameCount < 1) && Application.isPlaying)	return;
		target.orthographicSize = oldSize;
		target.orthographic = wasOrthographic;
		target.farClipPlane = oldFarClipPlane;
		transform.position = oldPosition;
	}
}
}