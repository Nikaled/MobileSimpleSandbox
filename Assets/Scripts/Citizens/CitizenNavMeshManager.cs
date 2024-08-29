using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenNavMeshManager : MonoBehaviour
{
    public static CitizenNavMeshManager instance;
    [SerializeField] private Collider teleportArea;
    [SerializeField] public NavMeshChecker Checker;
    private void Awake()
    {
        instance = this;
    }
    public Vector3? MoveCheckerToNewPoint(GameObject sphere)
    {
        sphere.SetActive(false);
        sphere.transform.position = RandomPointInBounds(teleportArea.bounds);
        //Debug.Log("sphere.transform.position"+sphere.transform.position);
        sphere.SetActive(true);
        return sphere.transform.position;
    }
    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x*0.9f, bounds.max.x * 0.9f),
            UnityEngine.Random.Range(bounds.max.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z * 0.9f, bounds.max.z * 0.9f)
        );
    }
}
