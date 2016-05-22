using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour
{
	[SerializeField] private float distance_with_player = 10f;
	[SerializeField] private float Y_MaxPos = 30f;
	private float distance;
	private GameObject playerObj;
	private bool heliflg;

    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
		heliflg = false;
    }
   
    void Update()
	{
		distance = (playerObj.transform.position - transform.position).magnitude;
		if (distance <= distance_with_player) heliflg = true;

		if (heliflg != true) return;
		HeliMove ();
		if(heliflg == true && distance > 50f) Destroy(transform.gameObject);

    }

	//ヘリの移動
	void HeliMove(){
		if (transform.position.y < Y_MaxPos) {
			transform.position += new Vector3 (0f, 3f, 0f) * Time.deltaTime;
		} else if(transform.position.y > Y_MaxPos){
			if (transform.eulerAngles.z < 40f) {
				transform.eulerAngles += new Vector3 (0f, 0f, 10f) * Time.deltaTime;
			}
			transform.position -= new Vector3 (3f, 0f, 0f) * Time.deltaTime;;
		} 
	}
}
