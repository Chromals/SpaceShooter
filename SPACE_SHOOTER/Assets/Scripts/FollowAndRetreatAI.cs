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

    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(_rb.position, target.position);

        if (distance > stopDistance)
            _rb.position = Vector2.MoveTowards(_rb.position,target.position,speed * Time.fixedDeltaTime);

        else if (distance < retreatDistance)
            _rb.position = Vector2.MoveTowards(_rb.position, target.position, -speed * Time.fixedDeltaTime);
        else if(distance < stopDistance && distance > retreatDistance)
            _rb.position = this._rb.position;

        transform.right = target.position - transform.position;

        //Fire to player -> 40pts
    }
}
