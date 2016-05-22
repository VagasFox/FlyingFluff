using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowingCamera : MonoBehaviour {
	private Transform target;
	[SerializeField] private float smoothing = 5f;
	Vector3 offset;

	void Start(){
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		offset = transform.position - target.position;

	}

	void Update(){
		LimitCameraPos ();
	}

	void FixedUpdate(){
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

	//カメラの移動制限
	void LimitCameraPos(){
		Vector3 pos = transform.position;
		if(pos.x < 0.75f) transform.position = new Vector3 (0.75f, 
															transform.position.y, 
															transform.position.z);
		if (pos.x > 412f) transform.position = new Vector3 (412f, 
															transform.position.y, 
															transform.position.z);
													
		if (pos.y < 3.8f) transform.position = new Vector3 (transform.position.x, 
															3.8f, 
															transform.position.z);
		if (pos.y > 36f) transform.position = new Vector3 (transform.position.x, 
															36f, 
															transform.position.z);
	}
}
