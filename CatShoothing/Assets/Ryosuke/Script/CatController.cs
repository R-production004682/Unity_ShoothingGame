using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField, Tooltip("猫の移動スピード")] private float catSpeed;
    [SerializeField, Tooltip("待機時間")] private float waitTime;
    [SerializeField, Tooltip("移動量")] private float catMovement;

    private Vector2 startPosition;
    private Vector2 originPosition;

    private Animator anim;

    private bool isMovingRight = true;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    private void Start()
    {
        originPosition = transform.position;
        anim = GetComponent<Animator>();

        catMovement = UnityEngine.Random.Range(0, catMovement);

        Debug.Log(originPosition);
    }

    private void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                waitTimer = 0f;
            }
        }
        else
        {
            Vector2 currentPosition = transform.position;

            if (isMovingRight)
            {
                MoveRight(currentPosition);
            }
            else
            {
                MoveLeft(currentPosition);
            }
        }
    }

    private void MoveRight(Vector2 currentPosition)
    {
        if (currentPosition.x <= startPosition.x + catMovement)
        {
            anim.SetBool("IsMove", true);
            transform.Translate(Vector2.right * catSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("IsMove", false);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isMovingRight = false;
            StartWait();
        }
    }

    private void MoveLeft(Vector2 currentPosition)
    {
        if (originPosition.x - catMovement <= currentPosition.x)
        {
            anim.SetBool("IsMove", true);
            transform.Translate(-Vector2.left * catSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("IsMove", false);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isMovingRight = true;
            StartWait();
        }
    }

    private void StartWait()
    {
        isWaiting = true;
        waitTimer = waitTime;
    }

    /// <summary>
    /// GameManagerから位置情報を受け取る
    /// </summary>
    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;

        transform.position = startPosition;
    }
}
