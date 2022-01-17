using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class Button : MonoBehaviour
    {
        [SerializeField] public int _buttonId;

        public Action<int> OnClicked;

        public void OnSelectAnswer()
        {
            OnClicked?.Invoke(_buttonId);
        }
    }
}