using UnityEngine;
using System.Collections;

public class OnLoadAd : MonoBehaviour
{
    private float _adIntervalInSeconds = 180f;
    [SerializeField] private bool _showAdOnStart = false;

    private void Start()
    {
        if (_showAdOnStart)
        {
            StartCoroutine(ShowAd());
        }
    }

    private IEnumerator ShowAd()
    {
        while (true)
        {
            Show();

            yield return new WaitForSeconds(_adIntervalInSeconds);
        }
    }

    public void Show()
    {
        Application.ExternalCall("ShowAd");
        print("Ad!");
    }
}
