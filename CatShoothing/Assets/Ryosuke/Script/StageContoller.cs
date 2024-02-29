using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageContoller : MonoBehaviour
{
    [SerializeField] public ObjectPool p_BulletPool = null;
    [SerializeField] public PlayerController playerObj = null;
    [SerializeField] private AudioClip clip;

    private AudioSource source;
    private static StageContoller instance;

    public static StageContoller Instance { get => instance; }
    
    public bool isClear;



    private void Awake()
    {
        source   = GetComponent<AudioSource>();
        instance = this.GetComponent<StageContoller>();
        isClear  = false;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        //Player‚Ì“®‚«
        {
            float horizontal = Input.GetAxis("Horizontal");
            playerObj.PlayerMovement(horizontal , this.transform.position);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            source.PlayOneShot(clip);
            playerObj.Shot();
        }
    }
}
