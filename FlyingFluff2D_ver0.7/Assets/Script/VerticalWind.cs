using UnityEngine;
using System.Collections;

public class VerticalWind : MonoBehaviour {
	public bool DownFlag;
	public float g_Scale = 0.5f;
	public float g_Default;
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
				g_Default = col.GetComponent<Rigidbody2D> ().gravityScale;
				updateFlag = true;
			}

			if (DownFlag != true) {
				Debug.Log ("hit up");
				g_Scale = 0.6f;
				col.GetComponent<Rigidbody2D> ().gravityScale -= g_Scale *Time.deltaTime;
			} else {
				Debug.Log ("hit down");
				g_Scale = 10f;
				col.GetComponent<Rigidbody2D> ().gravityScale += g_Scale *Time.deltaTime;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag("Player")) {
			col.GetComponent<Rigidbody2D> ().gravityScale = g_Default;
			updateFlag = false;
		}
	}
}
