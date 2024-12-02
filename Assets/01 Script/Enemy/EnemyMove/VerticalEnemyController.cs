using System.Data.SqlTypes;
using UnityEngine;

public class VerticalEnemyController : MonoBehaviour
{
    public EnemyData enemyData;
    private Vector3 direction;
    bool MoveState=true;
    private void OnEnable()
    {
        Turn();
    }
    void Update()
    {
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "UpDownBorder")
            Turn();
    }
    void Turn()
    {
        if (MoveState == true)
        {
            direction = Vector3.right;
            MoveState = false;
        }
        else if (MoveState == false)
        {
            direction = Vector3.left;
            MoveState = true;
        }
    }
}
