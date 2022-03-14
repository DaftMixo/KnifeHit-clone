using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    [SerializeField] private UIController _ui;

    private GameData _data;
    private Settings _settings;
    private AudioFX _audioFx;
    private Game _game;

    private int _maxScore;

    public GameData Data => _data;

    private void Awake()
    {
        if (Inst != null)
            Destroy(this.gameObject);
        Inst = this;

        _data = SaveManager.LoadData();
        _maxScore = _data.Score;
        Debug.Log(_data.ToString());

        _settings = GetComponent<Settings>();
        _audioFx = GetComponent<AudioFX>();
        _game = GetComponent<Game>();

        _game.InitData(_data);
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
        _game.OnHit.AddListener(OnHit);
        UpdateData();
    }

    private void OnHit()
    {
        _audioFx.PlayHitSound();
    }

    private void OnStageComplete()
    {
        _ui.ResetGamePanel(_game.ItemsCount);
    }

    private void OnGameFail()
    {
        _game.DisableLevel();
        if (_data.Score < _maxScore)
            _data.Score = _maxScore;
        _ui.SetPanel(GamePanelState.Fail);

    }

    private void OnTakeMoney()
    {
        _data.Money++;
    }

    private void UpdateData()
    {
        if (_game.IsInitialized)
        {
            _data = _game.Data;
        }

        _ui.UpdateGamePanel(_game.Data, _game.ItemsCount);
        _ui.UpdateFailPanel(_game.Data);
        _ui.UpdatePanels(_data);

        SaveManager.SaveData(_data);
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
            if (_data.Score < _maxScore)
                _data.Score = _maxScore;
            SaveManager.SaveData(_data);
        }
    }

    private void OnDestroy()
    {
        _game.OnHit.RemoveAllListeners();
        _game.OnTakeMoney.RemoveAllListeners();
        _game.OnGameFail.RemoveAllListeners();
        _game.OnStageComplete.RemoveAllListeners();
        _game.OnDataUpdate.RemoveAllListeners();
        _settings.OnSettingsUpdate.RemoveAllListeners();
    }
}
