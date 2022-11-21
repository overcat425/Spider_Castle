using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerData
{
    public float bgmVolume;
    public float effectVolume;
    public int skill1Lv;
    public int skill2Lv;
    public int skill3Lv;
    public bool hideHelpNotice;
}
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public static SoundManager soundManager;
    private MainMenu mainMenu;
    [SerializeField]
    private AudioClip clip;
    PlayerData playData = new PlayerData();
    string path;
    string filename = "savedata";
    public bool savefile;
    public bool hideNotice;

    [Header("�ν��Ͻ�")]       // �ܺ� ��ũ��Ʈ ���� static ��ġ
    public static int skill1LvInstance;
    public static int skill2LvInstance;
    public static bool hideNoticeInstance;

    [Header("��ų����")]
    public int skill1Level;
    public int skill2Level;
    [SerializeField]    public Text skill1;
    [SerializeField]    public Text skill2;
    [SerializeField]    public Text skill1Dmg;
    [SerializeField]    public Text skill2Dmg;

    [Header("������")]
    [SerializeField] public int baseDamage;
    [SerializeField] public int maceDamage;

    [Header("ȿ����")]
    [SerializeField]
    private AudioClip lvUpSound;
    [SerializeField]
    private AudioClip maxLvSound;
    [SerializeField]
    private AudioClip alreadySavedGame;
    [SerializeField]
    private AudioClip savedGameAlert;

    public GameObject noSaveData;
    public GameObject newGameAlert;
    public GameObject savedAlert;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        path = Application.persistentDataPath+ "/";

    }
    void Start()
    {
        if (File.Exists(path+filename))
        {
            savefile = true;
        }
        LoadData();
        skill1Level = playData.skill1Lv;
        skill2Level = playData.skill2Lv;
        hideNotice = playData.hideHelpNotice;
        //HealthGauge.canAutoSave = true;
    }
    public void Update()
    {
        //playData.bgmVolume = soundManager.musicSource.volume;
        //playData.effectVolume =  soundManager.effectSource.volume;
        Instancing(); 
        skill1.text = skill1Level.ToString();
        skill2.text = skill2Level.ToString();
        baseDamage = skill1Level * 10;
        maceDamage = skill2Level * 20;
        skill1Dmg.text =baseDamage.ToString();
        skill2Dmg.text = maceDamage.ToString();

        if (HealthGauge.health <= 0)        // ������ ������ �� �ڵ�����
        {
            if (HealthGauge.canAutoSave == true)
            {
                Debug.Log("�����");
                string data = JsonUtility.ToJson(playData);
                File.WriteAllText(path + filename, data);
                HealthGauge.canAutoSave = false;
            }
        }
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(gameObject);
        }
    }
    public void NewGame()
    {
        if (savefile == true)                       // ���̺������� ���� ��
        {
            newGameAlert.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("AlreadySavedGame", alreadySavedGame);
        }
        else if (savefile == false)                 // ���̺����� ���� ��
        {
            SceneManager.LoadScene("StartGame");
        }
    }
    public void LoadGame()
    {
        if (savefile == false)                      // ���̺������� ���� ��
        {
            noSaveData.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("AlertSound", clip);
        }
        else if (savefile == true)                  // ���̺����� ���� ��
        {
            LoadData();
            SceneManager.LoadScene("StartGame");
        }
    }
    public void ExitAlert()
    {
        noSaveData.SetActive(false);
    }
    public void OnClickYesBtn()
    {
        File.Delete(path+filename);
        playData.hideHelpNotice = false;
        SceneManager.LoadScene("StartGame");
    }
    public void OnClickNoBtn()
    {
        newGameAlert.SetActive(false);
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(playData);
        File.WriteAllText(path+filename, data);
        savedAlert.SetActive(true);
        SoundManager.SoundEffect.SoundPlay("savedGameAlert", savedGameAlert);
        print(path);
    }
    public void ExitAlert2()
    {
        savedAlert.SetActive(false);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path+filename);
        playData = JsonUtility.FromJson<PlayerData>(data);
    }
    public void OnClickSkill1LvUp()
    {
        if(skill1Level < 10)                    // ��ų �ִ뷹�� 10
        {
            SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
            skill1Level += 1;
            playData.skill1Lv = skill1Level;
        }else if(skill1Level >=10)
        {
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill2LvUp()
    {
        if (skill2Level < 10)
        {
            SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
            skill2Level += 1;
            playData.skill2Lv = skill2Level;
        }
        else if (skill2Level >= 10)
        {
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void Instancing()
    {
        skill1LvInstance = playData.skill1Lv;
        skill2LvInstance = playData.skill2Lv;
        hideNoticeInstance = playData.hideHelpNotice;
}
    public void OnClickHideHelp()
    {
        playData.hideHelpNotice = true;
        hideNotice = true;
        Debug.Log(hideNotice);
    }
}
