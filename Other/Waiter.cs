using System.Collections;
using UnityEngine;

public class Waiter : MonoBehaviour
{
    private bool _isWaiterEnd = false;

    public void StartWaiter()
    {
        StartCoroutine(MakeWaiter());
    }

    IEnumerator MakeWaiter()
    {
        var timer = 1;
        _isWaiterEnd = false;

        for (int i = 1; i <= timer; i++)
        {
            yield return new WaitForSeconds(1);

            if (i == timer)
            {
                _isWaiterEnd = true;
            }
        }
    }

    public bool GetWaiterStatus()
    {
        return _isWaiterEnd;
    }
}
