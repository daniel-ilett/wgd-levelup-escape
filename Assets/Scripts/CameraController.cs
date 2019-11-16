using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    [SerializeField]
    private Transform focusTarget;

    [SerializeField]
    private float speed = 5.0f;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.fixedDeltaTime * speed);
        transform.LookAt(focusTarget, Vector3.up);
    }
}
