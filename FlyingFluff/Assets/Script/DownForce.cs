using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DownForce : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        other.attachedRigidbody.AddForce(Vector2.down * 5);//天井にあたったら反射
    }
}
