using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Charactor
{
    White, Black, Red, Green, Blue, Yellow
}
public class SelcetManager : MonoBehaviour
{
    public static SelcetManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
        {
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public Charactor currentCharactor;
}
