using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.position.x < Screen.width / 2f) player.MoveLeft();
        else if (eventData.position.x > Screen.width / 2f) player.MoveRight();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.MoveUp();
    }

}
