using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public bool bubble_Active = false;	//シャボン玉がAciveかどうか
	private GameObject bubbleObj;		//シャボン玉Object

	// Use this for initialization
	void Awake () {
		bubbleObj = transform.Find ("PlayerBubble").gameObject;
		if (bubbleObj.activeSelf == true) bubbleObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collider2D col){
		//もしシャボン玉が当たったら
		if(col.name == ""){
			
		}
	}
}
