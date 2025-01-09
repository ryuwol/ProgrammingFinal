using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    public ShootType ShootType;
    public float bullet_delay = 0.25f;
    public float time_set = 0f;
    public PlayerData data;
    void Update()
    {
        Shot();
        Vector2 PlayerMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(PlayerMove.normalized * data.SpeedValue * Time.deltaTime);

    }
    void Shot()
    {
        time_set += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && bullet_delay <= time_set)
        {
            Bullet bullet = B_PoolingManager.B_Pooling.GetObject("P_Bullet");
            var direction = Vector3.up;
            bullet.transform.position = this.transform.position;
            bullet.Shoot(data.DamageValue, this.ShootType);
            time_set = 0;
        }
    }

}