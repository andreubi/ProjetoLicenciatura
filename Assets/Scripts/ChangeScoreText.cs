using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScoreText : MonoBehaviour
{
    [SerializeField] private Text text;
    public void TextChange(int score)
    {
        text.text = $"Score: {score}";
    }
}
