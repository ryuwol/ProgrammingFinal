using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private StatisticsManager StatisticsManager;
    [SerializeField] private float Speed;
    [SerializeField] private int Hp;
    [SerializeField] private int Damage;
    public float SpeedValue => Speed;
    void Start()
    {
        StatisticsManager = new StatisticsManager(Speed, Hp, Damage);
        StatisticsManager.ReturnSpeed();
    }

}