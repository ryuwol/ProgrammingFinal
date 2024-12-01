using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private StatisticsManager StatisticsManager;
    [SerializeField] private float Speed;
    [SerializeField] private float Hp;
    [SerializeField] private float Damage;
    Bullet Bullet;
    EnemyData enemyData;
    public float SpeedValue => Speed;
    public float DamageValue => Damage;
    public float HpValue => Hp;
    void Start()
    {
        StatisticsManager = new StatisticsManager(Speed, Hp, Damage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "E_Bullet")
        {
            Bullet = collision.gameObject.GetComponent<Bullet>();
            Hp -= Bullet.PublicDamage;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            enemyData = collision.gameObject.GetComponent<EnemyData>();
            Hp -= enemyData.DamageValue;
        }
    }
}