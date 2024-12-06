using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum MovementPattern
    {
        Straight,        // 직선 이동
        UpDown,          // 위 아래 반복
        Sinfunction,      // 사인 이동
        Diagonal         // 대각선 이동
    }

    public EnemyData enemyData;               // 적의 데이터
    public MovementPattern movementPattern;    // 적의 이동 패턴
    private Vector3 direction;                 // 적의 이동 방향
    bool Turn=false;                          //UpDown 패턴과 대각선 패턴에서 콜라이더가 닿았는지 확인
    private bool isMovingUp = true;           // UpDown 패턴에서 위로 이동 중인지 여부

    private void OnEnable()
    {
        SetTransform();
    }

    void Update()
    {
        // 패턴에 맞는 이동 처리
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
        // UpDownBorder와 충돌 했으면 실행
        if (Turn)
        {
            isMovingUp = !isMovingUp; // 방향 반전
            Turn = false;
        }
        direction = isMovingUp ? Vector3.left : Vector3.right;
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }
    private void MoveSinfunction()
    {
        float xOffset = Mathf.Sin(Time.time * enemyData.SpeedValue); // 시간에 따라 X축으로 이동
        direction = new Vector3(xOffset, 1.5f, 0); // Y축과 함께 X축 이동
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }

    private void MoveDiagonal()
    {
        // UpDownBorder와 충돌 했으면 실행
        if (Turn)
        {
            // 대각선 방향 반전
            direction = new Vector3(-direction.x, 0.5f, 0);
            Turn = false;
        }
        transform.Translate(direction * enemyData.SpeedValue * Time.deltaTime);
    }
    public void SetTransform()
    {
        // 초기 방향 설정
        direction = Vector3.up;
        gameObject.transform.position = new Vector3(9.5f, Random.Range(-4.3f, 4.3f));
        if (movementPattern == EnemyController.MovementPattern.Sinfunction)
        {
            gameObject.transform.position = new Vector3(9.5f, Random.Range(-2.5f, 2.5f));
        }
        else if (movementPattern == MovementPattern.Diagonal)
        {
            direction = new Vector3(-0.5f, 0.5f, 0); //대각선일 때 초기에 대각선으로 이동하도록
        }
        else if (movementPattern == MovementPattern.UpDown) 
        {
            gameObject.transform.position = new Vector3(8.0f, Random.Range(-2.5f, 2.5f));
        }
    } 
    // UpDownBorder와 충돌 햇을때
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "UpDownBorder")
        {
            Turn = true;
        }
    }
}