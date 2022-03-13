using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _fail;
    [SerializeField] private GameObject _settings;
    [Space] 

    private MenuPanel _menuPanel;
    private GamePanel _gamePanel;
    private FailPanel _failPanel;

    private GamePanelState _activePanel = GamePanelState.Unknown;

    public GamePanelState ActivePanel => _activePanel;

    private void Awake()
    {
        _menu.SetActive(false);
        _game.SetActive(false);
        _fail.SetActive(false);
        _settings.SetActive(false);

        _menuPanel = _menu.GetComponent<MenuPanel>();
        _gamePanel = _game.GetComponent<GamePanel>();
        _failPanel = _fail.GetComponent<FailPanel>();
    }

    public void SetPanel(GamePanelState panel)
    {
        _menu.SetActive(false);
        _game.SetActive(false);
        _fail.SetActive(false);
        _settings.SetActive(false);
        
        switch (panel)
        {
            case GamePanelState.Menu :
                _menu.SetActive(true);
                _activePanel = GamePanelState.Menu;
                break;
            case GamePanelState.Game :
                _game.SetActive(true);
                _activePanel = GamePanelState.Game;
                break;
            case GamePanelState.Fail :
                _fail.SetActive(true);
                _activePanel = GamePanelState.Fail;
                break;
            case GamePanelState.Settings :
                _settings.SetActive(true);
                _activePanel = GamePanelState.Settings;
                break;
            default:
                _activePanel = GamePanelState.Unknown;
                break;
        }
    }

    public void ResetGamePanel(int items)
    {
        _gamePanel.ResetPanel(items);
    }
    
    public void UpdateGamePanel(GameData data, int score, int itemsCount)
    {
        GameData d = data;
        d.Score = score;
        _gamePanel.UpdatePanel(d, itemsCount);
    }

    public void UpdatePanels(GameData data)
    {
        _menuPanel.UpdatePanel(data);
        _failPanel.UpdatePanel(data);
    }
}

public enum GamePanelState
{
    Unknown,
    Menu,
    Game,
    Fail,
    Settings
}
