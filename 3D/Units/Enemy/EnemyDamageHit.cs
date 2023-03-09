using UnityEngine;

public class EnemyDamageHit : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<Player>())
        {
            coll.GetComponent<Player>().TakeDamage(_enemy.GetDamageCount());
        }
    }
}
