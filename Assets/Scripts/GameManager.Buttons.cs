using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public partial class GameManager : MonoBehaviour
{
    public void OpenSettings()
    {
        _ui.SetPanel(GamePanelState.Settings);
        _audioFx.PlayButtonSound();
    }

    public void StartGame()
    {
        _ui.SetPanel(GamePanelState.Game);
        _audioFx.PlayButtonSound();
        StartCoroutine(InitLevel());
    }

    private IEnumerator InitLevel()
    {
        _game.InitializeLevel(_data);
        yield return new WaitForSeconds(.01f);
        _ui.ResetGamePanel(_game.ItemsCount);
        _ui.UpdateGamePanel(_game.Data, _game.ItemsCount);
    }

    public void Home()
    {
        _ui.SetPanel(GamePanelState.Menu);
        _ui.UpdatePanels(_data);
        _audioFx.PlayButtonSound();
    }

    public void RestartGame()
    {
        StartGame();
        _audioFx.PlayButtonSound();
    }
}
