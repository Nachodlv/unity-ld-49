﻿using TMPro;
using UnityEngine;

public class NumberToText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string prefix;

    public void SetText(int number)
    {
        text.text = prefix + number.ToString();
    }
}
