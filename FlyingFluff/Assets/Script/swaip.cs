using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class swaip : MonoBehaviour {
	private bool isFlick;
	private bool isClick;
	private Vector3 touchStartPos;		//タッチのスタート地点
	private Vector3 touchEndPos;		//タッチのエンド地点
	private int direction;

	public Camera mainCamera;			//メインカメラ
	public GameObject test;
	public float destoryTimer = 0f;
	private float timer = 0f;
	private bool insFlag=false;
	GameObject windObj;
	float windScale;
	public float windPower;

	float divisionWidth;				//分割した画面横サイズ
	float divisionHeight;				//分割した画面縦サイズ
	Rect[,] rectList;					//画面分割のリスト
	public int divisionCount = 3;		//分割数 例)9分割なら3に設定

	[System.Serializable]
	public class WindStatus
	{
		public Vector3 windVector;		//風のベクトル情報
		public bool windFlag = false;	//風が発生しているかどうか
		public int widthNum;
		public int heightNum;
	}

	public WindStatus[,] windStatusList; //風の情報用リスト
	public float windTime;
	private float WindTimer;

	int vectorNum;

	// Use this for initialization
	void Awake () {
		//画面分割リストの初期化
		rectList = new Rect[divisionCount, divisionCount];
		windStatusList = new WindStatus[divisionCount, divisionCount];

		//Screen座標をViewport座標に変換
//		Vector2 newVec = mainCamera.ScreenToViewportPoint (new Vector2 (Screen.width / (divisionCount * 2), 
//			                											Screen.height / (divisionCount * 2)));
		Vector2 newVec = mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0f));


		//縦,横サイズを分割したサイズを計算
		divisionWidth = newVec.x / divisionCount;
		divisionHeight = newVec.y / divisionCount;

		//画面分割リストの登録
		//左下が[0,0],右に行くと[1,0], [2,0],上に行くと[0,1],[0,2]となる
