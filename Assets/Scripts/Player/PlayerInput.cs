using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public RaycastHit2D GetTargetHit() =>  Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

    public Vector2 GetTargetPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

}
