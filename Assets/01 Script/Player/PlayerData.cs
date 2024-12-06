using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerData : MonoBehaviour
{
    GameObject TextObject;
    public TextMeshProUGUI HpUi;
    private StatsManager StatisticsManager;
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
        TextObject = GameObject.Find("HP");
        HpUi =TextObject.GetComponent<TextMeshProUGUI>();
        HpUi.text = "HP : " + Hp.ToString();
        StatisticsManager = new StatsManager(Speed, Hp, Damage);
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
            if (Hp<=0)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
        HpUi.text = "HP : " + Hp.ToString();
    }
}