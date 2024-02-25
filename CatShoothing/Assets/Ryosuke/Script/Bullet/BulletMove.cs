using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 1.0f;

    PoolContent poolContent;

    private void Start()
    {
        poolContent = transform.GetComponent<PoolContent>();
    }

    private void Update()
    {
        transform.Translate((Vector2.up * speed) * Time.deltaTime / 4);

        if(transform.localPosition.y > 10)
        {
            poolContent.Hide();
        }
    }


}
