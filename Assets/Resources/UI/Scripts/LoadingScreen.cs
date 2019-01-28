using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private const string _connectingMessage = "Connecting...";
    private const string _mapLoadingMessage = "Loading map...";
    [SerializeField]
    private Text _text;

    public void ShowConnectingMessage()
    {
        _text.text = _connectingMessage;
    }

    public void ShowMapLoadingMessage()
    {
        _text.text = _mapLoadingMessage;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
