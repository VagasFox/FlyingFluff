using UnityEngine;
using System.Collections;

public class GameEndDecision : MonoBehaviour {

	[SerializeField]
	private bool gameEnd;
	public bool Level {
		get { return gameEnd; } 
		private set { gameEnd = value; }
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.CompareTag ("Player")) {
			gameEnd = true;
		}
	}
}
