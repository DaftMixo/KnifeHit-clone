using UnityEngine;
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
        _gameController.InitializeLevel();
        _ui.ResetGamePanel(_gameController.ItemsCount);
    }

    public void Home()
    {
        _ui.SetPanel(GamePanelState.Menu);
        _audioFx.PlayButtonSound();
    }

    public void RestartGame()
    {
        StartGame();
        _audioFx.PlayButtonSound();
    }
}
