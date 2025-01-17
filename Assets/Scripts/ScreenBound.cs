﻿using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ScreenBound : MonoBehaviour
{
    [SerializeField] private Vector2 boundingBox;
    [SerializeField] private bool destroyWhenFall = true;
    [SerializeField] private bool restrictX = true;

    private Camera _cachedCamera;
    private IDamageable _damageable;

    private void Awake()
    {
        _cachedCamera = Camera.main;
        _damageable = GetComponent<IDamageable>();
        if (_damageable == null)
        {
            _damageable = GetComponentInChildren<IDamageable>();
        }
    }

    private void Update()
    {
        Vector3 position = transform.position;

        Vector3 minScreenBounds = _cachedCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = _cachedCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


        if (destroyWhenFall && position.y < minScreenBounds.y - boundingBox.y)
        {
            if (_damageable != null)
            {
                HitInformation hitInformation = new HitInformation();
                hitInformation.IsAbsoluteDamage = true;
                _damageable.ApplyDamage(999999, this, hitInformation);
            }
            else
            {
                Destroy(gameObject);
            }
            return;
        }

        if (restrictX)
        {
            float x = Mathf.Clamp(position.x, minScreenBounds.x + boundingBox.x, maxScreenBounds.x - boundingBox.x);
            transform.position = new Vector3(x, position.y, position.z);
        }
    }
}
