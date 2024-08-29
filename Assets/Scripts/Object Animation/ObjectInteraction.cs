using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private bool _isPlayerNear;
    private bool _objectActivated;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Player.instance.currentState != Player.PlayerState.Idle)
            {
                return;
            }
            CanvasManager.instance.ShowObjectInteructInstruction(true);
            _isPlayerNear = true;
            CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();

            CanvasManager.instance.InteracteButton.onClick.AddListener(delegate { ActivateObject(); });
            CanvasManager.instance.ShowCurrentInteracteButton(3);
            CanvasManager.instance.InteracteButton.gameObject.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasManager.instance.ShowObjectInteructInstruction(false);
            _isPlayerNear = false;
            CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
            CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        if (_isPlayerNear == false || Player.instance.AdWarningActive)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Pressed");
            if (_objectActivated == false && Player.instance.currentState == Player.PlayerState.Idle)
            {
                ActivateObject();
                _objectActivated = true;
                return;
            }
            else
            {
                DeactivateObject();
                _objectActivated = false;
                return;
            }
        }

    }
    private void OnDestroy()
    {
        if (CanvasManager.instance != null)
        {
            CanvasManager.instance.ShowObjectInteructInstruction(false);
            CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
        }

    }
    protected virtual void ActivateObject()
    {
        if (_objectActivated == true)
        {
            return;
        }
        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
        CanvasManager.instance.InteracteButton.onClick.AddListener(delegate { DeactivateObject(); });
        _objectActivated = true;
    }
    protected virtual void DeactivateObject()
    {
        if (_objectActivated == false)
        {
            return;
        }
        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
        CanvasManager.instance.InteracteButton.onClick.AddListener(delegate { ActivateObject(); });
        _objectActivated = false;
    }
}
