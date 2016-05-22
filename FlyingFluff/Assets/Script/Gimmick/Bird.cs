using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
	public float b_speed;

	private Vector3 target;
	private Vector3 birdPos;
	private Vector3 direction;
	private Vector3 prev;
	protected GameObject Player;

	private bool P_Flag = false;

	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");

		birdPos = this.transform.position;
	}

	void Update()
	{
		if (P_Flag == false)
		{
			this.transform.position -= new Vector3(b_speed * Time.deltaTime, 0, 0);
		}
		if (P_Flag == true)
		{
			RigidbodyMove();
		}
	}

	void RigidbodyMove()
	{
		target = Player.transform.position;
		float Vx = target.x - birdPos.x;
		float Vy = target.y - birdPos.y;

		direction = new Vector3(Vx, Vy).normalized;

		GetComponent<Transform>().position += direction * b_speed * Time.deltaTime;
		if (Mathf.Abs(Vx) < 0.1f || Mathf.Abs(Vy) < 0.1f)
		{
			transform.position -= new Vector3(b_speed * Time.deltaTime, 0);            
		}
		Rotation();
	}

	void Rotation()
	{
		prev = transform.localPosition;

		Vector3 diff = transform.localPosition - prev;
		if (diff.magnitude < 0.1f)
		{
			float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg * 60;
			GetComponent<Transform>().rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			P_Flag = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			this.transform.position -= new Vector3(b_speed * Time.deltaTime, 0, 0);
			P_Flag = false;
		}
	}
}
