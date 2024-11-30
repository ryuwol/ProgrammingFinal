using UnityEngine;

public class StatisticsManager
{
    private float Speed;
    private int Hp;
    private int Damage;
    public StatisticsManager(float _Speed, int _Hp, int _Damage)
    {
        this.Speed = _Speed;
        this.Hp = _Hp;
        this.Damage = _Damage;
    }
    public void DecreasingHP()
    {
        Hp -= Damage;
    }
    public void Die(GameObject go)
    {
        go.SetActive(false);
    }
    public float ReturnSpeed()
    {
        return Speed;
    }
}