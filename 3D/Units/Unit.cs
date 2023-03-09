using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected enum TypeOfAttack
    {
        Nothing,
        AttackWithAnimation,
        Shoot
    }

    [Header("Base settings")]
    [SerializeField] private protected float _hp;
    [SerializeField] private protected float _speed;
    [SerializeField] private protected float _attackDistance;
    [SerializeField] private protected float _damageCount;
    [SerializeField] private protected TypeOfAttack _typeOfAttack = TypeOfAttack.Nothing;

    [Header("Additional components")]
    [SerializeField] private protected EnemyArchive _enemyArchive;

    [Header("Animation")]
    [SerializeField] private protected Animator _animator;
    [Space]

    private protected GameObject _attackEnemy;
    private protected GameObject _attackingObj;

    private void Awake()
    {
        if (TryGetComponent<Animator>(out Animator anim))
        {
            _animator = anim;
        }
    }

    protected void AttackWithAnimation(GameObject attackObj) // Assign a game object with a EnemyDamageHit script to the attacking hand of the enemy
    {
        if (_animator != null)
        {
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(transform.position.x, attackObj.transform.position.y, transform.position.z), transform.forward, out hit))
            {
                _attackingObj = attackObj;
                _animator.SetBool("Run", false);
                _animator.SetTrigger("Attack");
            }
        }
        else
        {
            Debug.LogError("Animator is null");
        }
    }

    private GameObject ShootChecker() // Import to make it work: EnemyArchive, Enemy
    {
        var enemies = _enemyArchive.GetEnemies();
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.TryGetComponent<Enemy>(out Enemy enemyInfo))
                {
                    if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= _attackDistance && !enemyInfo.IsAlive())
                    {
                        if (_attackEnemy == enemy)
                        {
                            return enemy;
                        }
                        else if (_attackEnemy != null &&
                            Vector3.Distance(transform.position, _attackEnemy.transform.position) > _attackDistance)
                        {
                            return enemy;
                        }
                        else if (_attackEnemy == null)
                        {
                            return enemy;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Debug.LogError("Script Enemy not found. Maybe script Enemy not imported");
                    return null;
                }
            }
            return null;
        }
        else
        {
            Debug.LogError("Script EnemyArchive not imported");
            return null;
        }
    }

    private void Shoot() //Import to make it work: Enemy, Waiter
    {
        var enemy = ShootChecker();

        if (enemy.TryGetComponent<Enemy>(out Enemy enemyInfo))
        {
            _attackEnemy = enemy;
            enemyInfo.TakeDamage(_damageCount, gameObject);
            if (enemyInfo.IsAlive())
            {
                if (TryGetComponent<Waiter>(out Waiter waiter))
                {
                    waiter.StartWaiter();
                }
                else
                {
                    Debug.LogError("Script Waiter not found");
                }
            }
        }
        else
        {
            Debug.LogError("Script Enemy not found");
        }
    }

    protected virtual void Die()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }
    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public float GetAttackDistance()
    {
        return _attackDistance;
    }
    public void SetAttackDistance(float newAttackDistance)
    {
        _attackDistance = newAttackDistance;
    }

    public float GetDamageCount()
    {
        return _damageCount;
    }
    public void SetDamageCount(float newDamageCount)
    {
        _damageCount = newDamageCount;
    }

    public float GetHP()
    {
        return _hp;
    }
    public void SetHP(float newHP)
    {
        _hp = newHP;
    }

    public GameObject GetAttackingObj()
    {
        return _attackingObj;
    }

    public virtual void TakeDamage(float damageCount, GameObject whoAttack = null)
    {
        _hp -= damageCount;

        Die();
    }

    public bool IsAlive()
    {
        if (gameObject)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
