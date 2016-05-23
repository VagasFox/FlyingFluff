using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//ギミックの種類
public enum GimmickType{
	AIR_SHIP = 0,
	BALLOON,
	BIRD,
	BUBBLE,
	WINDMILL,
	GIRL,
	HELICOPTER,
	KOINOBORI,
	PEPAR_AIRPLANE,
	SEAGULLERS,
	FIREWORK,
	SMOKE,
}

//時間帯
public enum TimePeriodState{
	NOON = 0,	//昼
	EVENUNG,	//夕
	NIGHT		//夜
}

//地域
public enum RegionState{
	CITY = 0,			//都会
	RESIDENTIAL_AREAS,	//住宅地
	COUNTRYSIDE			//田舎
}
	
public class GimmickManager : MonoBehaviour {
	/*******************************ギミックの識別用*********************************/
	[System.Serializable]
	public class GimmickState{
		public GimmickType type;			//ギミックの種類
		public GameObject obj;				//ギミックのPrefab
		public TimePeriodState tp_state;	//時間帯ステータス
		public RegionState r_state;			//地域ステータス
	}

	[SerializeField]
	GimmickState[] GSParameter;				//登録設定リスト(Inspecterに表示するため)

	Dictionary <GimmickType, GimmickState> gimmickList;	//ギミックのリスト

	/*********************************ギミック生成用*********************************/
	[SerializeField]
	Transform[] InstancePosition;			//生成位置(プランナー調整用)

	public float tp_ChengeTime = 1.5f;		//何分くらいで時間帯ステータスを変更するか	

	TimePeriodState nowTimePeriod;			//現在の時間帯
	RegionState nowRegion;					//現在プレイヤーがいる地域

	void Awake () {
		//ディクショナリの初期化
		gimmickList = new Dictionary<GimmickType, GimmickState> ();
		for (int i = 0; i < GSParameter.Length; i++) {
			gimmickList [GSParameter [i].type] = gimmickList [i];
		}

		nowTimePeriod = TimePeriodState.NOON;	//現在の時間帯の初期化
		nowRegion = RegionState.CITY;			//現在の地域の初期化
	}

	void Update () {
		
	}

	public void InstanceGimmick(){
		
	}

	void ChangeTimePeriod(){
		
	}
}
