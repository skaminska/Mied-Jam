using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        if (Input.mousePosition.x <= 30 && transform.position.x>-15)
            transform.position += Vector3.left * Time.deltaTime * speed;

        if (Input.mousePosition.x >= Screen.width-30 && transform.position.x <13)
            transform.position += Vector3.right * Time.deltaTime * speed;

        if (Input.mousePosition.y <= 30 && transform.position.y >-11)
            transform.position += Vector3.down * Time.deltaTime * speed;

        if (Input.mousePosition.y >= Screen.height-30 && transform.position.y < 11)
            transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
