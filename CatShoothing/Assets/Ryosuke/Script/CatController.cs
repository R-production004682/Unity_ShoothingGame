using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField , Tooltip("猫の移動スピード")] private float catMoveSpeed;
    [SerializeField , Tooltip("待機時間")] private float waitTime;

    private Vector2 startPosition;
    private Vector2 moveDirection = Vector2.right;
    private Animator anim;
    private GameManager gameManager;


    /// <summary>
    /// GameManagerから位置情報を受け取る
    /// </summary>
    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;
        transform.position = startPosition;
    }

}
