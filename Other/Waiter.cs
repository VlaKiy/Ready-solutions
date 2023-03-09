using System.Collections;
using UnityEngine;

public class Waiter : MonoBehaviour
{
    private bool _isWaiterEnd = true;
    private float _timeSeconds = 1f;

    public void StartWaiter(float seconds)
    {
        SetTimeSeconds(seconds);
        StartCoroutine(MakeWaiter());
    }

    IEnumerator MakeWaiter()
    {
        _isWaiterEnd = false;

        for (int i = 1; i <= _timeSeconds; i++)
        {
            yield return new WaitForSeconds(1);

            if (i == _timeSeconds)
            {
                _isWaiterEnd = true;
            }
        }
    }

    public bool GetWaiterStatus()
    {
        return _isWaiterEnd;
    }

    public float GetTimeSeconds()
    {
        return _timeSeconds;
    }
    private void SetTimeSeconds(float seconds)
    {
        _timeSeconds = seconds;
    }
}
