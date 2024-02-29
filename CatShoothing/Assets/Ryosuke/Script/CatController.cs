using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatController : MonoBehaviour
{

    [SerializeField, Tooltip("猫の移動スピード")] private float catSpeed;
    [SerializeField, Tooltip("待機時間")] private float waitTime;
    [SerializeField, Tooltip("移動量")] private float catMovement;
    [SerializeField, Tooltip("Colliderを格納")] private Collider2D col;
    [SerializeField, Tooltip("サウンドの格納")] private AudioClip shotSE;
    [SerializeField, Tooltip("サウンドの格納")] private AudioClip clearSE;

    public bool isClear;
    public bool allCatsHit;

    private Vector2 startPosition;
    private Vector2 originPosition;
    private Animator anim;
    private bool isMovingRight = true;
    private bool isWaiting;
    private bool isBreakTime;
    private float waitTimer = 0f;

    AudioSource source;
    Spawner spawner;

    private void Start()
    {
        originPosition = transform.position;
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        catMovement = UnityEngine.Random.Range(0, catMovement);

        col.enabled = true;

        spawner = FindObjectOfType<Spawner>();
        allCatsHit = false;
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
    /// GameManagerから位置情報を受け取る
    /// </summary>
    public void SetStartPosition(Vector2 position)
    {
        startPosition = position;

        transform.position = startPosition;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            isBreakTime = true;
            col.enabled = false;

            PoolContent poolObj = other.GetComponent<PoolContent>();
            poolObj.Hide();

            //食べて寝るモーションの再生（一度当たったらここでモーションをループさせる）
            {
                anim.SetBool("IsEat", true);
                StartCoroutine(EatingTime_And_SleepTime());
            }
            source.PlayOneShot(shotSE);
            BulletHit();
        }
    }

    private IEnumerator EatingTime_And_SleepTime()
    {
        float eatingTime = UnityEngine.Random.Range(2.0f, 4.0f);
        yield return new WaitForSeconds(eatingTime);

        anim.SetBool("IsEat", false);
        anim.SetBool("IsReadySleep", true);

        yield return new WaitForSeconds(2.0f);

        anim.SetBool("IsReadySleep", false);
        anim.SetBool("IsSleep", true);
    }

    public void BulletHit()
    {
        isClear = true;

        // すべての猫が弾に当たったかどうかを確認
        allCatsHit = true;
        foreach (CatController cat in spawner.spawnedCats)
        {
            if (!cat.isClear)
            {
                allCatsHit = false;
                break;
            }
        }

        if (allCatsHit)
        {
            Debug.Log("Game clear!");
            // ここにクリア処理を追加
            StartCoroutine(TransitionScene());
            source.PlayOneShot(clearSE);
        }
    }

    /// <summary>
    /// ゲームクリア後にSceneをGameClearSceneに移動させる
    /// </summary>
    /// <returns></returns>
    IEnumerator TransitionScene()
    {
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene("GameClearScene");
    }
}
