using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField] Behaviour[] componentsToDisable;
	[SerializeField] string localLayerName = "LocalPlayer";
	[SerializeField] string remoteLayerName = "RemotePlayer";

	Camera sceneCamera;

	void Start()
	{
		if (!isLocalPlayer)
		{
			DisableComponents();
			AssignRemoteLayer();
		}

		else {
			gameObject.layer = LayerMask.NameToLayer(localLayerName);
			sceneCamera = Camera.main;
			if (sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
			}

		}
	}

	void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void DisableComponents()
	{
		for(int i = 0; i < componentsToDisable.Length; i++)
		{
			componentsToDisable[i].enabled = false;
		}
	}

	void OnDisable()
	{
		if (sceneCamera != null)
		{
			sceneCamera.gameObject.SetActive(true);
		}
	}
}
