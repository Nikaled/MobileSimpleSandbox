using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimalMovementManager : MonoBehaviour
{
    public static TutorialAnimalMovementManager instance;
    [SerializeField] BoxCollider SpawnArea;
    [SerializeField] BoxCollider SpawnAreaAnimalLeft;
    [SerializeField] BoxCollider SpawnAreaForCar;
    [SerializeField] public NavMeshChecker Checker;
    private void Awake()
    {
        instance = this;
    }
    public Vector3? MoveCheckerToNewPoint(GameObject sphere)
    {
        sphere.SetActive(false);
        sphere.transform.position = RandomPointInBounds(SpawnArea.bounds);
        sphere.SetActive(true);
        return sphere.transform.position;
    }
    public Vector3? MoveCheckerToNewPointToLeft(GameObject sphere)
    {
        sphere.SetActive(false);
        sphere.transform.position = RandomPointInBounds(SpawnAreaAnimalLeft.bounds);
        sphere.SetActive(true);
        return sphere.transform.position;
    }
    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.max.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    public Vector3? MoveCheckerToNewPointForCar(GameObject sphere)
    {
        sphere.SetActive(false);
        sphere.transform.position = RandomPointInBounds(SpawnAreaForCar.bounds);
        sphere.SetActive(true);
        return sphere.transform.position;
    }
}
