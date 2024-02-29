using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDowan : MonoBehaviour
{
    [SerializeField] Text timeUpText;
    [SerializeField] Button retryButton;
    [SerializeField] Button exitButton;
    [SerializeField] AudioClip buttonSE;
    [SerializeField] AudioClip textSE;
    [SerializeField] private float durationTime = 3.0f;
    [SerializeField] private float rainbowSpeed = 5;

    public float totalTime = 30f;
    private float currentTime;

    private Text countdownText;
    private AudioSource se;
    private CatController controller;

    void Start()
    {
        controller = FindObjectOfType<CatController>();
        se = GetComponent<AudioSource>();
        countdownText = GetComponent<Text>();
        currentTime = totalTime;

        timeUpText.enabled  = false;
        retryButton.gameObject.SetActive(false);
        exitButton .gameObject.SetActive(false);
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0) 
        {
            currentTime = 0;

            countdownText.enabled = false;
            timeUpText.enabled = true;
            retryButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);

            if (timeUpText.enabled)
            {
                se.PlayOneShot(textSE);
            }

            StartCoroutine(DisableInputCoroutine(durationTime));//“ü—Í’†Ž~
        }
        UpdateCountdownText();
    }


    void UpdateCountdownText()
    {
        if (controller != null && controller.allCatsHit)
        {
            TextEmotion();
            StartCoroutine(DisableInputCoroutine(durationTime));
            return;
        }
        else
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();
        }
        
    }

    void TextEmotion()
    {
        if (controller != null && controller.allCatsHit)
        {
            // “øF‚ÉÝ’è‚·‚é
            countdownText.color = new Color(
                Mathf.PingPong(Time.time * rainbowSpeed, 1f),
                Mathf.PingPong(Time.time * rainbowSpeed + 0.33f, 1f),
                Mathf.PingPong(Time.time * rainbowSpeed + 0.66f, 1f)
            );
        }
        else
        {
            // isClear‚ªfalse‚Ìê‡A’Êí’Ê‚è‚ÌF‚ðÝ’è‚·‚é
            countdownText.color = Color.white;
        }

    }

    IEnumerator DisableInputCoroutine(float duration)
    {
        Input.ResetInputAxes();
        yield return new WaitForSeconds(duration);
    }


    public void RetryButtonCleck()
    {
        se.PlayOneShot(buttonSE);
        StartCoroutine(Retry_WaitTime());
    }

    public void ExitButtonCleck()
    {
        se.PlayOneShot(buttonSE);
        StartCoroutine(Exit_WaitTime());
    }

    IEnumerator Retry_WaitTime()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator Exit_WaitTime()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("TitleScene");
    }
}
