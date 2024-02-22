using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startmusic : MonoBehaviour
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
    }
}