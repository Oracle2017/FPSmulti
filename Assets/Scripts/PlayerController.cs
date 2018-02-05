using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

	[SerializeField] float speed = 5f;
	[SerializeField] float lookSensitivity = 3f;
	[SerializeField] float thrusterForce = 1000;
	ConfigurableJoint joint;

	PlayerMotor motor;

	[Header("Spring settings")]
	[SerializeField] JointDriveMode jointMode = JointDriveMode.Position;
	[SerializeField] float jointSpring = 20;
	[SerializeField] float jointMaxForce = 40;

	void Start()
	{
		motor = GetComponent<PlayerMotor>();
		joint = GetComponent<ConfigurableJoint>();
		SetJoinSettings(jointSpring);
	}

	void Update()
	{
		// Player Movement
			float _xMov = Input.GetAxisRaw("Horizontal");
			float _zMov = Input.GetAxisRaw("Vertical");

			Vector3 _movHorizontal = transform.right * _xMov;
			Vector3 _movVertical = transform.forward * _zMov;

			Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

			motor.Move(_velocity);

		// Player Rotation
			float _yRot = Input.GetAxisRaw("Mouse X");

			Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

			motor.Rotate(_rotation);

		// Camera Rotation
			float _xRot = Input.GetAxisRaw("Mouse Y");

			float _cameraRotationX = _xRot * lookSensitivity;

			motor.RotateCamera(_cameraRotationX);

		// Calculate thruster force based on player input
			Vector3 _thrusterForce = Vector3.zero;

			// Apply thruster force
			if (Input.GetButton("Jump"))
			{
				_thrusterForce = Vector3.up * thrusterForce;
				SetJoinSettings(0f);
			}

			else 
			{
				SetJoinSettings(jointSpring);
			}

			motor.ApplyThruster(_thrusterForce);
	}

	void SetJoinSettings(float _jointSpring)
	{
		joint.yDrive = new JointDrive 
		{
			mode = jointMode, 
			positionSpring = _jointSpring, 
			maximumForce = jointMaxForce 
		};
	}
}
