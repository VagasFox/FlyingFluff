using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public bool bubble_Active = false;	//シャボン玉がAciveかどうか
	private GameObject bubbleObj;		//シャボン玉Object

	public bool balloon_Active = false;
	private GameObject balloonObj;

	void Awake () {
		//泡の初期化
		bubbleObj = transform.Find ("PlayerBubble").gameObject;
		if (bubbleObj.activeSelf == true) bubbleObj.SetActive (false);

		//風船の初期化
		balloonObj = transform.Find("PlayerBalloon").gameObject;
		if (balloonObj.activeSelf == true) balloonObj.SetActive(false);
	}

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.CompareTag("BubbleBeta")){
			bubbleObj.SetActive(true);
		}
		if (col.transform.CompareTag("Balloon")){
			Destroy(col.gameObject);
			balloonObj.SetActive(true);
			balloon_Active = true;
		}
		if (col.transform.CompareTag("Girl"))
		{
			balloonObj.SetActive(false);
		}
	}
}