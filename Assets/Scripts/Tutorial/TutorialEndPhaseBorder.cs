using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndPhaseBorder : TutorialPhaseBorders
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            TutorialCanvasManager.instance.ShowEndTutorialUI(true);
        }
        if (other.gameObject.layer == 11) // 11 - Enter collider for transport 
        {
           var EnterScript =  other.GetComponent<EnterController>();
            if(EnterScript != null)
            {
                if(EnterScript.IsPlayerIn == true)
                {
                    TutorialCanvasManager.instance.ShowEndTutorialUI(true);
                }
            }
        } 
    }
    
}
