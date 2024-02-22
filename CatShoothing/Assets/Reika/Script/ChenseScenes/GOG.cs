using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOG : MonoBehaviour
{
    private AudioSource se;

    void Start()
    {
        se = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        se.PlayOneShot(se.clip);

        StartCoroutine(SE_WaitTime());
    }

    IEnumerator SE_WaitTime()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }
}
