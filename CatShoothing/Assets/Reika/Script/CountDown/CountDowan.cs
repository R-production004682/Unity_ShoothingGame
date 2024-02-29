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
    /// �e�L�X�g�̃J�E���g�_�E��
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
    /// �e�L�X�g�̑���
    /// </summary>
    void TextEmotion()
    {
        if (controller != null && controller.allCatsHit)
        {
            // ���F�ɐݒ肷��
            countdownText.color = new Color(
                Mathf.PingPong(Time.time * rainbowSpeed, 1f),
                Mathf.PingPong(Time.time * rainbowSpeed + 0.33f, 1f),
                Mathf.PingPong(Time.time * rainbowSpeed + 0.66f, 1f)
            );
        }
        else { countdownText.color = Color.white; }
    }

    /// <summary>
    /// ���͋K���ƁASE�Đ�
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator DisableInputCoroutineAndSE(float duration)
    {
        Input.ResetInputAxes();
        yield return new WaitForSeconds(duration);
    }


    /// <summary>
    /// �J�ڂ܂ł̑҂�����
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
    /// Scene�J��
    /// </summary>
    private void TransitionScene()
    {
        if (isTimeUp == true && controller.allCatsHit == true)
        { StartCoroutine(ClearTransitionTime()); }

        else { StartCoroutine(GameOverTransitionTime()); }
    }
}
