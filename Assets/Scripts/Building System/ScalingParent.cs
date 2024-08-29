using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingParent : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToScale;
  public   Vector3 DefaultObjectScale;
    public void SetThisAsParent()
    {
        gameObject.transform.parent = null;
        Debug.Log("gameObject.transform.localScale" + gameObject.transform.localScale);
        ObjectToScale.transform.SetParent(gameObject.transform);
        Debug.Log("gameObject.transform.localScale" + gameObject.transform.localScale);
    }
    public void SetThisAsChild()
    {
        ObjectToScale.transform.parent = null;
        gameObject.transform.SetParent(ObjectToScale.transform);

    }
}
