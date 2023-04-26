using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFader : MonoBehaviour
{
    [SerializeField]
    private float _high;
    [SerializeField] 
    private float _low;
    [SerializeField]
    private float _timeIn;
    [SerializeField]
    private float _timeOut;
    private CanvasGroup _canvasGroup;
    private Tween _tween;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup.alpha = this.isActiveAndEnabled ? _high : _low;
    }

    // Update is called once per frame
    private void OnEnable()
    {
        if (_tween != null) _tween.Kill();
        _canvasGroup.alpha = _low;
        _tween = _canvasGroup.DOFade(_high, _timeIn);
    }
    private void OnDisable()
    {
        if (_tween != null) _tween.Kill();
        _canvasGroup.alpha = _high;
        _tween = _canvasGroup.DOFade(_low, _timeOut);
    }
}
