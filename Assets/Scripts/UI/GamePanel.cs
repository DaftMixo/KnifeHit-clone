using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
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
}
