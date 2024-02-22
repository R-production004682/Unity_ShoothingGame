using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField , Tooltip("�L�̈ړ��X�s�[�h")] private float catMoveSpeed;
    [SerializeField , Tooltip("�ҋ@����")] private float waitTime;

    private Vector2 startPosition;
    private Vector2 moveDirection = Vector2.right;
    private Animator anim;
    private GameManager gameManager;


    /// <summary>
    /// GameManager����ʒu�����󂯎��
    /// </summary>
    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;
        transform.position = startPosition;
    }

}
