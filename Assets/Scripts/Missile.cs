using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float damage = 50.0f;

    [SerializeField]
    private float fireTime = 3.0f;

    [SerializeField]
    private Transform fireCone;

    // State variables.
    private bool isFired = false;
    private PlayerMech playerMech;
    private float startFireTime;
    private string friendTag = "";

    private void Start()
    {
        startFireTime = fireTime;
    }

    public void SetMech(PlayerMech playerMech)
    {
        this.playerMech = playerMech;
    }

    public void SetTag(string tag)
    {
        friendTag = tag;
    }

    public void Fire()
    {
        transform.parent = null;
        isFired = true;
        fireCone.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isFired)
        {
            fireTime -= Time.deltaTime;

            transform.position += transform.forward * Time.deltaTime * speed;

            var fireScale = 0.15f + Mathf.Sin(Time.time * 25.0f) * 0.05f;
            fireCone.localScale = new Vector3(fireScale, fireScale, fireScale);
        }

        if(fireTime < 0.0f)
        {
            Explode(null);
            fireTime = startFireTime;
        }
    }

    private void Explode(Collider other)
    {
        isFired = false;

        var explosion = new ExplosionData(transform.position, 5.0f, damage);

        Debug.Log("Send explosion message to all in nearby area.");

        var objects = new List<Collider>(Physics.OverlapSphere(transform.position, 5.0f));

        for(int i = 0; i < objects.Count; ++i)
        {
            objects[i].gameObject.SendMessage("Hit", explosion, SendMessageOptions.DontRequireReceiver);
        }

        playerMech.ReturnMissile(this);
        fireCone.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // When the missile collides with a non-player, send message and deactivate.
        if(isFired && other.tag != friendTag && other.tag != transform.tag)
        {
            Explode(other);
        }
    }
}
