using UnityEngine;
using System.Collections;

public class VerticalWind : MonoBehaviour {
	public bool DownFlag;
	public float g_Scale = 0.5f;
	bool updateFlag;

	// Use this for initialization
	void Start () {
		updateFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void  OnTriggerStay2D(Collider2D col){
		if (col.CompareTag("Player")) {
			if (updateFlag != true) {
				updateFlag = true;
			}

			if (DownFlag != true) {
				g_Scale = 40f;

				float velY = g_Scale * Time.deltaTime;

				//col.GetComponent<Rigidbody2D> ().gravityScale -= g_Scale *Time.deltaTime;
				col.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0.0f, velY));
			} else {
				Debug.Log ("hit down");
				g_Scale = 40f;

				float velY = g_Scale * Time.deltaTime;

				//col.GetComponent<Rigidbody2D> ().gravityScale += g_Scale *Time.deltaTime;
				col.GetComponent<Rigidbody2D> ().AddForce(new Vector2(0.0f, -velY));
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag("Player")) {
			updateFlag = false;
		}
	}
}
