using System.Collections;
using System.Collections.Generic;
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

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

}
