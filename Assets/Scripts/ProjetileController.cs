using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetileController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float force;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(rb.transform.right * force * -1);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.CompareTag("Enemy"))
            Destroy(other.gameObject);

        if (other.CompareTag("Parede"))
            Destroy(gameObject);
    }
}
