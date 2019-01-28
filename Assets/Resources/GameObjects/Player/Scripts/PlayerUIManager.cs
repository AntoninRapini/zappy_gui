using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    private Text _levelText;
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private RectTransform _hpBarFill;

    void Update ()
    {
        _canvas.transform.rotation = Quaternion.Euler(0, 0, 0);
	}
	
	public void OnLevelChange(uint level)
    {
        _levelText.text = level.ToString();
    }

    public void OnLifeChange(uint life)
    {
        _hpBarFill.localScale = new Vector3(((float)life) / 10.0f, 1, 1);
    }
}
