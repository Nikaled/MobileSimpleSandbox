using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pulse : MonoBehaviour
{
    public Transform TransformOfOtherObject;
    IEnumerator Start()
    {
        if(TransformOfOtherObject == null)
        {
            TransformOfOtherObject = gameObject.transform;
        }
        while (true)
        {
            TransformOfOtherObject.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 1);
            yield return new WaitForSeconds(0.5f);
            TransformOfOtherObject.DOScale(new Vector3(1f, 1f, 1f), 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

