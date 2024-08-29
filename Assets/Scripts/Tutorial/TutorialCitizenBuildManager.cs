using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCitizenBuildManager : MonoBehaviour
{
    public static TutorialCitizenBuildManager instance;
    [SerializeField] GameObject[] ObjectsToSpawn;
    private Queue<GameObject> ObjectsOnScene;
    [SerializeField] BoxCollider SpawnArea;
    [SerializeField] BoxCollider LeftSpawnArea;
    [SerializeField] public NavMeshChecker Checker;
    private int TimerToAbleBuilding = 3;
    private int MaxBuildingCount = 30;
    private void Awake()
    {
        instance = this;
        ObjectsOnScene = new();
    }
    public Vector3? MoveCheckerToNewPoint(GameObject sphere)
    {
        sphere.SetActive(false);
        sphere.transform.position = RandomPointInBounds(SpawnArea.bounds);
        //Debug.Log("sphere.transform.position"+sphere.transform.position);
        sphere.SetActive(true);
        return sphere.transform.position;
    }
    public Vector3? MoveCheckerToNewPointLeftSide(GameObject sphere)
    {
        sphere.SetActive(false);
        sphere.transform.position = RandomPointInBounds(LeftSpawnArea.bounds);
        //Debug.Log("sphere.transform.position"+sphere.transform.position);
        sphere.SetActive(true);
        return sphere.transform.position;
    }
    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.max.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z )
        );
    }
    public void BuildObject(Vector3 CitizenTransformPosition) 
    {
        int RandomObjectIndex = Random.Range(0, ObjectsToSpawn.Length);
        //Quaternion RandomRotate = 
        GameObject newObj = Instantiate(ObjectsToSpawn[RandomObjectIndex], CitizenTransformPosition, Quaternion.identity);
        ObjectsOnScene.Enqueue(newObj);
        newObj.transform.parent = gameObject.transform;
        if(ObjectsOnScene.Count > MaxBuildingCount)
        {
        var ObjectToDelete = ObjectsOnScene.Dequeue();
            Destroy(ObjectToDelete);
        }
    }

}
