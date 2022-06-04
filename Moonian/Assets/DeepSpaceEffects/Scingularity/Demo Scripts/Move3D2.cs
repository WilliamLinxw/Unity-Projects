using UnityEngine;
using System.Collections;
[System.Serializable]
public partial class Move3D2 : MonoBehaviour{
	public float speed;
	public string AxisX;
	public string AxisY;
	public string AxisZ;
	[UnityEngine.Tooltip("Whether to prevent movement through colliders.\nSet to false to ignore colliders or true to simulate a rigid body.")]
	public bool detectCollisions;
	public virtual void Update(){
		Vector3 oldPosition = this.transform.position;
		this.transform.Translate((Time.deltaTime * this.speed) * new Vector3(Input.GetAxis(this.AxisX), Input.GetAxis(this.AxisY), Input.GetAxis(this.AxisZ)));
		if(!this.detectCollisions){
		return;
		}
		float d = (this.transform.position - oldPosition).magnitude * 2;
		foreach (Collider hitCollider in Physics.OverlapSphere(oldPosition, d)){
		if(hitCollider.enabled){
			this.transform.Translate((hitCollider.bounds.center - this.transform.position).normalized * -d);
		}
		}
	}
	public Move3D2(){
		this.speed = 1f;
		this.AxisX = "Horizontal";
		this.AxisY = "Jump";
		this.AxisZ = "Vertical";
	}
}