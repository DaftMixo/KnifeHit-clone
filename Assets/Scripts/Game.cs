using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _targetPrefad;
    [SerializeField] private GameObject _dropItemPrefab;

    [Header("Properties")] 
    [SerializeField] private float _targetHeight = 2.25f;
    [SerializeField] private float _dropItemYPosition = -1f;
    [SerializeField] private float _gameObjectScale = 1.3f;

    public UnityEvent OnDataUpdate;
    public UnityEvent OnGameFail;
    public UnityEvent OnStageComplete;
    public UnityEvent OnTakeMoney;
    public UnityEvent OnHit;

    private Target _target;
    private DropItem _dropItem;
    private GameObject _level;
    private GameData _gameData;
    private List<DropItem> _dropedItems = new List<DropItem>();

    private int _itemsCount;
    private int _currentLevel;
    private bool _isInitialized;

    public GameData Data => _gameData;
    public bool IsInitialized => _isInitialized;
    public int ItemsCount => _itemsCount;

    public void InitData(GameData data)
    {
        _gameData = data;
    }
    public void InitializeLevel(GameData data)
    {
        _gameData = data;
        if(_currentLevel == 0)
        {
            _gameData.Score = 0;
            _gameData.Stage = 0;
        }

        _level = new GameObject($"Level {_currentLevel}");
        _level.transform.parent = transform;

        _target = Instantiate(_targetPrefad, _level.transform).GetComponent<Target>();
        _target.transform.localScale = Vector3.one * _gameObjectScale;
        _target.transform.localPosition = new Vector3(0, _targetHeight);
        
        var rotDirection = Random.Range(0, 2) == 0 ? true : false;
        var rotSpeed = Random.Range(50, 250);
        _target.Initialize(rotDirection, rotSpeed, _gameObjectScale);

        StartCoroutine(InitDropItem(0));

        _itemsCount = Random.Range(5, 10);

        _isInitialized = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _dropItem != null)
        {
            _dropItem.Push();
            if (_itemsCount > 0)
            { 
                StartCoroutine(InitDropItem(.2f));
            }
            else
                _dropItem = null;
        }
    }

    private IEnumerator InitDropItem(float delay)
    {
        _dropItem = null;
        yield return new WaitForSeconds(delay);
        _dropItem = Instantiate(_dropItemPrefab, _level.transform).GetComponent<DropItem>();
        _dropItem.transform.localScale = Vector3.one * _gameObjectScale;
        _dropItem.transform.localPosition = new Vector3(0, _dropItemYPosition);
        _dropedItems.Add(_dropItem);
        _dropItem.OnHit.AddListener(OnItemHit);
        _dropItem.OnDrop.AddListener(OnItemDrop);
    }

    private void OnItemDrop(DropItem item)
    {
        _itemsCount--;
        OnDataUpdate?.Invoke();
        item.OnDrop.RemoveListener(OnItemDrop);
    }

    private void OnItemHit(HitPoint hit)
    {
        switch (hit)
        {
            case HitPoint.Target:
                if (GameManager.Inst.Data.Settings.Vibration) Vibration.Vibrate(30);
                _gameData.Score += 1 + _gameData.Stage;
                OnHit?.Invoke();
                if (_itemsCount <= 0)
                {
                    _gameData.Stage++;
                    StartCoroutine(NextLevel());
                }
                OnDataUpdate?.Invoke();
                break;
            case HitPoint.Money:
                OnTakeMoney?.Invoke();
                OnDataUpdate?.Invoke();
                break;
            case HitPoint.DropItem:
                if (GameManager.Inst.Data.Settings.Vibration) Vibration.Vibrate(30);
                OnDataUpdate?.Invoke();
                OnGameFail?.Invoke();
                break;
        }

    }

    private IEnumerator NextLevel()
    {
        _target?.Destroy();
        yield return new WaitForSeconds(1);
        DisableLevel();
        _currentLevel++;
        InitializeLevel(_gameData);

        OnStageComplete?.Invoke();
    }

    public void DisableLevel()
    {
        foreach (var dropItem in _dropedItems)
        {
            dropItem.OnHit.RemoveAllListeners();
        }
        _level.SetActive(false);
        Destroy(_level.gameObject, .1f);
        _dropItem = null;
        _currentLevel = 0;
        _isInitialized = false;
        OnDataUpdate?.Invoke();
    }
}
