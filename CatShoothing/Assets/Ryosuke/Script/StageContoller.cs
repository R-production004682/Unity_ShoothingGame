using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageContoller : MonoBehaviour
{
    [SerializeField] public ObjectPool particlePool = null;
    [SerializeField] public ObjectPool p_BulletPool = null;
    [SerializeField] public PlayerController playerObj = null;

    private static StageContoller instance;

    public static StageContoller Instance { get => instance; }


    private void Awake()
    {
        instance = this.GetComponent<StageContoller>();
    }

    private void Start()
    {}

    private void Update()
    {
        //Player‚Ì“®‚«
        {
            float horizontal = Input.GetAxis("Horizontal");
            playerObj.PlayerMovement(horizontal , this.transform.position);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            playerObj.Shot();
        }
    }
}
