using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float catMoveSpeed;
    [SerializeField] private float waitTime;

    private Vector2 startPosition;


    private void Start()
    {
        
    }


    private void Update( )
    {
        
    }

    
    private void Move()
    {

        //移動制限まで行ったら、
        //トランザクションの休憩か、Sleepを呼び出し、アニメーションの再生が終わったらY軸に180度オブジェクトを回転させて徘徊させ直す。
        //それを繰り返す。
    }

    /// <summary>
    /// ネコの移動に制限をかける
    /// </summary>
    private void MovementRestrictions()
    {

    }




}
