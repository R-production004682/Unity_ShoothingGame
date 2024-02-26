using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    public float totalTime = 30f; 
    private float currentTime;
    private Text countdownText;

    void Start()
    {
        countdownText = GetComponent<Text>();
        currentTime = totalTime; 
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0) { currentTime = 0; }

        UpdateCountdownText();
    }

    void UpdateCountdownText() => countdownText.text = Mathf.Ceil(currentTime).ToString();
}
