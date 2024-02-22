using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> cats;
    [SerializeField] protected int randomPickUp;//���C�����_���ɏo�������邩

    public float spawnX = 6.0f , spawnY = 4.0f;


    public List<Vector2> spawnPositions = new List<Vector2>();


    private void Start( )
    {
        if (cats == null || cats.Count == 0) return;

        //�X�|�[�������鐔�������_���ɂ���
        randomPickUp = Random.Range(3 , cats.Count);

        for (int i = 0; i < randomPickUp && spawnPositions.Count < randomPickUp; i++)
        {
            int randomCreate = Random.Range(0 , randomPickUp);
            Vector2 spawnPosirion = GetValidSpawnPosition();

            if (spawnPosirion == Vector2.zero) break;

            GameObject newCat = Instantiate(cats[randomCreate] , spawnPosirion , Quaternion.identity);
            CatController catController = newCat.GetComponent<CatController>();

            if(catController != null)
            {
                catController.SetStartPosition(spawnPosirion);
            }

            spawnPositions.Add(spawnPosirion);
        }
    }


    /// <summary>
    /// �X�|�[������|�W�V�����������_���Ɏw��
    /// </summary>
    /// <returns></returns>
    private Vector2 GetValidSpawnPosition()
    {
        int number_Of_Attempts = 0;
        Vector2 spawnPosition = Vector2.zero;

        while (number_Of_Attempts < 100)
        {
            spawnPosition = new Vector2(Random.Range(-spawnX , spawnX) , Random.Range(1 , spawnY + 1));
            if (IsPositionValid(spawnPosition)) return spawnPosition;
            number_Of_Attempts++;
        }
        return Vector2.zero;
    }


    /// <summary>
    /// �d�Ȃ��ăX�|�[�����Ȃ��悤�Ɉʒu�𒲐�
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool IsPositionValid(Vector2 position)
    {
        foreach(Vector2 spawnPosition in spawnPositions)
        {
            if(Vector2.Distance(position , spawnPosition) < 1.0f) return false;
        }
        return true;
    }
}
