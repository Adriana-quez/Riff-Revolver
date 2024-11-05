using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseToTheBeat : MonoBehaviour
{
    [SerializeField] float _pulseSize = 1.2f;
    [SerializeField] float _returnSpeed = 5f;
    private Vector2 _startSize;
    void Start()
    {
        _startSize = transform.localScale;

    }

    void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
    }

    public void Pulse()
    {
        transform.localScale = _startSize * _pulseSize;
    }
}
