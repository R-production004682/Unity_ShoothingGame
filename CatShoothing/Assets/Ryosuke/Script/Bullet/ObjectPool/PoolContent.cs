using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    ObjectPool pool;

    private void Start()
    {
        pool = transform.parent.GetComponent<ObjectPool>();
        gameObject.SetActive(false);
    }

    /*’e‚ðŒ©‚¹‚é*/
    public void Show(Vector3 position , float angle)
    {
        transform.position = position;
        transform.eulerAngles = new Vector2(angle , 0);
    }

    /*’e‚ð‰B‚·*/
    public void Hide()
    {
        Debug.Assert(gameObject.activeInHierarchy);
        pool.Collect(this);
    }
}
