using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _fail;

    private GamePanel _activePanel = GamePanel.Unknown;

    public GamePanel ActivePanel => _activePanel;

    private void Awake()
    {
        _menu.SetActive(false);
        _game.SetActive(false);
        _fail.SetActive(false);
    }

    public void SetPanel(GamePanel panel)
    {
        _menu.SetActive(false);
        _game.SetActive(false);
        _fail.SetActive(false);
        
        switch (panel)
        {
            case GamePanel.Menu :
                _menu.SetActive(true);
                _activePanel = GamePanel.Menu;
                break;
            case GamePanel.Game :
                _game.SetActive(true);
                _activePanel = GamePanel.Game;
                break;
            case GamePanel.Fail :
                _fail.SetActive(true);
                _activePanel = GamePanel.Fail;
                break;
            default:
                _activePanel = GamePanel.Unknown;
                break;
        }
    }
}

public enum GamePanel
{
    Unknown,
    Menu,
    Game,
    Fail
}
