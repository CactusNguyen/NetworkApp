using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vote : MonoBehaviour
{
    public Toggle Tick;
    public TMP_Text Text;

    public void Init(string content)
    {
        Text.text = content;
    }
}
