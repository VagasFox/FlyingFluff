using UnityEngine;
using System.Collections;

public class Seagull : MonoBehaviour {
	[SerializeField] private AnimationCurve curve;
	[SerializeField] private float speed;
	private GameObject player;
	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		float moveX = speed*Time.deltaTime;

		this.transform.position += new Vector3 (moveX, curve.Evaluate (Time.time/1.5f), 0);
	}
}
