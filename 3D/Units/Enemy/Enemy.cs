using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Enemy : Unit
{
    [SerializeField] private FollowObject _followObject;

    private protected bool _isAttacking = false;
    private protected GameObject _oldAttackingObj;
    private protected Rigidbody _rb;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_typeOfAttack == TypeOfAttack.AttackWithAnimation)
        {
            AttackWithAnimation(_followObject.GetFollowObject());
        }
        else if (_typeOfAttack != TypeOfAttack.Nothing)
        {
            Debug.LogWarning("Other attack types are temporarily unavailable");
        }
        
        if (_hp <= 0)
        {
            if (_animator != null)
            {
                // Add in end animation event Die()
                _animator.SetTrigger("IsDead");
                _followObject.SetStopFollow(true);
                _animator.SetBool("Run", false);
            }
        }

        if (_followObject != null && _followObject.GetStopFollow())
        {
            _animator.SetBool("Run", false);
        }
        else if (_followObject == null)
        {
            Debug.LogError("Script FollowObject not found. Maybe script FollowObject not assigned");
        }
        else
        {
            _animator.SetBool("Run", true);
        }

        if (_isAttacking)
        {
            if (_oldAttackingObj == null)
            {
                if (_followObject != null)
                {
                    _animator.SetBool("Run", true);
                    _followObject.SetFollowObject(_attackingObj);
                }
                else if (_followObject == null)
                {
                    Debug.LogError("Script FollowObject not found. Maybe script FollowObject not assigned");
                }
            }
            
            if (_attackingObj != null)
            {
                transform.LookAt(new Vector3(_attackingObj.transform.position.x, transform.position.y, _attackingObj.transform.position.z));
            }
        }
    }

    public override void TakeDamage(float damageCount, GameObject whoAttack = null)
    {
        base.TakeDamage(damageCount, whoAttack);
        _animator.SetTrigger("TakeDamage");

        _isAttacking = true;
    }

    protected override void Die()
    {
        if (_hp <= 0)
        {
            // Just uncomment this code if you need
            /*if (TryGetComponent<RandomDrop>(out RandomDrop randDrop))
            {
                randDrop.MakeRandomDrop(transform.position);
            }
            else
            {
                Debug.LogWarning("RandomDrop not found. There will be no drop");
            }*/

            Destroy(gameObject);
        }
    }
}
