using System;
using UnityEngine;
using DG.Tweening;

public class DoTweenText : MonoBehaviour
{
    public Action OnCompleteTask;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private float _duration;
    [SerializeField] private string _endText; 


    public void AnimateText()
    {
        _text.text = "";
        var progress = 0f;

        DOTween.To(() => progress,
        (float lenght) =>
        {
            progress = lenght;
            _text.text = _endText.Substring(0, (int)progress);
        },
            _endText.Length,
            _duration
        ).SetEase(Ease.Linear)
        .OnComplete(() => OnCompleteTask?.Invoke());
    }
}