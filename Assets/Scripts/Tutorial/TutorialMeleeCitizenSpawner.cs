using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMeleeCitizenSpawner : MonoBehaviour
{
    public static TutorialMeleeCitizenSpawner instance;
    [SerializeField] GameObject BoxerPrefab;
    private void Awake()
    {
        instance = this;
    }
    public void CreateNewUnit(Vector3 DeadUnitPosition, Quaternion DeadUnitRotation, bool IsWhite)
    {
        StartCoroutine(CreateUnitAfterDelay());
        IEnumerator CreateUnitAfterDelay()
        {
            yield return new WaitForSeconds(2.4f);
          var NewBoxer=  Instantiate(BoxerPrefab, DeadUnitPosition, DeadUnitRotation);
            if (IsWhite)
            {
                NewBoxer.GetComponent<TutorialBoxingCitizen>().ChangeTexturesToWhite(true);
            }
        }

    }
}
