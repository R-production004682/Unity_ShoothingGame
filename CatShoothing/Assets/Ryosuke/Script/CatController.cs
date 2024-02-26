using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField, Tooltip("�L�̈ړ��X�s�[�h")] private float catSpeed;
    [SerializeField, Tooltip("�ҋ@����")] private float waitTime;
    [SerializeField, Tooltip("�ړ���")] private float catMovement;

    [SerializeField, Tooltip("�L��BoxCollider���i�[���Ă����ꏊ")]
    private Collider2D col;

    private Vector2 startPosition;
    private Vector2 originPosition;
    private Animator anim;
    private bool isMovingRight = true;
    private bool isWaiting;
    private bool isBreakTime;
    private float waitTimer = 0f;

    private void Start()
    {
        originPosition = transform.position;
        anim = GetComponent<Animator>();

        catMovement = UnityEngine.Random.Range(0, catMovement);

        col.enabled = true;
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

            if (isBreakTime == true) { transform.Translate(Vector2.zero); }
            else { transform.Translate(Vector2.right * catSpeed * Time.deltaTime); }
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
            if (isBreakTime == true) { transform.Translate(Vector2.zero); }
            else { transform.Translate(-Vector2.left * catSpeed * Time.deltaTime); }
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
    /// GameManager����ʒu�����󂯎��
    /// </summary>
    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;

        transform.position = startPosition;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            isBreakTime = true;
            col.enabled = false;

            PoolContent poolObj = other.GetComponent<PoolContent>();
            poolObj.Hide();

            //�H�ׂĐQ�郂�[�V�����̍Đ��i��x���������炱���Ń��[�V���������[�v������j
            {
                anim.SetBool("IsEat", true);
                StartCoroutine(EatingTime_And_SleepTime());

            }
        }
    }

    private IEnumerator EatingTime_And_SleepTime()
    {
        float eatingTime =  UnityEngine.Random.Range(2.0f , 4.0f);
        yield return new WaitForSeconds(eatingTime);

        anim.SetBool("IsEat" , false);
        anim.SetBool("IsReadySleep", true);

        yield return new WaitForSeconds(2.0f);

        anim.SetBool("IsReadySleep", false);
        anim.SetBool("IsSleep", true);
    }



}
