using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData data;
    void Update()
    {
        Vector2 PlayerMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(PlayerMove.normalized * data.SpeedValue * Time.deltaTime);
    }
}