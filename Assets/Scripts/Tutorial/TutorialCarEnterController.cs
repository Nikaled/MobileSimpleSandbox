using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCarEnterController : CarEnterController
{
    [SerializeField] GameObject SpawnedCarPrefabOnSitting;
    [SerializeField] GameObject CarOnScene;
    [SerializeField] GameObject CarRoot;
    public override void SitIntoTransport()
    {
       var NewCar =  Instantiate(SpawnedCarPrefabOnSitting, CarOnScene.transform.position, Quaternion.identity);
       var CarEnter = NewCar.GetComponentInChildren<CarEnterController>();
        CarEnter.ForceEnter();
        Destroy(CarRoot);
    }
}
