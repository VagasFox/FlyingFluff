using UnityEngine;
using System.Collections;

public class PlayerBubble : MonoBehaviour {
 
    void Start () {
	
	}

	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.transform.CompareTag("BubbleJama"))
        {
            gameObject.SetActive(false);
        }
    }
}
