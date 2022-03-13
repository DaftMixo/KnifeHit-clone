using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _stage;
    [Header("Drop item")]
    [SerializeField] private GameObject _dropItemPrefab;
    [SerializeField] private GameObject _itemsHolder;
    
    private List<GameObject> _dropItemsList = new List<GameObject>();
    
    public void ResetPanel(int items)
    {
        foreach (var item in _dropItemsList)
        {
            Destroy(item.gameObject);
        }
        _dropItemsList.Clear();

        for (int i = 0; i < items; i++)
        {
            _dropItemsList.Add(Instantiate(_dropItemPrefab, _itemsHolder.transform));
        }
    }

    public void UpdatePanel(GameData data, int itemsCount)
    {
        _stage.text = $"Stage: {data.Stage}";
        _score.text = $"Score: {data.Score}";
        _money.text = $"Money: {data.Money}";

        foreach (var item in _dropItemsList)
        {
            item.GetComponent<Image>().color = Color.gray;
        }

        for (int i = 0; i < itemsCount; i++)
        {
            _dropItemsList[i].GetComponent<Image>().color = Color.white;
        }
    }
}
