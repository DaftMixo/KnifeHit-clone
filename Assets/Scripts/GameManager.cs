using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] private UIController _ui;

    private GameData _data;
    private Settings _settings;
    private AudioFX _audioFx;
    private Game _game;

    private void Awake()
    {
        _data = SaveManager.LoadData();
        Debug.Log(_data.ToString());

        _settings = GetComponent<Settings>();
        _audioFx = GetComponent<AudioFX>();
        _game = GetComponent<Game>();
        
        _settings.Initialize(_data);
        _settings.OnSettingsUpdate.AddListener(SaveSettings);
    }

    private void Start()
    {
        _ui.SetPanel(GamePanelState.Menu);
        _game.OnDataUpdate.AddListener(UpdateData);
        _game.OnStageComplete.AddListener(OnStageComplete);
        _game.OnGameFail.AddListener(OnGameFail);
        _game.OnTakeMoney.AddListener(OnTakeMoney);
        UpdateData();
    }

    private void OnStageComplete()
    {
        _ui.ResetGamePanel(_game.ItemsCount);
    }

    private void OnGameFail(GameData data)
    {
        if (_data.Score < data.Score)
            _data.Score = data.Score;
        _game.DisableLevel();
        _ui.SetPanel(GamePanelState.Fail);
    }

    private void OnTakeMoney()
    {
        _data.Money++;
    }

    private void UpdateData()
    {
        _data.Stage = _game.Data.Stage;

        SaveManager.SaveData(_data);
        _ui.UpdateGamePanel(_data, _game.Data.Score, _game.ItemsCount);
    }
    
    private void SaveSettings(GameData.SettingsData data)
    {
        _audioFx.SetSound(data.Sound);
        _audioFx.SetMusic(data.Music);
        
        _data.Settings.Sound = data.Sound;
        _data.Settings.Music = data.Music;
        _data.Settings.Vibration = data.Vibration;
        SaveManager.SaveData(_data);
    }
    
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveManager.SaveData(_data);
        }
    }

    private void OnDestroy()
    {
        _game.OnTakeMoney.RemoveAllListeners();
        _game.OnGameFail.RemoveAllListeners();
        _game.OnStageComplete.RemoveAllListeners();
        _game.OnDataUpdate.RemoveAllListeners();
        _settings.OnSettingsUpdate.RemoveAllListeners();
    }
}
