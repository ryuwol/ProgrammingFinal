using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject Player;
    void Start()
    {
        Player = Instantiate(charPrefabs[(int)SelcetManager.Instance.currentCharactor]);
        Player.transform.position = transform.position;
    }
}
