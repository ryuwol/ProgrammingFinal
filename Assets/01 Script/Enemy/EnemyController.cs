using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData;
    private Vector3 direction;

    private void OnEnable()
    {
        direction = Vector3.up;
    }
    void Update()
    {
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }
}