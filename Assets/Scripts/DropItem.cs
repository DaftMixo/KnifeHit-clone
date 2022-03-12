using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private bool _isActive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive) return;
    }
}
