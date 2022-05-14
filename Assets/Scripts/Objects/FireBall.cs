using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public Rigidbody2D arrowRigidbody;

    [Header("Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;

    [Header("Lifetime")]
    public Animator fireballAnim;

    // Start is called before the first frame update
    void Start()
    {
        lifetimeSeconds = lifetime;
    }

    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        arrowRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Entities"))
        {
            Destroy(this.gameObject);
        }
    }
}
