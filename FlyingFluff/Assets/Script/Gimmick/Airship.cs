using UnityEngine;
using System.Collections;

public class Airship : MonoBehaviour {
	[SerializeField] private float moveSpeed; 					//動く速度
	bool CameraIn = false;
	GameObject player;

	[SerializeField] private float distancePoint = 20f;			//消える距離

	void Awake () {
		DirectionCheck () ;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		float m_SpeedX = moveSpeed * Time.deltaTime;

		this.transform.position += new Vector3 (m_SpeedX, 0, 0);

		DestroyAirShip ();
	}

	//進行方向の確認
	void DirectionCheck(){
		if (moveSpeed > 0) {
			transform.localScale = new Vector2 (
				transform.localScale.x * 1, 
				transform.localScale.y
			);
		} else if (moveSpeed < 0) {
			transform.localScale = new Vector2 (
				transform.localScale.x * -1, 
				transform.localScale.y
			);

		}
	}

	//死亡判定
	void DestroyAirShip(){
		if (CameraIn == true) {
			float distance = Vector2.Distance (
				player.transform.position, 
				this.transform.position
			);
			Debug.Log ("distance:" + distance);
			if (distance > distancePoint) {
				Destroy (this.gameObject);
			}
		}
	}

	//カメラに映ってる間に呼ばれる
	private void OnWillRenderObject(){
		//メインカメラに映った時だけ_isRenderedを有効に
		if (Camera.current.tag == "MainCamera") {
			CameraIn = true;
		} 
	}
}