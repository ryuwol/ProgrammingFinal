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
        Vector2 PlayerMove = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        transform.position = PlayerMove.normalized * PlayerSpeed;
    }
}
