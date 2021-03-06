﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	Vector3 rotation = Vector3.zero;
	float cameraRotationX = 0f;
	float currentCameraRotationX = 0f;
	Vector3 thrusterForce = Vector3.zero;

	Rigidbody rb;
	Transform playerCamera;

	[SerializeField] float cameraRotationLimit = 85f;

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

	public void RotateCamera(float _cameraRotation)
	{
		cameraRotationX = _cameraRotation;
	}

	public void ApplyThruster(Vector3 _thrusterForce)
	{
		thrusterForce = _thrusterForce;
	}

	void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
		PerformCameraRotation();
		PerformThrusterForce();
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
		if (playerCamera != null)
		{
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			playerCamera.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}

	void PerformThrusterForce()
	{
		if (thrusterForce != Vector3.zero)
		{
			rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}
	}
}
