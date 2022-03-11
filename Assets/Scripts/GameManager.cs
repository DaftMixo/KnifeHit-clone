using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] private UIController _ui;

    private GameData _data;
    private Settings _settings;
    private AudioFX _audioFx;
    private GameController _gameController;

    private void Awake()
    {
        _data = SaveManager.LoadData();
        
        _settings = GetComponent<Settings>();
        _audioFx = GetComponent<AudioFX>();
        _gameController = GetComponent<GameController>();
        
        _settings.Initialize(_data);
        _settings.OnSettingsUpdate.AddListener(SaveSettings);
    }

    private void Start()
    {
        _ui.SetPanel(GamePanelState.Menu);
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
            //OnDataUpdate?.Invoke(_data);
        }
    }

    private void OnDestroy()
    {
        _settings.OnSettingsUpdate.RemoveAllListeners();
    }
}
