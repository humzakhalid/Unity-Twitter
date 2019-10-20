using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetString ("DeletePrefab") == "YES") {
			Destroy (this.gameObject);

		}	
	}
	void OnDisable()
	{
		Destroy (this.gameObject);
	}

}
