using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExplosion : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PlayExplosionSound()
    {
        gameObject.transform.parent = null;
        audioSource.Play();
        StartCoroutine(DeleteThisObject());
    }
    private IEnumerator DeleteThisObject()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
