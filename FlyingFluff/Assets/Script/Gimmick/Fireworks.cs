using UnityEngine;
using System.Collections;

public class Fireworks : MonoBehaviour {
	private bool update = false;

	void Awake () {
		GetComponent<ParticleSystem> ().Stop ();
	}

	void Update () {
	
	}

	private void OnWillRenderObject(){
		if (Camera.current.tag == "MainCamera") {
			if (update != true) {
				GetComponent<ParticleSystem> ().Play ();
				update = true;
			}
		}
	}
}
