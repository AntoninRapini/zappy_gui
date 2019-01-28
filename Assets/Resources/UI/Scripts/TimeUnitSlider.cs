using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeUnitSlider : MonoBehaviour, IPointerUpHandler
{
  
    private Slider _slider;
    float _prevValue;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _prevValue = _slider.value;
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (_prevValue != _slider.value)
        {
            GameManager.Instance.TimeUnitReciprocal = _slider.value;
            _prevValue = _slider.value;
        }

    }
}
