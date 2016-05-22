using UnityEngine;
using System.Collections;

public class UVScroller : MonoBehaviour {
	public Vector2 m_ScrollSpeed;
	Material m_Material;

	Vector2 m_CurrentOffset;

	void Start () {
		m_Material = GetComponent<Renderer> ().material;
	}

	void Update () {
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			m_CurrentOffset += m_ScrollSpeed * Time.deltaTime;
//		}

		m_Material.SetTextureOffset ("_MainTex", m_ScrollSpeed * Time.time);
		//m_Material.SetTextureOffset ("_MainTex", m_CurrentOffset);
	}
}
