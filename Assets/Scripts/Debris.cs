using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private new Collider collider;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Hit(ExplosionData explosion)
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;

        rigidbody.AddExplosionForce(1000.0f, explosion.position - new Vector3(0.0f, 2.5f, 0.0f), explosion.size + 2.5f);
    }

    public void ResetPart()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;

        transform.position = startPos;
        transform.rotation = startRot;
    }
}
