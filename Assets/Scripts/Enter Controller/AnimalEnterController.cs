using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEnterController : EnterController
{
    [SerializeField] AnimalRiding animalRiding;

    private readonly string AnalyticsRidingAnimal = "RidingAnimal";
    public override void SitIntoTransport()
    {
        IsPlayerIn = true;
        //HideEnterInstruction();
        HpView.SetActive(false);
        ActivateTransport();
        IsInterfaceActive = false;
        player.motor.SetPositionAndRotation(animalRiding.PlayerSittingTransform.position, animalRiding.PlayerSittingTransform.rotation, true);

       


        Analytics.instance.SendEvent(AnalyticsRidingAnimal);


    }
    protected override void ShowEnterInstruction()
    {
        base.ShowEnterInstruction();
        CanvasManager.instance.ShowCurrentInteracteButton(2);
    }
    public override void GetOutTransport()
    {
        animalRiding.DeactivateRiding();
        animalRiding.enabled = false;

        HpView.SetActive(true);
        player.PlayerParent.transform.position = PlayerSpawnTransform.position;
        player.motor.SetPositionAndRotation(PlayerSpawnTransform.position, PlayerSpawnTransform.rotation, true);
        player.PlayerSetActive(true);
        DeactivateTransport();
        IsPlayerIn = false;
        CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
        player.SwitchPlayerState(Player.PlayerState.Idle);
    }
    protected override void ActivateTransport()
    {


        animalRiding.enabled = true;
        animalRiding.ActivateRiding();
        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
        CanvasManager.instance.InteracteButton.onClick.AddListener(delegate { GetOutTransport(); });
        StartCoroutine(ShowExitTransportButton());
    }
    private IEnumerator ShowExitTransportButton()
    {
        yield return new WaitForSeconds(0.1f);
        CanvasManager.instance.InteracteButton.gameObject.SetActive(true);
    }
    protected override void DeactivateTransport()
    {
        //CanvasManager.instance.ShowHelicopterInstruction(false);
        //CanvasManager.instance.ShowHelicopterMobileInstruction(false);
        //if (Geekplay.Instance.mobile)
        //{
        //    //animalRiding.MyClearButtons();
        //    HelicopterButtons.instance.GetOutButton.onClick.RemoveAllListeners();
        //}
        animalRiding.DeactivateRiding();
        animalRiding.enabled = false;
    }
}
