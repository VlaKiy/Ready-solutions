using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    [System.Serializable]
    public class DropCurrency
    {
        public string name;
        public GameObject objectPrefab;
        public int dropRarity;
    }

    [SerializeField] private List<DropCurrency> objTable = new List<DropCurrency>();

    public void MakeRandomDrop(Vector3 whereToSpawn)
    {
        int itemWeight = 0;

        for (int i = 0; i < objTable.Count; i++)
        {
            itemWeight += objTable[i].dropRarity;
        }

        int randomValue = Random.Range(0, itemWeight);

        for (int i = 0; i < objTable.Count; i++)
        {
            if (randomValue <= objTable[i].dropRarity)
            {
                if (objTable[i].objectPrefab != null)
                {
                    var obj = Instantiate(objTable[i].objectPrefab, whereToSpawn, objTable[i].objectPrefab.transform.rotation);

                    return;
                }
                else
                {
                    return;
                }
            }

            randomValue -= objTable[i].dropRarity;
        }
    } 
}
