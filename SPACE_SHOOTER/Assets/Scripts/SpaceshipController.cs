using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float speed = 10.0F;

    [SerializeField]
    Vector2 edges;

    [SerializeField]
    bool handleClamp;


    [Header("Rotation")]
    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    bool mouseRotation;

    [SerializeField]
    float rotationTime = 0.05F;

    [Header("Firing")]
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float bulletLifeTime;

    Vector2 _move = Vector2.zero;

    Vector2 _mouseScreenPoint;

    Rigidbody2D _rb;

    float _rotationDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        HandleInputMove();
        HandleInputRotation();
        HandleFire();
       


    }



    private void FixedUpdate()
    {
        HandleRotation();
        if (_move.sqrMagnitude == 0.0F)
            return;

        HandleMove();

        HandleClamp();
        HandleTeleport();


        //forma de movimiento -> velocidad
        ///_rb.velocity = _move* speed *Time.fixedDeltaTime
    }
    private void HandleFire()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab,firePoint.position, transform.rotation);
            Destroy(bullet,bulletLifeTime);
        }
    }

    private void HandleInputRotation()
    {
        if (Input.GetKey(KeyCode.Q))
            _rotationDir = +1.0F;
        else if (Input.GetKey(KeyCode.E))
            _rotationDir = -1.0F;
        else
            _rotationDir = 0.0F;

        _mouseScreenPoint = Input.mousePosition;
    }

    private void HandleInputMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        _move = new Vector2(x, y);
    }

    private void HandleTeleport()
    {
        if (handleClamp)
            return;

        Vector2 currentPos = _rb.position;
        if (currentPos.x > 0.0F && currentPos.x >= edges.x)
        {
            _rb.position = new Vector2(-edges.x + 0.01F, currentPos.y);
        }
        else if (currentPos.x < 0.0F && currentPos.x <= -edges.x)
        {
            _rb.position = new Vector2(edges.x - 0.01F, currentPos.y);
        }
        else if (currentPos.y > 0.0F && currentPos.y >= edges.y)
        {
            _rb.position = new Vector2(currentPos.x, -edges.y + 0.01F);
        }
        else if (currentPos.y < 0.0F && currentPos.y <= -edges.y)
        {
            _rb.position = new Vector2(currentPos.x, edges.y - 0.01F);
        }
    }

    private void HandleClamp()
    {
        //esto es para que con la primera forma de movimiento (velocidad) no se saliera de las barreras de la pantalla
        if (!handleClamp)
            return;
        float x = Mathf.Clamp(_rb.position.x, -edges.x, edges.x);
        float y = Mathf.Clamp(_rb.position.y, -edges.y, edges.y);
        _rb.position = new Vector2(x, y);
    }

    private void HandleMove()
    {
        //forma de movimiento -> moverlo en una direccion
        Vector2 direction = _move.normalized;
        Vector2 currentPos = _rb.position;
        _rb.MovePosition(currentPos + direction * speed * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        if (mouseRotation)
        {
            Vector2 currentSP = Camera.main.WorldToScreenPoint(_rb.position);
            Vector2 direction = (_mouseScreenPoint - currentSP).normalized;


            //                  el atan2 esta en radianes       aqui lo hacemos en grados                 
            float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.MoveRotation(angleZ);
            return;
        }

        float currentRotation = _rb.rotation;
        if (_rotationDir != 0.0F)
        {
            float targetRotation = currentRotation + _rotationDir * rotationSpeed * Time.fixedDeltaTime;
            float rotation = Mathf.Lerp(currentRotation, targetRotation, rotationTime);
            _rb.rotation = rotation;
        }



        


        //otras formas
        ///transform.rotation = Quaternion.Euler(0.0F,0.0F,angle);
        ///transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
