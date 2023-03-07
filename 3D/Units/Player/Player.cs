using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    enum HowMove
    {
        Joystick,
        Keyboard,
        All
    }

    [SerializeField] private GameObject _healthBar;
    [SerializeField] private bool _isMakeRotation = false;
    [SerializeField] private HowMove _howMove = HowMove.Joystick;
    /*[SerializeField] private Joystick _joystick;*/

    [Header("Audio")]
    [SerializeField] private AudioSource _fireAudio;

    private GameObject _canvas;
    private float _maxHp;
    private bool _isCanShooting = true;
    private Rigidbody _rb;

    private Vector3 _moveVelocity;
    private string _oldCollName;
    private int _trashCount = 0;

    private void Awake()
    {
        _canvas = GameObject.Find("Canvas");
        _maxHp = GetHP();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isMakeRotation 
            && _attackEnemy != null &&
            !_attackEnemy.GetComponent<Enemy>().IsAlive() &&
            Vector3.Distance(transform.position, _attackEnemy.transform.position) <= _attackDistance)
        {
            transform.LookAt(new Vector3(_attackEnemy.transform.position.x, transform.position.y, _attackEnemy.transform.position.z));
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        /*if (_howMove == HowMove.Joystick)
        {
            Vector3 moveInput = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            Vector3 moveVector = (Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal);

            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(moveVector, Vector3.up);
                _animator.SetBool("Run", true);
            }
            else
            {
                _animator.SetBool("Run", false);
            }
            _moveVelocity = moveInput.normalized * _speed;
            _rb.MovePosition(_rb.position + _moveVelocity * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Other modes of movement are temporarily unavailable");
        }*/
    }

    protected override void Die()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Game");
        }
    }

    public override void TakeDamage(float damageCount, GameObject whoAttack = null)
    {
        base.TakeDamage(damageCount);
        _animator.SetTrigger("TakeDamage");
        /*_canvas.GetComponent<HealthbarChanger>().UpdateHealthBar(_healthBar, _hp, _maxHp);*/
    }

    public void TakeHeal(float healCount)
    {
        _hp += healCount;

        if (_hp > 100)
        {
            _hp = 100;
        }
        /*_canvas.GetComponent<HealthbarChanger>().UpdateHealthBar(_healthBar, _hp, _maxHp);*/
    }

    IEnumerator Waiter()
    {
        var timer = 1;
        _isCanShooting = false;

        for (int i = 1; i <= timer; i++)
        {
            yield return new WaitForSeconds(1);

            if (i == timer)
            {
                _isCanShooting = true;
            }
        }
    }
}
