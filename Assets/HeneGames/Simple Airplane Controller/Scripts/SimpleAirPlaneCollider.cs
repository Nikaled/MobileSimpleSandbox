using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeneGames.Airplane
{
    public class SimpleAirPlaneCollider : MonoBehaviour
    {
        public bool collideSometing;
        public LayerMask ColliderLayerMask;
        [HideInInspector]
        public SimpleAirPlaneController controller;
        public PlaneEnterController enterController;

        private void OnTriggerEnter(Collider other)
        {
            //Collide someting bad
            //if (other.gameObject.GetComponent<SimpleAirPlaneCollider>() == null && other.gameObject.GetComponent<LandingArea>() == null)
            //{
            //    collideSometing = true;
            //}
            if (other.CompareTag("Ground") || other.CompareTag("Road"))
            {
                Debug.Log("Столкновение с землей");
                controller.airplaneState = SimpleAirPlaneController.AirplaneState.Landing;
              
            }
            if ((ColliderLayerMask & (1 << other.gameObject.layer)) != 0)
            {
                Debug.Log("Столкновение с объектом по маске:" + other.gameObject.name + "/" + other.gameObject.layer);
                controller.airplaneState = SimpleAirPlaneController.AirplaneState.Landing;
            }
            //if (other.CompareTag("MapBorder"))
            //{
            //    if(controller.airplaneState == SimpleAirPlaneController.AirplaneState.Takeoff)
            //    controller.ExitOnMapBorderCollision();
            //}
            //if (Physics.Raycast(gameObject.transform.position, Vector3.down, out RaycastHit raycastHit, 45))
            //{
            //    Debug.Log(raycastHit.collider.gameObject.name);
            //    if (Vector3.Distance(raycastHit.collider.transform.position, gameObject.transform.position) < gameObject.GetComponent<Collider>().bounds.size.y*2)
            //    {
            //        Debug.Log("самолет посажен");
            //        controller.airplaneState = SimpleAirPlaneController.AirplaneState.Landing;
            //    }
            //}
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("MapBorder"))
            {
                //controller.airplaneState = SimpleAirPlaneController.AirplaneState.Landing;
                controller.ForceBack();
            }
        }
    }
}