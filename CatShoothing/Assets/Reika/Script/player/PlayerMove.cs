using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.0f;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        var position = transform.position;

        position.x = Mathf.Clamp(position.x, -8.0f, 8.0f);
        transform.position = position;
    }
}
