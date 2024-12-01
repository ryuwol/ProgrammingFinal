using UnityEngine;

public class StatisticsManager
{
    public float Speed;
    public float Hp;
    public float Damage;
    public StatisticsManager(float _Speed, float _Hp, float _Damage)
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
    public float ReturnDamage()
    {
        return Damage;
    }
}