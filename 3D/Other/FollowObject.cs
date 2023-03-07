using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject _followObj;
    [SerializeField] private bool _isMakeRotation = false;
    [SerializeField] private bool _awakeFollow = false;

    private bool _isStopFollow = false;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_awakeFollow)
        {
            Follow();
        }
    }

    public void Follow()
    {
        if (!_isStopFollow && _followObj != null)
        {
            var moveVelocity = (_followObj.transform.position - transform.position).normalized;
            _rb.MovePosition(_rb.position + moveVelocity * Time.deltaTime);

            if (_isMakeRotation)
            {
                transform.LookAt(new Vector3(_followObj.transform.position.x, transform.position.y, _followObj.transform.position.z));
            }
        }
        else if (_followObj == null)
        {
            Debug.LogError("followObj is null");
        }
    }

    public GameObject GetFollowObject()
    {
        return _followObj;
    }

    public void SetFollowObject(GameObject obj)
    {
        if (obj != null)
        {
            _followObj = obj;
        }
        else
        {
            Debug.LogError("Obj is null");
        }
    }

    public bool GetStopFollow()
    {
        return _isStopFollow;
    }

    public void SetStopFollow(bool status)
    {
        _isStopFollow = status;
    }
}