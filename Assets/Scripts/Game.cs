using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _targetPrefad;
    [SerializeField] private GameObject _dropItemPrefab;

    [Header("Properties")] 
    [SerializeField] private float _targetHeight = 2.25f;

    [SerializeField] private float _targetScale = 1.3f;

    private Target _target;
    private DropItem _dropItem;
    private GameObject _level;

    private int _itemsCount;
    private int _currentLevel;
    private bool _isInitialized;

    public bool IsInitialized => _isInitialized;

    public int ItemsCount => _itemsCount;

    public void InitializeLevel()
    {      
        _level = Instantiate(new GameObject(), transform);
        _level.name = $"Level {_currentLevel}";

        _target = Instantiate(_targetPrefad, _level.transform).GetComponent<Target>();
        _target.transform.localScale = Vector3.one * _targetScale;
        _target.transform.localPosition = new Vector3(0, _targetHeight);
        
        var rotDirection = Random.Range(0, 2) == 0 ? true : false;
        var rotSpeed = Random.Range(50, 300);
        _target.Initialize(rotDirection, rotSpeed);

        _itemsCount = Random.Range(5, 10);

        _isInitialized = true;
    }

    public void NextLevel()
    {
        _currentLevel++;
        InitializeLevel();
    }

    public void DisableLevel()
    {
        Destroy(_level.gameObject);
        _isInitialized = false;
    }
}
