using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectCharactor : MonoBehaviour
{
    Color OrignalColor;
    Color SelctColor;
    public Charactor Charactor;
    SpriteRenderer spriteRenderer;
    public SelectCharactor[] chars;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        OrignalColor = spriteRenderer.color;
        SelctColor = spriteRenderer.color;
        SelctColor.a = 1f;
        if (DataManager.Instance.currentCharactor == Charactor)
            OnSelect();
        else
            Deselect();
    }
    private void OnMouseUpAsButton()
    {
        DataManager.Instance.currentCharactor = Charactor;
        OnSelect();
        for (int i = 0; chars.Length > i; i++)
        {
            if (chars[i] != this)
                chars[i].Deselect();
        }
    }
    private void OnSelect()
    {
        spriteRenderer.color = SelctColor;
    }
    private void Deselect()
    {
        spriteRenderer.color = OrignalColor;
    }
}