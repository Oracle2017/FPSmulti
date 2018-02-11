using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {
	string PLAYER_TAG = "Player";

	public PlayerWeapon weapon;

	[SerializeField] Camera cam;
	[SerializeField] LayerMask mask;

	// Use this for initialization
	void Start () {
		if (cam == null)
		{
			Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	// local method, only called on the client never on the server
	[Client]
	void Shoot()
	{
		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
		{
			if (_hit.collider.tag == PLAYER_TAG)
			{
				CmdPlayerShot(_hit.collider.name);
				//Debug.Log("We hit " + _hit.collider.name);
			}
		}
	}

	// Methods that are only called on the server
	[Command]
	void CmdPlayerShot(string _ID)
	{
		Debug.Log(_ID + " has been shot.");
	}
}
