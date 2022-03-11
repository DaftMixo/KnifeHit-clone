using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _itemsCount;
    private int _currentLevel;
    private bool _isInitialized = false;

    public bool IsInitialized => _isInitialized;

    public int ItemsCount => _itemsCount;

    public void InitializeLevel()
    {
        
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
        _isInitialized = false;
    }
}
