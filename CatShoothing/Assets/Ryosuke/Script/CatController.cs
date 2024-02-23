using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField , Tooltip("猫の移動スピード")] private float cat_speed;
    [SerializeField , Tooltip("待機時間")] private float wait_time;
    [SerializeField, Tooltip("移動量")] private float cat_movement;

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
        //オブジェクトの原点を設定
        origine_position = this.transform.position;
        anim = GetComponent<Animator>();

        cat_movement = UnityEngine.Random.Range(0, cat_movement);

        Debug.Log(origine_position);

    }


    public void Update()
    {
        //移動した先で被らないように設定
        if (spawner != null) { spawner.IsPositionValid(start_position); }

        Vector2 current_position = this.transform.position;


        if (canMove)
        {
            //右に移動
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
            //左に移動
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
    /// GameManagerから位置情報を受け取る
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
