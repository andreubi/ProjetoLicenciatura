using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWaveText : MonoBehaviour
{
    [SerializeField] private Text text;
    public void TextChange(int wave)
    {
        text.text = $"Wave: {wave}";
    }
}
