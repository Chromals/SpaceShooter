using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float sleepTime;


    [SerializeField]
    float lifeTime;

    [SerializeField]
    int maxPathPoints;


    Transform _nextPoint;
    Rigidbody2D _rb;
    Transform[] _pathPoints;

    float _aliveTime;
    int _maxPathPoints = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (_nextPoint == null)
            Next();

        Vector2 currentPos = _rb.position;
        Vector2 targetPos = _nextPoint.position;
        Vector2 movePos = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);

        _rb.MovePosition(movePos);
        if (Vector2.Distance(_rb.position, targetPos) < 0.2F)
        {
            if (_aliveTime <= 0.0F)
            {

                Next();
                _maxPathPoints--;
            }
            else
                _aliveTime -= Time.deltaTime;
        }

    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.position = _pathPoints[0].position;
        _maxPathPoints = Random.Range(maxPathPoints / 2, maxPathPoints);
        Next();
    }

    private void Start()
    {
        _rb.position = _pathPoints[0].position;
        _maxPathPoints = Random.Range(maxPathPoints / 2, maxPathPoints);
        Next();
    }

    private void Next()
    {
        if (_maxPathPoints <= 0)
        {
            Destroy(gameObject);
            return;
        }

        int pointNumber = Random.Range(2, _pathPoints.Length + 1);
        foreach (Transform t in _pathPoints)
        {
            if (t.name == "Point " + pointNumber.ToString())
            {
                _nextPoint = t;
                break;
            }
        }

        _aliveTime = lifeTime;
    }

    public void setPathPoints(Transform[] pathPoints)
    {
        _pathPoints = pathPoints;
        enabled = true;
    }
}
