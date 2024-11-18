using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 1.0f;
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 PlayerMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position = PlayerMove.normalized * PlayerSpeed;
    }
}
