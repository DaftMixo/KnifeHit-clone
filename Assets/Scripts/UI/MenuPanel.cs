using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _money;

    public void UpdatePanel(GameData data)
    {
        _score.text = $"Record: {data.Score}";
        _money.text = $"Money: {data.Money}";
    }
}
