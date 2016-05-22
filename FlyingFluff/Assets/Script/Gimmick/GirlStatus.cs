using UnityEngine;
using System.Collections;

public class GirlStatus : MonoBehaviour {
	private GameObject gballoonObj;
	private bool ps;

    void Awake()
    {
        gballoonObj = transform.Find("GirlBalloon").gameObject;
        if (gballoonObj.activeSelf == true) gballoonObj.SetActive(false);        
    }

	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col){
        if (col.transform.CompareTag("Player"))
        {
			ps= col.gameObject.GetComponent<PlayerStatus>().balloon_Active;
            
            if (ps == true) gballoonObj.SetActive(true);
        }
    }
}
