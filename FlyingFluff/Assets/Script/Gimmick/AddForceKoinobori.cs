using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddForceKoinobori : MonoBehaviour
{
    public float coefficient;   // 空気抵抗係数
    public Vector2 velocity;    // 風速

    void OnTriggerStay2D(Collider2D other)
    {
        other.attachedRigidbody.AddForce(Vector2.right * 10);
    }


}
