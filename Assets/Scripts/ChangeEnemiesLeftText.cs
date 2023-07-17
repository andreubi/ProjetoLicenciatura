using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEnemiesLeftText : MonoBehaviour
{
    [SerializeField] private Text text;
    public void TextChange(int count)
    {
        text.text = $"Enemies Left: {count}";
    }
}
