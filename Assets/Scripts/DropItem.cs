using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropItem : MonoBehaviour
{
    public UnityEvent<HitPoint> OnHit;
    public UnityEvent<DropItem> OnDrop;

    [SerializeField] private float _speed = 1f;

    private PolygonCollider2D _collider;
    private Rigidbody2D _rb;
    private bool _isActive;

    private void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Push()
    {
        OnDrop?.Invoke(this);
        _isActive = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(Vector2.up * _speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive) return;

        if (collision.gameObject.GetComponent<DropItem>())
        {
            OnHit?.Invoke(HitPoint.DropItem);
            _isActive = false;
            _rb.velocity = Vector2.left;
            Destroy(_collider);
            Destroy(this.gameObject, 1f);
        }

        var target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            OnHit?.Invoke(HitPoint.Target);
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Static;
            transform.parent = target.Model.transform;
            _isActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive) return;

        if (other.GetComponent<Money>())
        {
            OnHit?.Invoke(HitPoint.Money);
            Destroy(other.gameObject);
        }
    }
}

public enum HitPoint
{
    None,
    Target,
    DropItem,
    Money
}
