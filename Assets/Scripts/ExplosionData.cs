using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This struct contains the origin and size of an explosion.
public struct ExplosionData
{
    public Vector3 position;
    public float size;
    public float damage;

    public ExplosionData(Vector3 position, float size, float damage)
    {
        this.position = position;
        this.size = size;
        this.damage = damage;
    }
}
