using UnityEditor.Experimental.GraphView;
using UnityEngine;
public enum ShootType
{
    None,
    P_Bullet,
    E_Bullet,
    Double_E_Bullet,
    SetTarget_E_Bullet,
    SetTarget_Double_E_Bullet
}
public class Bullet : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color color;
    ShootType ShootType;
    GameObject Player;
    public float BulletSpeed = 10f;
    public float PublicDamage;
    float Distance;
    private Vector3 direction;
    bool Target;

    public void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        Player = GameObject.FindGameObjectWithTag("Player");
        direction = Vector3.up.normalized;
    }
    public void Shoot(float _Damage, ShootType Type)
    {
        this.PublicDamage = _Damage;
        switch (Type)
        {
            case ShootType.E_Bullet:
                break;
            case ShootType.Double_E_Bullet:
                Target = false;
                Double_bullet(Target);
                DestroyBullet();
                break;
            case ShootType.SetTarget_E_Bullet:
                Target = true;
                Target_Bullet(Target);
                break;
            case ShootType.SetTarget_Double_E_Bullet:
                Target = true;
                Double_bullet(Target);
                DestroyBullet();
                break;
        }
    }
    void Target_Bullet(bool Target)
    {
        if (transform.position.x < Player.transform.position.x)
        {
            DestroyBullet();
        }
        spriteRenderer.color=Color.black;
        // �Ѿ��� ȸ�� ���� (Player�� �ٶ󺸵��� ����)
        Vector3 targetDirection = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // �Ѿ��� �⺻ ȸ�� ����
    }
    void Double_bullet(bool Target)
    {
        // �� ���� �Ѿ��� ����
        SpawnBulletWithOffset(PublicDamage, new Vector3(0, 0.3f, 0), Target); // ���ʿ� ����
        SpawnBulletWithOffset(PublicDamage, new Vector3(0, -0.3f, 0), Target); // �Ʒ��ʿ� ����
    }
    private void SpawnBulletWithOffset(float _Damage, Vector3 offset, bool Target)
    {
        // ���� �Ѿ��� ��ġ�� �������� �߰��� ���ο� �Ѿ� ����
        GameObject newBullet = Instantiate(this.gameObject, transform.position + offset, transform.rotation);
        Bullet bulletComponent = newBullet.GetComponent<Bullet>();
        if (Target)
        {
            bulletComponent.Shoot(_Damage, ShootType.SetTarget_E_Bullet); // Ÿ�� �Ѿ˷� �߻�
        }
        else
        {
            bulletComponent.Shoot(_Damage, ShootType.E_Bullet); // �Ϲ� �Ѿ˷� �߻�
        }
    }

    public void DestroyBullet()
    {
        B_PoolingManager.B_Pooling.ReturnObject(this.gameObject);
        if (this.gameObject.tag == "P_Bullet")
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        spriteRenderer.color = color;
    }
    void Update()
    {
        // ���� �������� �Ѿ� �̵�
        transform.Translate(direction * BulletSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == "E_Bullet" && collision.gameObject.tag == "Player")
        {
            DestroyBullet();
        }
        else if (this.gameObject.tag == "P_Bullet" && collision.gameObject.tag == "Enemy")
        {
            DestroyBullet();
        }
        else if(collision.gameObject.tag=="Border")
        {
            DestroyBullet();
        }
    }
}