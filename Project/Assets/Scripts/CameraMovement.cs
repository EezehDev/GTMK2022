using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform = null;
    [SerializeField] private float _depthValue = -10f;
    [SerializeField] private float _followSpeed = 3f;

    private void Start()
    {
        if (!_playerTransform) return;

        Vector3 targetPos = _playerTransform.position;
        targetPos.z = _depthValue;
        transform.position = targetPos;
    }

    private void FixedUpdate()
    {
        if (!_playerTransform) return;

        Vector3 a = transform.position;
        Vector3 b = _playerTransform.position;
        b.z = _depthValue;
        float t = _followSpeed * Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(a, b, t);
    }
}
