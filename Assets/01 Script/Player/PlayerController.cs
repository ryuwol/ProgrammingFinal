using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    public float bullet_delay = 0.25f;
    public float time_set = 0f;
    public PlayerData data;
    void Update()
    {
     Shot();
    }
    void Shot()
    {
        time_set += Time.deltaTime;
        Vector2 PlayerMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(PlayerMove.normalized * data.SpeedValue * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && bullet_delay <= time_set)
        {
            var bullet = B_PoolingManager.GetObject(BulletType.Player);
            var direction = Vector3.up;
            bullet.transform.position = this.transform.position;
            bullet.Shoot(direction.normalized, data.DamageValue);
            time_set = 0;
        }
    }

}