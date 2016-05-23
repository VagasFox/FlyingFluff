using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour
{
    public float speed;
    public float minSpeed;
    GameObject planeObj;

    void Start()
    {
        minSpeed = speed;
        planeObj.layer = 9;
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Force);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject)
        {
            speed--;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject)
        {
            if (speed < minSpeed)
            {
                speed = minSpeed;
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Force);
            }
        }
    }
}
