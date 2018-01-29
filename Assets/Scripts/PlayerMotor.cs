using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	Vector3 rotation = Vector3.zero;
	Vector3 cameraRotation = Vector3.zero;
	Rigidbody rb;
	Transform playerCamera;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		playerCamera = GetComponentInChildren<Camera>().transform;
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	public void RotateCamera(Vector3 _cameraRotation)
	{
		cameraRotation = _cameraRotation;
	}

	void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
		PerformCameraRotation();
	}

	void PerformMovement()
	{
		if (velocity != Vector3.zero)
		{
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
	}

	void PerformCameraRotation()
	{
		playerCamera.Rotate(-cameraRotation);
	}
}
