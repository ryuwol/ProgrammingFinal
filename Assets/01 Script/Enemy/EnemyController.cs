using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum MovementPattern
    {
        Straight,        // ���� �̵�
        UpDown,          // �� �Ʒ� �ݺ�
        Sinfunction,      // ���� �̵�
        Diagonal         // �밢�� �̵�
    }

    public EnemyData enemyData;               // ���� ������
    public MovementPattern movementPattern;    // ���� �̵� ����
    private Vector3 direction;                 // ���� �̵� ����
    bool Turn=false;                          //UpDown ���ϰ� �밢�� ���Ͽ��� �ݶ��̴��� ��Ҵ��� Ȯ��
    private bool isMovingUp = true;           // UpDown ���Ͽ��� ���� �̵� ������ ����

    private void OnEnable()
    {
        SetTransform();
    }

    void Update()
    {
        // ���Ͽ� �´� �̵� ó��
        switch (movementPattern)
        {
            case MovementPattern.Straight:
                MoveStraight();
                break;
            case MovementPattern.UpDown:
                MoveUpDown();
                break;
            case MovementPattern.Sinfunction:
                MoveSinfunction();
                break;
            case MovementPattern.Diagonal:
                MoveDiagonal();
                break;
        }
    }
    private void MoveStraight()
    {
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }
    private void MoveUpDown()
    {
        // UpDownBorder�� �浹 ������ ����
        if (Turn)
        {
            isMovingUp = !isMovingUp; // ���� ����
            Turn = false;
        }
        direction = isMovingUp ? Vector3.left : Vector3.right;
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }
    private void MoveSinfunction()
    {
        float xOffset = Mathf.Sin(Time.time * enemyData.SpeedValue); // �ð��� ���� X������ �̵�
        direction = new Vector3(xOffset, 1.5f, 0); // Y��� �Բ� X�� �̵�
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }

    private void MoveDiagonal()
    {
        // UpDownBorder�� �浹 ������ ����
        if (Turn)
        {
            // �밢�� ���� ����
            direction = new Vector3(-direction.x, 0.5f, 0);
            Turn = false;
        }
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }
    public void SetTransform()
    {
        // �ʱ� ���� ����
        direction = Vector3.up;
        gameObject.transform.position = new Vector3(9.5f, Random.Range(-4.3f, 4.3f));
        if (movementPattern == EnemyController.MovementPattern.Sinfunction)
        {
            gameObject.transform.position = new Vector3(9.5f, Random.Range(-2.5f, 2.5f));
        }
        else if (movementPattern == MovementPattern.Diagonal)
        {
            direction = new Vector3(-0.5f, 0.5f, 0); //�밢���� �� �ʱ⿡ �밢������ �̵��ϵ���
        }
        else if (movementPattern == MovementPattern.UpDown) 
        {
            gameObject.transform.position = new Vector3(8.0f, Random.Range(-2.5f, 2.5f));
        }
    } 
    // UpDownBorder�� �浹 ������
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "UpDownBorder")
        {
            Turn = true;
        }
    }
}