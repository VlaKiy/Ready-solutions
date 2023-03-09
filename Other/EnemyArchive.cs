using System.Collections.Generic;
using UnityEngine;

public class EnemyArchive : MonoBehaviour
{
    [SerializeField] private GameObject _addThisObjInEnemies;

    private List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        if (_addThisObjInEnemies != null)
        {
            Add(gameObject);
        }
    }

    public void Add(GameObject enemy)
    {
        _enemies.Add(enemy);
    }

    public List<GameObject> GetEnemies()
    {
        return _enemies;
    }
}
