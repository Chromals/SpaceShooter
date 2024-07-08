using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndRetreatAI : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float speed;

    [SerializeField]
    float stopDistance;

    [SerializeField]
    float retreatDistance;

    [Header("Fire")]
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject bulletPrefab;

    //[SerializeField]
    //float fireRate;

    [SerializeField]
    float bulletLifeTime;

    [SerializeField]
    float fireTimeout;

    float _fireTimer;
    float nextFireTime;
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _fireTimer = fireTimeout;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(_rigidbody.position, target.position);

        if (distance > stopDistance)
        {
            _rigidbody.position = Vector2.MoveTowards(_rigidbody.position, target.position, speed * Time.fixedDeltaTime);

        }
        else if (distance < stopDistance)
        {
            _rigidbody.position = Vector2.MoveTowards(_rigidbody.position, target.position, -speed * Time.fixedDeltaTime);

        }
        else if (distance < stopDistance && distance > retreatDistance)
        {
            _rigidbody.position = this._rigidbody.position;
        }

        transform.right = target.position - transform.position;

        //FIRE TO PLAYER => 40pts
        _fireTimer -= Time.fixedDeltaTime;

        if (_fireTimer <= 0.0F)
            HandleBossFire();
        
    }

    private void HandleBossFire()
    {
        


        Vector2 direction = (firePoint.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward));

        BossBulletController controller = bullet.GetComponent<BossBulletController>();
        controller.SetDirection(direction);
        _fireTimer = fireTimeout;
        Destroy(bullet, bulletLifeTime);


    }
}