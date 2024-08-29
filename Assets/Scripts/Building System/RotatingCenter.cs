using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCenter : MonoBehaviour
{
    Vector3 CashedPositionInObject;
    [SerializeField] public MeshRenderer RootObjectMesh;
    [Header("Transport only")]
    [SerializeField]public GameObject RootObjectEmpty;
    public bool IsManualSet;
    private void OnEnable()
    {

        if (IsManualSet == false)
        {
            gameObject.transform.position = RootObjectMesh.bounds.center;
        }
        if (RootObjectMesh != null)
        {
            gameObject.transform.SetParent(RootObjectMesh.gameObject.transform);

        }
        else if (RootObjectEmpty != null)
        {
            gameObject.transform.SetParent(RootObjectEmpty.gameObject.transform);
            //CashedPositionInObject = transform.localPosition;
            //gameObject.transform.parent = null;
        }
    }
    public void SetRotatingCenter()
    {
        if (IsManualSet == false)
        {
            if (RootObjectMesh != null)
                gameObject.transform.position = RootObjectMesh.bounds.center;
        }
        if (RootObjectMesh != null)
        {
            gameObject.transform.parent = RootObjectMesh.transform;
            gameObject.transform.parent = RootObjectMesh.transform.parent;
            RootObjectMesh.transform.parent = gameObject.transform;
        }
        else if (RootObjectEmpty != null)
        {
            gameObject.transform.parent = RootObjectEmpty.transform;
            gameObject.transform.parent = RootObjectEmpty.transform.parent;
            RootObjectEmpty.transform.parent = gameObject.transform;
        }
    }
    public void UnbindRotatingCenter()
    {
        if(RootObjectEmpty == null)
        {
        RootObjectMesh.transform.parent = gameObject.transform.parent;  
        gameObject.transform.parent = RootObjectMesh.transform.parent;
        }
        else
        {
            RootObjectEmpty.transform.parent = gameObject.transform.parent;
            gameObject.transform.parent = RootObjectEmpty.transform.parent;
        }
    }
}
