using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMove3D : MonoBehaviour{

	public float speedIncrement = 0.1f;
	public float maxSpeed = 10f;
	public bool worldSpace = false;
	Vector3 velocity = Vector3.zero;

	public enum DemoMoveDirection{
		Forward,	Backward,
		Left,	Right,
		Up,	Down
	}
	
	// Update is called once per frame
	void Update(){
		transform.Translate(velocity * Time.deltaTime, worldSpace ? Space.World : Space.Self);
	}

	public void Accelerate(int direction){
		Accelerate((DemoMoveDirection)direction);
	}

	public void Accelerate(DemoMoveDirection direction){
		switch(direction){
			case DemoMoveDirection.Forward:
				velocity.z += speedIncrement;
				break;
			case DemoMoveDirection.Backward:
				velocity.z -= speedIncrement;
				break;
			case DemoMoveDirection.Left:
				velocity.x -= speedIncrement;
				break;
			case DemoMoveDirection.Right:
				velocity.x += speedIncrement;
				break;
			case DemoMoveDirection.Up:
				velocity.y += speedIncrement;
				break;
			case DemoMoveDirection.Down:
				velocity.y -= speedIncrement;
				break;
			default:
				break;
		}
		if(velocity.sqrMagnitude > maxSpeed * maxSpeed)
			velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
	}

	public void Stop(){
		velocity = Vector3.zero;
	}
}
