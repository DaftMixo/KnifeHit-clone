using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FailPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _stage;

    public void UpdatePanel(GameData data)
    {
        _score.text = $"Score: {data.Score}";
        _money.text = data.Money.ToString();
        _stage.text = $"Stage: {data.Stage}";
    }
}
