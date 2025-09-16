using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class DissolveHelper : MonoBehaviour
    {
        [SerializeField] private Renderer renderer;

        [SerializeField] private float dissolveTime = 1;
        [SerializeField] private string _propertyName = "_Cutoff_Height";

        private int _id;

        public event UnityAction OnDissolve;
        public event UnityAction OnRessolve;
        private void Awake()
        {
            _id = Shader.PropertyToID("_Cutoff_Height");
            renderer.material.SetFloat(_id, 1);
        }

        [ContextMenu("Dissolve")]
        public void Dissolve()
        {
            DOTween.To(
                () => renderer.material.GetFloat(_id),          // getter
                x => renderer.material.SetFloat(_id, x),        // setter
                -1,                                              // target value
                dissolveTime                                               // duration
            ).OnComplete(() => OnDissolve?.Invoke());
        }

        [ContextMenu("Ressolve")]
        
        public void Ressolve()
        {
            DOTween.To(
                () => renderer.material.GetFloat(_id),          // getter
                x => renderer.material.SetFloat(_id, x),        // setter
                1,                                              // target value
                dissolveTime                                               // duration
            ).OnComplete(() => OnRessolve?.Invoke());;
        }

        private void OnValidate()
        {
            #if UNITY_EDITOR
            renderer = GetComponent<Renderer>();
            #endif
        }
    }
}