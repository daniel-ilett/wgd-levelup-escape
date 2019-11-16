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
    }

    private void Update()
    {
        if (isFired)
        {
            fireTime -= Time.deltaTime;

            transform.position += transform.forward * Time.deltaTime * speed;
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

        if (other != null)
        {
            other.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
        }

        Debug.Log("Send explosion message to all in nearby area.");

        playerMech.ReturnMissile(this);
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
