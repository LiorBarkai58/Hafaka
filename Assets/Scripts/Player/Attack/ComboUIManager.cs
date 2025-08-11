using System;
using DG.Tweening;
using EventSystem;
using TMPro;
using UnityEngine;

namespace Player.Attack
{
    public class ComboUIManager : MonoBehaviour
    {
        [SerializeField] private IntEventListener comboCountListener;
        
        [SerializeField] private TextMeshProUGUI comboText;

        private Tween currentTween;
        private void Start()
        {
            comboCountListener.OnEvent += UpdateCounter;
        }

        private void UpdateCounter(int counter)
        {
            currentTween?.Kill();
            comboText.rectTransform.localScale = Vector3.one; // Reset to original scale
            currentTween = comboText.rectTransform.DOPunchScale(
                new Vector3(1f, 1f, 0f), // Smaller punch — more subtle, no heavy shrink
                0.3f, // Shorter duration for snappiness
                6, // Vibrato: how many shakes
                0.8f);
            comboText.SetText($"x{counter}");
        }
    }
}