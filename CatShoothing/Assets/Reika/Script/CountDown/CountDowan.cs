using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDowan : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private float durationTime = 3.0f;
    [SerializeField] private float rainbowSpeed = 5;

    public float totalTime = 30f;
    private float currentTime;
    private bool isTimeUp;

    private Text countdownText;
    private AudioSource source;
    private CatController controller;

    void Start()
    {
        controller = FindObjectOfType<CatController>();
        source     = GetComponent<AudioSource>();
        countdownText = GetComponent<Text>();
        currentTime = totalTime;

        source.clip = this.clip;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0) 
        {
            currentTime = 0;

            countdownText.enabled = false;
            StartCoroutine(DisableInputCoroutineAndSE(durationTime));

            isTimeUp = true;

            TransitionScene();

            if (!source.isPlaying) { source.Play();}
        }
        UpdateCountdownText();
    }


    /// <summary>
    /// テキストのカウントダウン
    /// </summary>
    void UpdateCountdownText()
    {
        if (controller != null && controller.allCatsHit)
        {
            TextEmotion();
            return;
        }
        else { countdownText.text = Mathf.Ceil(currentTime).ToString(); }
    }


    /// <summary>
    /// テキストの装飾
    /// </summary>
    void TextEmotion()
    {
        if (controller != null && controller.allCatsHit)
        {
            // 虹色に設定する
            countdownText.color = new Color(
                Mathf.PingPong(Time.time * rainbowSpeed, 1f),
                Mathf.PingPong(Time.time * rainbowSpeed + 0.33f, 1f),
                Mathf.PingPong(Time.time * rainbowSpeed + 0.66f, 1f)
            );
        }
        else { countdownText.color = Color.white; }
    }

    /// <summary>
    /// 入力規制と、SE再生
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator DisableInputCoroutineAndSE(float duration)
    {
        Input.ResetInputAxes();
        yield return new WaitForSeconds(duration);
    }


    /// <summary>
    /// 遷移までの待ち時間
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearTransitionTime()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameClearScene");
    }
    
    IEnumerator GameOverTransitionTime()
    {
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene("GameOverScene");
    }

    /// <summary>
    /// Scene遷移
    /// </summary>
    private void TransitionScene()
    {
        if (isTimeUp == true && controller.allCatsHit == true)
        { StartCoroutine(ClearTransitionTime()); }

        else { StartCoroutine(GameOverTransitionTime()); }
    }
}
