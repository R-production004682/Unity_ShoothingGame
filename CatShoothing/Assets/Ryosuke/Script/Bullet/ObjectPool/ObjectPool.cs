using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] PoolContent content = null;
    [SerializeField] private int maxBullet = 10;

    Queue<PoolContent> bulletQueue;

    private void Start()
    {
        bulletQueue = new Queue<PoolContent>(maxBullet);

        for(int i = 0; i < maxBullet; i++)
        {
            var templateObj = Instantiate(content);
            templateObj.transform.parent = transform;

            templateObj.transform.localPosition = new Vector2(30, 30);
            bulletQueue.Enqueue(templateObj);
        }
    }


    public PoolContent Launch(Vector3 position , float angle)
    {
        if (bulletQueue.Count <= 0) { return null; }

        var temp = bulletQueue.Dequeue();
        temp.gameObject.SetActive(true);
        
        temp.Show(position , angle);
        return temp;
    }


    public void Collect(PoolContent bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }


    public void Reset()
    {
        BroadcastMessage($"Hide , {SendMessageOptions.DontRequireReceiver}");
    }
}
