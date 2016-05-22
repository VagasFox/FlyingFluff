using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwipeInstance : MonoBehaviour {
	private Vector3 startPos;	//タッチのスタート地点
	private Vector3 endPos;		//タッチのエンド地点

	public Camera mainCamera;			//メインカメラ
	public GameObject test;

	private bool insFlag=false;
	GameObject windObj;
	float windScale;
	public float windPower;

	float count=0;
	private bool hanteiFlag = false;

	int vectorNum;

	void Start () {

	}

	void Update () {
		InputCheck ();

	}

	void InputCheck(){
		if(Input.GetKeyDown (KeyCode.Mouse0))    //マウス左クリック時に始点座標を代入
		{
			startPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,	Input.mousePosition.y, 0f));
		}

		//マウスを押している間の判定
		if(Input.GetKey(KeyCode.Mouse0))
		{
			//カウントが5以上
			if(count>=5)
			{
				hanteiFlag = CreateDecision ();
				if (hanteiFlag)
				{
					WindCreate();
					startPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
					count = 0;
				}

			}
			count +=Time.time;
		}

		if (Input.GetKeyUp (KeyCode.Mouse0)) {    //マウスのボタン解放時に終点座標を代入
			if(insFlag == false){
					WindCreate();
					count = 0;
			}
		}	
			
	}

	//生成判定
	bool CreateDecision()
	{
		bool move=false;
		//現在マウスポイント
		Vector3 mousepoint = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
		if (startPos.y - mousepoint.y >= 1 ||
			startPos.x - mousepoint.x >= 1)
		{
			move = true;
		}
		else if (startPos.y - mousepoint.y <= -1 ||
			startPos.x - mousepoint.x <= -1)
		{
			move = true;
		}

		return move;
	}

	//風の生成
	void WindCreate(){
		endPos = mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f));

		float directionX = endPos.x - startPos.x;
		float directionY = endPos.y - startPos.y;

		float distance = Vector3.Distance (startPos, endPos);
		windScale = distance;

		windObj = Instantiate (test, new Vector3 (startPos.x, startPos.y, 0f), Quaternion.identity)as GameObject;
		windObj.transform.localScale = new Vector3 (windScale, 1f, 1f);

		float radian = Mathf.Atan2 (directionY, directionX) * Mathf.Rad2Deg;    //radianに角度を代入
		string direction = "";
		if (radian < 0) {
			radian += 360;    //マイナスのものは360を加算
		}

		//方向判定
		if (radian <= 22.5f || radian > 337.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x + (windScale / 2),
				windObj.transform.position.y,
				0f);
			direction = "右";
			vectorNum = 0;
		} else if (radian <= 67.5f && radian > 22.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x + (windScale / 2),
				windObj.transform.position.y + (windScale / 3),
				0f);
			direction = "右上";
			vectorNum = 0;
		} else if (radian <= 112.5f && radian > 67.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x,
				windObj.transform.position.y + (windScale / 2),
				0f);
			direction = "上";
			vectorNum = 2;
		} else if (radian <= 157.5f && radian > 112.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x - (windScale / 2),
				windObj.transform.position.y + (windScale / 3f),
				0f);
			direction = "左上";
			vectorNum = 1;
		} else if (radian <= 202.5f && radian > 157.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x - (windScale / 2),
				windObj.transform.position.y,
				0f);
			direction = "左";
			vectorNum = 1;
		} else if (radian <= 247.5f && radian > 202.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x - (windScale / 2),
				windObj.transform.position.y - (windScale / 3),
				0f);
			direction = "左下";
			vectorNum = 1;
		} else if (radian <= 292.5f && radian > 247.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x,
				windObj.transform.position.y - (windScale / 2),
				0f);
			direction = "下";
			vectorNum = 3;
		} else if (radian <= 337.5f && radian > 292.5f) {
			windObj.transform.position = new Vector3 (windObj.transform.position.x + (windScale / 2),
				windObj.transform.position.y - (windScale / 3),
				0f);
			direction = "右下";
			vectorNum = 0;
		}
		//Debug.Log ("direction : " + direction);

		Vector3 wind = windObj.transform.eulerAngles;
		wind.z = GetAim (startPos, endPos);
		windObj.transform.eulerAngles = wind;
		windObj.GetComponent<AreaEffector2D> ().forceAngle = wind.z;

		if (vectorNum == 0 || vectorNum == 1) {
			windObj.GetComponent<AreaEffector2D> ().forceMagnitude = windPower;

			if(vectorNum == 0)windObj.layer = 10;     //layerの指定
			else if(vectorNum == 1)windObj.layer = 11;
		} else if (vectorNum == 2) {
			windObj.GetComponent<AreaEffector2D> ().enabled = false;
			windObj.GetComponent<VerticalWind> ().enabled = true;
			windObj.layer = 12;
		} else {
			windObj.GetComponent<AreaEffector2D> ().enabled = false;
			windObj.GetComponent<VerticalWind> ().enabled = true;
			windObj.GetComponent<VerticalWind> ().DownFlag = true;

			windObj.layer = 13;
		}

		windObj.GetComponent<DestroyWind>().instanceFlag = true;
	}

	/// <summary>
	// p2からp1への角度を求める
	// @param p1 自分の座標
	// @param p2 相手の座標
	// @return 2点の角度(Degree)
	/// </summary>
	float GetAim(Vector2 p1, Vector2 p2) {
		float dx = p2.x - p1.x;
		float dy = p2.y - p1.y;
		float rad = Mathf.Atan2(dy, dx);
		return rad * Mathf.Rad2Deg;
	}
}
