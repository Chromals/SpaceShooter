using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentinelController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpaceshipController controller = collision.gameObject.GetComponent<SpaceshipController>();
            controller.Die();
            Destroy(gameObject);
        }
    }
}
