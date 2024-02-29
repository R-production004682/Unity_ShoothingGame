using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> cats;
    public int randomPickUp;//何匹ランダムに出現させるか

    public float spawnX = 6.0f , spawnY = 4.0f;


    private List<Vector2> spawnPositions = new List<Vector2>();


    private void Start( )
    {
        if (cats == null || cats.Count == 0) { return; }


        for (int i = 0; i < randomPickUp; i++)
        {

            if (spawnPositions.Count >= randomPickUp) { break; }

            int randomCreate = Random.Range(0 , randomPickUp);//ネコをランダム生成

            Vector2 spawnPosition;
            bool positionFound = false;
            int attempts = 0;

            do
            {
                spawnPosition = new Vector2(Random.Range(-spawnX, spawnX), Random.Range(1, spawnY + 1));
                positionFound = IsPositionValid(spawnPosition);
                attempts++;
            }
            while (!positionFound && attempts < 100);

            if (!positionFound) { break; }


            Instantiate(cats[randomCreate] , spawnPosition , Quaternion.identity);
            spawnPositions.Add(spawnPosition);
        }
    }


    private bool IsPositionValid(Vector2 position)
    {
        foreach(Vector2 spawnPosition in spawnPositions)
        {
            if(Vector2.Distance(position , spawnPosition) < 1.0f)
            {
                return false;
            }
        }

        return true;
    }
}
