using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class IngameUIManager : MonoBehaviour
{
    private GameObject _saveData;
    private IngameMusicManager _ingameMusic;
    private GameManager _GM;
    private GameObject _configWindow;
    private GameObject _endScene;
    private GameObject _endSceneLayout;
    private Ingame_TextEffect_Manager _textEffect;
    private IngameMusicManager _BGM;
    private IngameSFXManager _SFX;
    private SFXManager _MenuSFX;
    private Slider _BGM_Slider;
    private Slider _SFX_Slider;
    private Slider _Score_Indicator;
    private Text _SongInfo;
        
    private Slider _progress;
    private Image _blackScreen;
    private Toggle _toggleTextEffect;
    private Dropdown _timeDropDown;
    private GameObject ButtonCheckImage;
    private Sprite _sprite;

    private bool isEnd = false;
    private bool isConfigOn = false;
    private bool textEffectTrigger = true;
    private float temp;
    private bool btnTriggerOn;
    private int hitCount = 0;
    private int dodgeCount = 0;
    private int missCount = 0;
    private int badCount = 0;
    private string resText = null;

    private ResultState resultState;
    private string bossStatus;
    private float score;
    private float clearPercentage;
    private bool isEnableBackground;
    private ButtonSelected _selectedButton = ButtonSelected.NULL;

    public SceneList ActiveScene = SceneList.NULL;
    public float EndSceneOpenTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (ActiveScene == SceneList.NULL)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Stage_StoneAge" :
                    Initialize_StoneAge();
                    ActiveScene = SceneList.StoneAge;
                    break;
                case "Stage_MiddleAge" :
                    Initialize_MiddleAge();
                    ActiveScene = SceneList.MiddleAge;
                    break;
                case "Stage_ModernAge" :
                    Initialize_ModernAge();
                    ActiveScene = SceneList.ModernAge;
                    break;
                case "Stage_SciFiAge" :
                    Initialize_SciFiAge();
                    ActiveScene = SceneList.SciFi;
                    break;
                default :
                    Debug.Log("UIManger : Unexepceted ActiveScene Name");
                    break;
            }
        }
        
        if (SceneData.Instance.TextEffect != TextEffectEnable.NULL)
        {
            switch (SceneData.Instance.TextEffect)
            {
                case TextEffectEnable.Enable :
                    _toggleTextEffect.isOn = true;
                    break;
                case TextEffectEnable.Disenable :
                    _toggleTextEffect.isOn = false;
                    break;
            }
        }

        hitCount = dodgeCount = missCount = badCount = 0;
        
        MultiResolutionUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            if (_ingameMusic.GetPlayTime() <= _progress.maxValue - 0.1f)
            {
                _progress.value = _ingameMusic.GetPlayTime();
            }
            else
            {
                isEnd = true;
            }
        }
        else
        {
            _progress.value = _progress.maxValue;
        }
    }

    public void Initialize_StoneAge()
    {
        _saveData = GameObject.Find("SavaData");
        _ingameMusic = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _progress = GameObject.Find("ProgressBar").GetComponent<Slider>();
        _blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        _endScene = GameObject.Find("Ending").transform.Find("EndScene").gameObject;
        _endSceneLayout = GameObject.Find("Ending").transform.Find("EndScene").Find("Ending_Layout").gameObject;
        _GM = GameObject.Find("Manager").GetComponent<GameManager>();
        _configWindow = GameObject.Find("Settings").transform.Find("ConfigWindow").gameObject;
        _textEffect = gameObject.GetComponent<Ingame_TextEffect_Manager>();
        _toggleTextEffect = _configWindow.transform.Find("TextEffect_Toggle").gameObject.GetComponent<Toggle>();
        _BGM = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _SFX = GameObject.Find("SFX").GetComponent<IngameSFXManager>();
        _MenuSFX = GameObject.Find("MainMenuSFX").GetComponent<SFXManager>();
        _Score_Indicator = GameObject.Find("Boss_HP_Indicator").GetComponent<Slider>();

        _BGM_Slider = _configWindow.transform.Find("BGM_Slider").GetComponent<Slider>();
        _SFX_Slider = _configWindow.transform.Find("SFX_Slider").GetComponent<Slider>();
        
        _BGM_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetBGMVol();
        _SFX_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetSFXVol();

        _SongInfo = GameObject.Find("Song_Info").GetComponent<Text>();

        _timeDropDown = _configWindow.transform.Find("Time").gameObject.GetComponent<Dropdown>();
        _timeDropDown.value = 3;

        _progress.minValue = 0.0f;
        _progress.maxValue = _ingameMusic.GetAudioLength();
        
        _Score_Indicator.maxValue = 200;
        _Score_Indicator.minValue = 0;
        _Score_Indicator.value = 100;

        if (_configWindow != null)
        {
            ButtonCheckImage = GameObject.Find("Settings").transform.Find("Check_Image").gameObject;
        }
    }
    
    public void Initialize_MiddleAge()
    {
        _saveData = GameObject.Find("SavaData");
        _ingameMusic = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _progress = GameObject.Find("ProgressBar").GetComponent<Slider>();
        _blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        _endScene = GameObject.Find("Ending").transform.Find("EndScene").gameObject;
        _endSceneLayout = GameObject.Find("Ending").transform.Find("EndScene").Find("Ending_Layout").gameObject;
        _GM = GameObject.Find("Manager").GetComponent<GameManager>();
        _configWindow = GameObject.Find("Settings").transform.Find("ConfigWindow").gameObject;
        _textEffect = gameObject.GetComponent<Ingame_TextEffect_Manager>();
        _toggleTextEffect = _configWindow.transform.Find("TextEffect_Toggle").gameObject.GetComponent<Toggle>();
        _BGM = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _SFX = GameObject.Find("SFX").GetComponent<IngameSFXManager>();
        _MenuSFX = GameObject.Find("MainMenuSFX").GetComponent<SFXManager>();
        _Score_Indicator = GameObject.Find("Boss_HP_Indicator").GetComponent<Slider>();

        _BGM_Slider = _configWindow.transform.Find("BGM_Slider").GetComponent<Slider>();
        _SFX_Slider = _configWindow.transform.Find("SFX_Slider").GetComponent<Slider>();
        
        _BGM_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetBGMVol();
        _SFX_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetSFXVol();

        _SongInfo = GameObject.Find("Song_Info").GetComponent<Text>();

        _timeDropDown = _configWindow.transform.Find("Time").gameObject.GetComponent<Dropdown>();
        _timeDropDown.value = 3;

        _progress.minValue = 0.0f;
        _progress.maxValue = _ingameMusic.GetAudioLength();
        
        _Score_Indicator.maxValue = 200;
        _Score_Indicator.minValue = 0;
        _Score_Indicator.value = 100;

        if (_configWindow != null)
        {
            ButtonCheckImage = GameObject.Find("Settings").transform.Find("Check_Image").gameObject;
        }
    }
    
    public void Initialize_ModernAge()
    {
        _saveData = GameObject.Find("SavaData");
        _ingameMusic = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _progress = GameObject.Find("ProgressBar").GetComponent<Slider>();
        _blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        _endScene = GameObject.Find("Ending").transform.Find("EndScene").gameObject;
        _endSceneLayout = GameObject.Find("Ending").transform.Find("EndScene").Find("Ending_Layout").gameObject;
        _GM = GameObject.Find("Manager").GetComponent<GameManager>();
        _configWindow = GameObject.Find("Settings").transform.Find("ConfigWindow").gameObject;
        _textEffect = gameObject.GetComponent<Ingame_TextEffect_Manager>();
        _toggleTextEffect = _configWindow.transform.Find("TextEffect_Toggle").gameObject.GetComponent<Toggle>();
        _BGM = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _SFX = GameObject.Find("SFX").GetComponent<IngameSFXManager>();
        _MenuSFX = GameObject.Find("MainMenuSFX").GetComponent<SFXManager>();
        _Score_Indicator = GameObject.Find("Boss_HP_Indicator").GetComponent<Slider>();

        _BGM_Slider = _configWindow.transform.Find("BGM_Slider").GetComponent<Slider>();
        _SFX_Slider = _configWindow.transform.Find("SFX_Slider").GetComponent<Slider>();
        
        _BGM_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetBGMVol();
        _SFX_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetSFXVol();

        _SongInfo = GameObject.Find("Song_Info").GetComponent<Text>();

        _timeDropDown = _configWindow.transform.Find("Time").gameObject.GetComponent<Dropdown>();
        _timeDropDown.value = 3;

        _progress.minValue = 0.0f;
        _progress.maxValue = _ingameMusic.GetAudioLength();
        
        _Score_Indicator.maxValue = 200;
        _Score_Indicator.minValue = 0;
        _Score_Indicator.value = 100;

        if (_configWindow != null)
        {
            ButtonCheckImage = GameObject.Find("Settings").transform.Find("Check_Image").gameObject;
        }
    }
    
    public void Initialize_SciFiAge()
    {
        _saveData = GameObject.Find("SavaData");
        _ingameMusic = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _progress = GameObject.Find("ProgressBar").GetComponent<Slider>();
        _blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        _endScene = GameObject.Find("Ending").transform.Find("EndScene").gameObject;
        _endSceneLayout = GameObject.Find("Ending").transform.Find("EndScene").Find("Ending_Layout").gameObject;
        _GM = GameObject.Find("Manager").GetComponent<GameManager>();
        _configWindow = GameObject.Find("Settings").transform.Find("ConfigWindow").gameObject;
        _textEffect = gameObject.GetComponent<Ingame_TextEffect_Manager>();
        _toggleTextEffect = _configWindow.transform.Find("TextEffect_Toggle").gameObject.GetComponent<Toggle>();
        _BGM = GameObject.Find("BGM").GetComponent<IngameMusicManager>();
        _SFX = GameObject.Find("SFX").GetComponent<IngameSFXManager>();
        _MenuSFX = GameObject.Find("MainMenuSFX").GetComponent<SFXManager>();
        _Score_Indicator = GameObject.Find("Boss_HP_Indicator").GetComponent<Slider>();

        _BGM_Slider = _configWindow.transform.Find("BGM_Slider").GetComponent<Slider>();
        _SFX_Slider = _configWindow.transform.Find("SFX_Slider").GetComponent<Slider>();
        
        _BGM_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetBGMVol();
        _SFX_Slider.value = GameObject.Find("MainMenuMusic").GetComponent<MusicManager>().GetSFXVol();

        _SongInfo = GameObject.Find("Song_Info").GetComponent<Text>();

        _timeDropDown = _configWindow.transform.Find("Time").gameObject.GetComponent<Dropdown>();
        _timeDropDown.value = 3;

        _progress.minValue = 0.0f;
        _progress.maxValue = _ingameMusic.GetAudioLength();
        
        _Score_Indicator.maxValue = 200;
        _Score_Indicator.minValue = 0;
        _Score_Indicator.value = 100;

        if (_configWindow != null)
        {
            ButtonCheckImage = GameObject.Find("Settings").transform.Find("Check_Image").gameObject;
        }
    }

    public void MultiResolutionUI()
    {
        if (Mathf.Approximately(Camera.main.aspect, 1.777777f))
        {
            _endScene.transform.Find("Restart").transform.localPosition = new Vector3(700f, -440f, 0);
        } else if (Mathf.Approximately(Camera.main.aspect, 1.6f))
        {
            _endScene.transform.Find("Restart").transform.localPosition = new Vector3(635f, -440f, 0);
            _endScene.transform.Find("ToMainMenu").transform.localPosition = new Vector3(374, -440f, 0);
        }
    }

    public void EnableEndScene()
    {
        string temp = null;
        _blackScreen.enabled = true;
        _endScene.SetActive(true);
        if (Mathf.Approximately(Camera.main.aspect, 2.388888f))
        {
            _endScene.transform.Find("Text").GetComponent<RectTransform>().offsetMin = new Vector2(125f, 384f);
            _endScene.transform.Find("Text").GetComponent<RectTransform>().offsetMax = new Vector2(-1477f, -44f);
        } else if (Mathf.Approximately(Camera.main.aspect, 2.111111f))
        {
        } else if (Mathf.Approximately(Camera.main.aspect, 1.777777f))
        {
            _endScene.transform.Find("Text").GetComponent<RectTransform>().offsetMin = new Vector2(230f, 384f);
            _endScene.transform.Find("Text").GetComponent<RectTransform>().offsetMax = new Vector2(-1359f, -44f);
        } else if (Mathf.Approximately(Camera.main.aspect, 1.6f))
        {
            _endScene.transform.Find("Text").GetComponent<RectTransform>().offsetMin = new Vector2(198f, 378f);
            _endScene.transform.Find("Text").GetComponent<RectTransform>().offsetMax = new Vector2(-1210f, -50f);
        }

        switch (ActiveScene)
        {
            case SceneList.StoneAge :
                switch (resultState)
                {
                    case ResultState.BossDead :
                        temp = "TrgBossDie";
                        _sprite = Resources.Load<Sprite>("Stage/StoneAge/mammoth_hunting-Boss_dead");
                        _endScene.transform.Find("Ending_IMG").GetComponent<Image>().transform.localScale = new Vector3(0.65f, 0.8f, 1.0f);
                        break;
                    case ResultState.BossGroggy :
                        temp = "TrgBossGroggy";
                        _sprite = Resources.Load<Sprite>("Stage/StoneAge/mammoth_hunting-Boss_groggy");
                        break;
                    case ResultState.BossRun :
                        temp = "TrgBossRun";
                        _sprite = Resources.Load<Sprite>("Stage/StoneAge/mammoth_hunting-Boss_Run");
                        break;
                    case ResultState.PlayerRun :
                        temp = "TrgPlayerRun";
                        _sprite = Resources.Load<Sprite>("Stage/StoneAge/mammoth_hunting-Player_run");
                        break;
                    case ResultState.PlayerFail :
                        temp = "TrgPlayerFail";
                        _sprite = Resources.Load<Sprite>("Stage/StoneAge/mammoth_hunting-Player_dead");
                        _endScene.transform.Find("Ending_IMG").GetComponent<Image>().transform.localScale = new Vector3(0.72f, 0.6f, 1.0f);
                        break;
                }
                break;
            case SceneList.MiddleAge :
                break;
            case SceneList.ModernAge :
                break;
            case SceneList.SciFi :
                break;
        }
        Debug.Log(temp);
        Debug.Log(_sprite);
        // 추후 이미지 추가될시 반드시 변경할 것
        _endScene.transform.Find("Buttons").GetComponent<Animator>().SetTrigger(temp);
        _endScene.transform.Find("Ending_IMG").GetComponent<Image>().sprite = _sprite;

        // ResultState 상태에 따라 이미지 변경
        GameObject.Find("Result_Text").GetComponent<Text>().text = resText;
        GameObject.Find("Hit_Count").GetComponent<Text>().text = hitCount.ToString();
        GameObject.Find("Nice_Count").GetComponent<Text>().text = dodgeCount.ToString();
        GameObject.Find("Miss_Count").GetComponent<Text>().text = missCount.ToString();
        GameObject.Find("Bad_Count").GetComponent<Text>().text = badCount.ToString();
    }

    public void EnableConfigWindow()
    {
        _MenuSFX.PlayLoadingSFX();
        if (!isConfigOn)
        {
            _blackScreen.enabled = true;
            _configWindow.SetActive(true);
            _ingameMusic.PauseBGM();
            _GM.GamePause();
        }
        else
        {
            ButtonCheckImage.SetActive(false);
            _blackScreen.enabled = false;
            _configWindow.SetActive(false);
            _ingameMusic.UnPauseBGM();
            _GM.GameUnPause();
        }

        _selectedButton = ButtonSelected.NULL;
        _textEffect.ToggleStatusTextEffect();
        isConfigOn = !isConfigOn;
    }
    
    public void RestartLevel()
    {
        if (!btnTriggerOn)
        {
            if (_selectedButton == ButtonSelected.OnClickRestart)
            {
                Debug.Log("Restart");
                
                // 디버깅용 시간 초기화
                _GM.TimeScale = 1f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 1f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 1f);
                
                _GM.NextScene = SceneList.StoneAge;
                _GM.EnableLoadingScreen = false;
                _GM.MoveNextScene();
                btnTriggerOn = true;
                _MenuSFX.PlayLoadingSFX();
            } 
            else
            {
                _selectedButton = ButtonSelected.OnClickRestart;
                ButtonCheckImage.transform.position = GameObject.Find("Restart").transform.position;
                ButtonCheckImage.SetActive(true);
                _MenuSFX.PlayButtonClickSFX();
            }
        }
    }

    public void RetrunToMainMenu()
    {
        if (!btnTriggerOn)
        {
            if (_selectedButton == ButtonSelected.OnClickMainMenu)
            {
                Debug.Log("Restart");
                
                // 디버깅용 시간 초기화
                _GM.TimeScale = 1f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 1f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 1f);
                
                _GM.NextScene = SceneList.Main_Scene;
                _GM.EnableLoadingScreen = false;
                _GM.MoveNextScene();
                btnTriggerOn = true;
                _MenuSFX.PlayLoadingSFX();
            } 
            else
            {
                _selectedButton = ButtonSelected.OnClickMainMenu;
                ButtonCheckImage.transform.position = GameObject.Find("ToMainMenu").transform.position;
                ButtonCheckImage.SetActive(true);
                _MenuSFX.PlayButtonClickSFX();
            }
        }
    }

    public void RestartLevelEnd()
    {
        if (!btnTriggerOn)
        {
            if (_selectedButton == ButtonSelected.OnClickRestart)
            {
                Debug.Log("Restart");
                
                // 디버깅용 시간 초기화
                _GM.TimeScale = 1f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 1f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 1f);
                
                _GM.NextScene = SceneList.StoneAge;
                _GM.EnableLoadingScreen = false;
                _GM.MoveNextScene();
                btnTriggerOn = true;
                _MenuSFX.PlayLoadingSFX();
            } 
            else
            {
                _selectedButton = ButtonSelected.OnClickRestart;
                // ButtonCheckImage.transform.position = GameObject.Find("Restart").transform.position;
                Transform tf = GameObject.Find("Restart").transform;
                ButtonCheckImage.transform.localPosition = tf.localPosition;
                if (Mathf.Approximately(Camera.main.aspect, 2.111111f))
                {
                    ButtonCheckImage.transform.localPosition = new Vector3(tf.localPosition.x, -440f, tf.localPosition.z);
                } else if (Mathf.Approximately(Camera.main.aspect, 1.777777f))
                {
                    ButtonCheckImage.transform.localPosition = new Vector3(700f, -440f, tf.localPosition.z);
                    _endScene.transform.Find("Restart").transform.localPosition = new Vector3(700f, -440f, tf.localPosition.z);
                } else if (Mathf.Approximately(Camera.main.aspect, 1.6f))
                {
                    ButtonCheckImage.transform.localPosition = new Vector3(635f, -440f, tf.localPosition.z);
                    _endScene.transform.Find("Restart").transform.localPosition = new Vector3(635f, -440f, tf.localPosition.z);
                }
                ButtonCheckImage.SetActive(true);
                _MenuSFX.PlayButtonClickSFX();
            }
        }
    }

    public void RetrunToMainMenuEnd()
    {
        if (!btnTriggerOn)
        {
            if (_selectedButton == ButtonSelected.OnClickMainMenu)
            {
                Debug.Log("Restart");
                
                // 디버깅용 시간 초기화
                _GM.TimeScale = 1f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 1f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 1f);
                
                _GM.NextScene = SceneList.Main_Scene;
                _GM.EnableLoadingScreen = false;
                _GM.MoveNextScene();
                btnTriggerOn = true;
                _MenuSFX.PlayLoadingSFX();
            } 
            else
            {
                _selectedButton = ButtonSelected.OnClickMainMenu;
                // ButtonCheckImage.transform.position = GameObject.Find("ToMainMenu").transform.position;
                Transform tf = GameObject.Find("ToMainMenu").transform;
                if (Mathf.Approximately(Camera.main.aspect, 2.111111f))
                {
                    ButtonCheckImage.transform.localPosition = new Vector3(tf.localPosition.x, -440f, tf.localPosition.z);
                } else if (Mathf.Approximately(Camera.main.aspect, 1.777777f))
                {
                    ButtonCheckImage.transform.localPosition = new Vector3(374, -440f, tf.localPosition.z);
                } else if (Mathf.Approximately(Camera.main.aspect, 1.6f))
                {
                    ButtonCheckImage.transform.localPosition = new Vector3(374, -440f, tf.localPosition.z);
                    _endScene.transform.Find("ToMainMenu").transform.localPosition = new Vector3(374, -440f, tf.localPosition.z);
                }
                ButtonCheckImage.SetActive(true);
                _MenuSFX.PlayButtonClickSFX();
            }
        }
    }

    public void PrintTextEffect(TextPrintType type)
    {
        switch (type)
        {
            case TextPrintType.Hit :
                _textEffect.PrintHitEffect();
                break;
            case TextPrintType.Dodge :
                _textEffect.PrintDodgeEffect();
                break;
            case TextPrintType.Miss :
                _textEffect.PrintMissEffect();
                break;
            case TextPrintType.Damaged :
                _textEffect.PrintDamagedEffect();
                break;
            default :
                break;
        }
    }

    public void OnToggleTextEffect()
    {
        textEffectTrigger = _toggleTextEffect.isOn;
        // if (!textEffectTrigger)
        // {
        //     _textEffect.ResetPosition();
        // }
        _GM.ToggleTextEffect(textEffectTrigger);
        switch (textEffectTrigger)
        {
            case true :
                _textEffect.TextEffect = TextEffectEnable.Enable;
                break;
            case false :
                _textEffect.TextEffect = TextEffectEnable.Disenable;
                break;
        }
    }
    
    public void OnBGMVolSlider()
    {
        _BGM.VolChangeBGM(_configWindow.transform.Find("BGM_Slider").GetComponent<Slider>().value);
    }

    public void OnSFXVolSlider()
    {
        _SFX.VolChangeSFX(_configWindow.transform.Find("SFX_Slider").GetComponent<Slider>().value);
    }

    public void GetGameResult(ResultState state, string bossStatus, float score, float clearPercentage, int[] stats)
    {
        switch (state)
        {
            case ResultState.BossDead :
                resText = "Perfect\n Hunting";
                break;
            case ResultState.BossGroggy :
                resText = "Hunting\n Sucess";
                break;
            case ResultState.BossRun :
                resText = "Clear";
                break;
            case ResultState.PlayerRun :
                resText = "Run Away";
                break;
            case ResultState.PlayerFail :
                resText = "Failed";
                break;
        }

        this.resultState = state;
        this.bossStatus = bossStatus;
        this.score = score;
        this.clearPercentage = clearPercentage;

        hitCount = stats[0];
        dodgeCount = stats[1];
        missCount = stats[2];
        badCount = stats[3];
    }

    public void OnBossHPChageListener()
    {
        _Score_Indicator.value = _GM.score;
    }

    public void OnToggleBackgroundImg()
    {
        isEnableBackground = GameObject.Find("Background_Toggle").GetComponent<Toggle>().isOn;
        GameObject.Find("BaseCanvas").GetComponent<Image>().enabled = isEnableBackground;
    }

    public void SetSongInfo(string str)
    {
        _SongInfo.text = str;
    }

    public void OnChangeTimeListener()
    {
        _timeDropDown.onValueChanged.AddListener(delegate { DropDownValueChanged(_timeDropDown); });
    }

    void DropDownValueChanged(Dropdown change)
    {
        float time = 1f;
        
        switch (change.value)
        {
            case 0 :
                _GM.TimeScale = 0f;
                time = 0f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 0f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 0f);
                break;
            case 1 :
                _GM.TimeScale = 0.25f;
                time = 0.25f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 0.25f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 0.25f);
                break;
            case 2 :
                _GM.TimeScale = 0.5f;
                time = 0.5f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 0.5f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 0.5f);
                break;
            case 3 :
                _GM.TimeScale = 1f;
                time = 1f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 1f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 1f);
                break;
            case 4 :
                _GM.TimeScale = 2f;
                time = 2f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 2f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 2f);
                break;
            case 5 :
                _GM.TimeScale = 3f;
                time = 3f;
                _ingameMusic.AudioMixer.SetFloat("BGM_Speed", 3f);
                _SFX.AudioMixer.SetFloat("SFX_Speed", 3f);
                break;
        }

        Time.timeScale = time;
    }
}
