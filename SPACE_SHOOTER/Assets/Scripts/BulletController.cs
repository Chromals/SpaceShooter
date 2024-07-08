using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float speed;


    Rigidbody2D _rb;

    Vector2 _direction;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = _direction * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CentinelController controller = other.gameObject.GetComponent<CentinelController>();


            //implement getPoints on centinelController -> 10pts  check
            float points = controller.GetPoints();

            //increase score -> 10pts -> check
            UIController.Instance.IncreaseScore(points);

            Destroy(other.gameObject);
            Destroy(gameObject);

        }// Aqui se llama la funcion para que el boss sufra 1 punto de dano
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossController bossController = other.gameObject.GetComponent<BossController>();
            bossController.TakeHit();

            Destroy(gameObject);
        }
    }


    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

}
