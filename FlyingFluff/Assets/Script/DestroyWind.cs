using UnityEngine;
using System.Collections;

public class DestroyWind : MonoBehaviour {
	public bool instanceFlag = false;
	public float destoryTimer = 0f;
	private float timer = 0f;

	void Update () {
		if (instanceFlag == true) {
			timer += Time.deltaTime;

			if (timer > destoryTimer) {
				timer = 0f;
				Destroy (this.gameObject);
			}
		}
	}
}
