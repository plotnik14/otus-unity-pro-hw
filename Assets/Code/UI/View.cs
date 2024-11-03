﻿using UnityEngine;

namespace UI
{
    public class View : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}