//		for (int i = 0; i < divisionCount; i++) {
//			for(int j = 0; j < divisionCount; j++) {
//				rectList [i, j] = new Rect (divisionWidth * i, 
//											divisionHeight * j, 
//											divisionWidth * (j + 1), 
//											divisionHeight * (i + 1));
//
//				rectList [i, j] = new Rect (newVec.x * i, 
//											newVec.y * j, 
//											newVec.x * (j + 1), 
//											newVec.y * (i + 1));
//
//				windStatusList [i, j] = new WindStatus ();
//				//Debug.Log ("rect[" + i + "][" + j + "]:" + rectList [i, j]);
//			}
//		}
	}
	
	// Update is called once per frame
	void Update () {
		InputCheck ();

		if (insFlag == true) {
			timer += Time.deltaTime;

			if (timer > destoryTimer) {
				Destroy (windObj);
				Debug.Log ("削除");
				timer = 0f;
				insFlag = false;
			}
		}
	}

	void InputCheck(){
		if(Input.GetKeyDown (KeyCode.Mouse0))
		{
			isFlick = true;
			touchStartPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x ,
				Input.mousePosition.y, 0f));

			Invoke ("FlickOff" , 0.2f);
		}
		if(Input.GetKey (KeyCode.Mouse0))
		{
			touchEndPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x ,
				Input.mousePosition.y, 0f));
			if(touchStartPos != touchEndPos )
			{
				ClickOff ();
			}
		}

		if(Input.GetKeyUp (KeyCode.Mouse0))
		{
			touchEndPos = mainCamera.ScreenToWorldPoint (new Vector3(Input.mousePosition.x ,
				Input.mousePosition.y, 0f));

			Debug.Log (touchEndPos);
			if(IsFlick ())
			{
				Debug.Log ("Flick");
				float directionX = touchEndPos.x - touchStartPos.x;
				float directionY = touchEndPos.y - touchStartPos.y;
				Debug.Log ("DirectionX : " + directionX);
				Debug.Log ("DirectionY : " + directionY);

				if(Mathf.Abs (directionY) < Mathf.Abs (directionX))
				{
					if(0 < directionX)
					{
						Debug.Log ("Flick : Right");
						direction = 6;
					}
					else
					{
						Debug.Log ("Flick : Left");
						direction = 4;
					}
				}
				else if(Mathf.Abs (directionX) < Mathf.Abs (directionY))
				{
					if(0 < directionY)
					{
						Debug.Log ("Flick : Up");
						direction = 8;
					}
					else
					{
						Debug.Log ("Flick : Down");
						direction = 2;
					}
				}
				else
				{
					Debug.Log ("Flick : Not, It's Tap");
					FlickOff();
				}
			}
			else
			{

//				float directionX = touchEndPos.x - touchStartPos.x;
//				float directionY = touchEndPos.y - touchStartPos.y;
//				for (int i = 0; i < divisionCount; i++) {
//					for (int j = 0; j < divisionCount; j++) {
//						//Rectとタッチの位置を判定
//						if((touchEndPos.x >= rectList[i,j].xMin && touchEndPos.x < rectList[i,j].xMax &&
//							touchEndPos.y >= rectList[i,j].yMin && touchEndPos.y < rectList[i,j].yMax)||
//							(touchStartPos.x >= rectList[i,j].xMin && touchStartPos.x < rectList[i,j].xMax &&
//							touchStartPos.y >= rectList[i,j].yMin && touchStartPos.y < rectList[i,j].yMax)){
//
//							windStatusList [i, j].windFlag = true;
//							Vector3 vec = CheckVector (directionX, directionY);
//							windStatusList [i, j].windVector = vec;
//							windStatusList [i, j].widthNum = i;
//							windStatusList [i, j].heightNum = j;
//
//							//InstanceWind (windStatusList [i, j]);
//
//							//Debug.Log ("WindStatus[" + i + "][" + j + "]:" + windStatusList [i, j].windFlag + ", " + windStatusList [i, j].windVector);
//						}
//					}
//				}

				if (insFlag == false) {
					Debug.Log ("Long Touch");
					float directionX = touchEndPos.x - touchStartPos.x;
					float directionY = touchEndPos.y - touchStartPos.y;

					float distance = Vector3.Distance (touchStartPos, touchEndPos);
					windScale = distance;

					windObj = Instantiate (test, new Vector3 (touchStartPos.x, touchStartPos.y, 0f), Quaternion.identity)as GameObject;
					windObj.transform.localScale = new Vector3 (windScale, 1f, 1f);


					if(Mathf.Abs (directionY) < Mathf.Abs (directionX))
					{
						if(0 < directionX)
						{
							Debug.Log ("Swaip : Right");
							windObj.transform.position = new Vector3 (windObj.transform.position.x + (windScale / 2),
																	  windObj.transform.position.y,
																	  0f);
							vectorNum = 0;
						}
						else
						{
							Debug.Log ("Swaip : Left");
							windObj.transform.position = new Vector3 (windObj.transform.position.x - (windScale / 2),
																	  windObj.transform.position.y,
																	  0f);
							vectorNum = 1;
						}
					}
					else if(Mathf.Abs (directionX) < Mathf.Abs (directionY))
					{
						if(0 < directionY)
						{
							Debug.Log ("Swaip : Up");
							windObj.transform.position = new Vector3 (windObj.transform.position.x,
																	  windObj.transform.position.y + (windScale / 2),
																	  0f);
							vectorNum = 2;
						}
						else
						{
							Debug.Log ("Swaip : Down");
							windObj.transform.position = new Vector3 (windObj.transform.position.x,
																	  windObj.transform.position.y - (windScale / 2),
																	  0f);
							vectorNum = 3;
						}
					}

					Vector3 wind = windObj.transform.eulerAngles;
					wind.z = GetAim (touchStartPos, touchEndPos);
					windObj.transform.eulerAngles = wind;
					windObj.GetComponent<AreaEffector2D> ().forceAngle = wind.z;

					if (vectorNum == 0) {
						windObj.GetComponent<AreaEffector2D> ().forceMagnitude = windPower;
					} else if (vectorNum == 1) {
						windObj.GetComponent<AreaEffector2D> ().forceMagnitude = -windPower;
					} else if (vectorNum == 2) {
						windObj.GetComponent<AreaEffector2D> ().enabled = false;
						windObj.GetComponent<VerticalWind> ().enabled = true;

					} else {
						windObj.GetComponent<AreaEffector2D> ().enabled = false;
						windObj.GetComponent<VerticalWind> ().enabled = true;
						windObj.GetComponent<VerticalWind> ().DownFlag = true;
					}

					direction = 5;
					insFlag = true;
				}
			}
		}
	}

	public void FlickOff()
	{
		direction = 5;
		isFlick = false;
	}

	public bool IsFlick()
	{
		return isFlick;
	}

	public void ClickOn()
	{
		isClick = true;
		Invoke ("ClickOff" , 0.2f);
	}

	public bool IsClick()
	{
		return isClick;
	}

	public void ClickOff()
	{
		isClick = false;
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

	Vector2 CheckVector(float CheckX, float CheckY){
		if(Mathf.Abs (CheckY) < Mathf.Abs (CheckX))
		{
			if(0 < CheckX)
			{
				Debug.Log ("Swaip : Right");
				Vector2 vector = new Vector3 (windPower, 0f);
				return vector;
			}
			else
			{
				Debug.Log ("Swaip : Left");
				Vector3 vector = new Vector3 (-windPower, 0f);
				return vector;
			}
		}
		else if(Mathf.Abs (CheckX) < Mathf.Abs (CheckY))
		{
			if(0 < CheckY)
			{
				Debug.Log ("Swaip : Up");
				Vector3 vector = new Vector3 (0f, windPower);
				return vector;
			}
			else
			{
				Debug.Log ("Swaip : Down");
				Vector3 vector = new Vector3 (0f, -windPower);
				return vector;
			}
		}

		return new Vector2 (0f, 0f);
	}


//	void InstanceWind(WindStatus w_Status){
//		if (w_Status.windFlag != true) return;
//		WindTimer += Time.deltaTime;
//
//		Vector3 addZoneStart = mainCamera.ViewportToScreenPoint (new Vector3 (rectList [w_Status.widthNum, w_Status.heightNum].x,
//			rectList [w_Status.widthNum, w_Status.heightNum].y, 0f));
//		
//		Vector3 addZoneEnd = mainCamera.ViewportToScreenPoint (new Vector3 (rectList [w_Status.widthNum, w_Status.heightNum].width,
//			                  											 rectList [w_Status.widthNum, w_Status.heightNum].height, 0f));
//		
//
//		if (WindTimer > windTime) {
//			Debug.Log ("time終了");
//			WindTimer = 0;
//			w_Status.windFlag = false;
//		}
//	}
}
