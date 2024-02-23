using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField , Tooltip("�L�̈ړ��X�s�[�h")] private float cat_speed;
    [SerializeField , Tooltip("�ҋ@����")] private float wait_time;
    [SerializeField, Tooltip("�ړ���")] private float cat_movement;

    Spawner spawner = new Spawner();

    private Vector2 start_position;
    private Vector2 origine_position;

    Animator anim;

    bool canMove = true;
    bool canBreakTime;
    bool canSleep;
    bool canEating;


    public void Start()
    {
        //�I�u�W�F�N�g�̌��_��ݒ�
        origine_position = this.transform.position;
        anim = GetComponent<Animator>();

        cat_movement = UnityEngine.Random.Range(0, cat_movement);

        Debug.Log(origine_position);

    }


    public void Update()
    {
        //�ړ�������Ŕ��Ȃ��悤�ɐݒ�
        if (spawner != null) { spawner.IsPositionValid(start_position); }

        Vector2 current_position = this.transform.position;


        if (canMove)
        {
            //�E�Ɉړ�
            if (current_position.x <= start_position.x + cat_movement)
            {
                anim.SetBool("IsMove", true);

                transform.Translate(Vector2.right * cat_speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("IsMove", false);
                Vector2 direction = new Vector3(0.0f , 180.0f , 0.0f);
                transform.rotation = Quaternion.Euler(direction);
                canMove = false;
            }
        }
        


        if(!canMove)
        {
            //���Ɉړ�
            if (origine_position.x - cat_movement <= current_position.x)
            {
                anim.SetBool("IsMove", true);
                transform.Translate((-Vector2.left * cat_speed) * Time.deltaTime);
                
            }
            else
            {
                anim.SetBool("IsMove", false);

                Vector2 direction = new Vector3(0.0f, 0.0f, 0.0f);
                transform.rotation = Quaternion.Euler(direction);

                canMove = true;
            }
        }
    }


    /// <summary>
    /// GameManager����ʒu�����󂯎��
    /// </summary>
    public void SetStartPosition(Vector2 position)
    {
        start_position = position;
        transform.position = start_position;
    }


    public void SetCanMove(bool move)
    {
        canMove = move;
    }
}
