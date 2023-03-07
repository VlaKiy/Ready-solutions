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
            UpdateEnemies(gameObject);
        }
    }

    public void UpdateEnemies(GameObject enemy)
    {
        _enemies.Add(enemy);
    }

    public List<GameObject> GetEnemies()
    {
        return _enemies;
    }
}
