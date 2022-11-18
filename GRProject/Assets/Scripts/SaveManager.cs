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
    [Header("스킬레벨")]
    public int skill1Level;
    public int skill2Level;
    [SerializeField]    public Text skill1;
    [SerializeField]    public Text skill2;

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
        else if(instance != this)
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
        //print(path);
        LoadData();
        skill1Level = playData.skill1Lv;
        skill2Level = playData.skill2Lv;
    }

    // Update is called once per frame
    void Update()
    {
        //playData.bgmVolume = soundManager.musicSource.volume;
        //playData.effectVolume =  soundManager.effectSource.volume;
        skill1.text = skill1Level.ToString();
        skill2.text = skill2Level.ToString();
    }
    public void NewGame()
    {
        if (savefile == true)
        {
            newGameAlert.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("AlreadySavedGame", alreadySavedGame);
        }
        else if (savefile == false)
        {
            SceneManager.LoadScene("StartGame");
        }
    }
    public void LoadGame()
    {
        if (savefile == false)
        {
            noSaveData.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("AlertSound", clip);
        }
        else if (savefile == true)
        {
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
        if(skill1Level < 10)
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
}
