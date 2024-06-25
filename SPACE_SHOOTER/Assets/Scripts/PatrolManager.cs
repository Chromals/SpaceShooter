using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolManager : MonoBehaviour
{
    [SerializeField]
    Path[] paths;

    [SerializeField]
    GameObject[] enemyPrefabs;

    [SerializeField]
    float spawnTimeout;

    float _currentTime;



    private void Awake()
    {
        foreach (Path path in paths)
        {
            Transform[] children = path.GetGameObject().transform.GetComponentsInChildren<Transform>();

            path.SetPoints(children);
        }
    }

    private void Spawn()
    {
        Path path = paths[Random.Range(0, paths.Length)];

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(prefab);

       
        PatrolController controller = enemy.GetComponent<PatrolController>();

        controller.setPathPoints(path.GetPoints());
    }


    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0.0F)
        {
            Spawn();
            _currentTime = Random.Range(spawnTimeout / 2.0F, spawnTimeout);
        }
    }
}
