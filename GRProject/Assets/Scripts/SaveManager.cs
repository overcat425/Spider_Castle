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
    public int coins;
    public int skill1Lv;
    public int skill2Lv;
    public int skill3Lv;
    public bool skill3Enable;
    public bool hideHelpNotice;
}
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SoundManager soundManager;
    public CoinManager coinManager;
    private MainMenu mainMenu;
    public GameObject needCost;
    public GameObject mastered;

    [SerializeField]
    private AudioClip clip;
    PlayerData playData = new PlayerData();
    string path;
    string filename = "savedata";
    public static bool savefile;
    public bool hideNotice;

    [Header("인스턴스")]       // 외부 스크립트 전용 static 수치
    public static float bgmVolumeInstance;
    public static float getBgmInstance;
    public static float effectVolumeInstance;
    public static float getEffectInstance;
    public static int skill1LvInstance;
    public static int skill2LvInstance;
    public static int skill3LvInstance;
    public static bool skill3EnableInstance;
    public static bool getSkill3EnableInstance;
    public static bool hideNoticeInstance;
    public static int coinsInstance;

    public static int getCoinInstance;          // CoinManager에서 가져오는 수치

    [Header("재화")]
    public int earnedCoins;
    [SerializeField]    public Text earnedCoinsCount;
    [SerializeField]    public Text earnedCoinsCountMain;

    [Header("스킬레벨")]
    public int skill1Level;
    public int skill2Level;
    public int skill3Level;
    [SerializeField]    public Text skill1;
    [SerializeField]    public Text skill2;
    [SerializeField]    public Text skill3;
    [SerializeField]    public Text skill1Dmg;
    [SerializeField]    public Text skill2Dmg;
    [SerializeField]    public Text skill3CoolDown;

    [Header("데미지&쿨타임")]
    [SerializeField] public int baseDamage;
    [SerializeField] public int maceDamage;
    [SerializeField] public float jumpCoolDown;

    [Header("효과음")]
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
        skill3Level = playData.skill3Lv;
        earnedCoins = playData.coins;
        hideNotice = playData.hideHelpNotice;
        //HealthGauge.canAutoSave = true;
    }
    public void Update()
    {
        //playData.bgmVolume = soundManager.musicSource.volume;
        //playData.effectVolume =  soundManager.effectSource.volume;
        Instancing();
        Sync();
        //Debug.Log(skill3EnableInstance);
        StartCoroutine("AutoSave");
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(gameObject);
        }
        //Debug.Log("saveFile:" + savefile);
    }
    public void NewGame()
    {
        if (savefile == true)                       // 세이브파일이 있을 때
        {
            newGameAlert.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("AlreadySavedGame", alreadySavedGame);
        }
        else if (savefile == false)                 // 세이브파일 없을 때
        {
            //ForTestCoin();
            SceneManager.LoadScene("StartGame");
        }
    }
    public void LoadGame()
    {
        if (savefile == false)                      // 세이브파일이 없을 때
        {
            noSaveData.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("AlertSound", clip);
        }
        else if (savefile == true)                  // 세이브파일 있을 때
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
        savefile = false;
        //ForTestCoin();
        SceneManager.LoadScene("StartGame");
    }
    public void OnClickNoBtn()
    {
        newGameAlert.SetActive(false);
    }
    public void SaveData()
    {
        SaveVolume();
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
    public IEnumerator AutoSave()
    {
        if ((HealthGauge.health <= 0)||(HealthGauge.health == 369))        // 던전이 끝났을 때 자동저장
        {
            if (HealthGauge.canAutoSave == true)
            {
                Debug.Log("게임오버 자동저장");
                SaveVolume();
                playData.coins += getCoinInstance;
                playData.skill3Enable = getSkill3EnableInstance;
                string data = JsonUtility.ToJson(playData);
                File.WriteAllText(path + filename, data);
                HealthGauge.canAutoSave = false;
            }
        }
        yield return null;
    }
    public void OnClickSkill1LvUp()
    {
        if (earnedCoins >= 10)                  // 코인 제한
        {
            if (skill1Level < 9)                    // 스킬 최대레벨 10(9+1)
            {
                earnedCoins -= 10;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill1Level += 1;
                playData.skill1Lv = skill1Level;
                playData.coins = earnedCoins;
            }
            else if (skill1Level >= 9)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }
        else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }

    public void OnClickSkill2LvUp()
    {
        if (earnedCoins >= 20)
        {
            if (skill2Level < 9)
            {
                earnedCoins -= 20;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill2Level += 1;
                playData.skill2Lv = skill2Level;
                playData.coins = earnedCoins;
            }
            else if (skill2Level >= 9)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }
        else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill3LvUp()
    {
        if (earnedCoins >= 100)
        {
            if (skill3Level < 2)
            {
                earnedCoins -= 100;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill3Level += 1;
                playData.skill3Lv = skill3Level;
                playData.coins = earnedCoins;
            }
            else if (skill3Level >= 2)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }
        else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void Sync()
    {
        skill1.text = (skill1Level + 1).ToString();
        skill2.text = (skill2Level + 1).ToString();
        skill3.text = (skill3Level + 1).ToString();
        baseDamage = (skill1Level + 1) * 30;
        maceDamage = (skill2Level + 1) * 20;
        jumpCoolDown = 6 - (skill3Level + 1);
        skill1Dmg.text = baseDamage.ToString();
        skill2Dmg.text = maceDamage.ToString();
        skill3CoolDown.text = jumpCoolDown.ToString();
        earnedCoinsCount.text = earnedCoins.ToString();
        earnedCoinsCountMain.text = earnedCoins.ToString();
    }
    public void Instancing()
    {
        bgmVolumeInstance = playData.bgmVolume;
        effectVolumeInstance = playData.effectVolume;
        skill1LvInstance = playData.skill1Lv+1;
        skill2LvInstance = playData.skill2Lv+1;
        skill3LvInstance = playData.skill3Lv+1;
        skill3EnableInstance = playData.skill3Enable;
        hideNoticeInstance = playData.hideHelpNotice;
        coinsInstance = playData.coins;
}
    public void SaveVolume()
    {
        playData.bgmVolume = getBgmInstance;
        playData.effectVolume = getEffectInstance;
    }
    public void OnClickHideHelp()
    {
        playData.hideHelpNotice = true;
        hideNotice = true;
        Debug.Log(hideNotice);
    }
    public void CostWarning()
    {
        needCost.SetActive(false);
    }
    public void MasterWarning()
    {
        mastered.SetActive(false);
    }
    public void ForTestCoin()
    {
        playData.coins = 100;
        string data = JsonUtility.ToJson(playData);
        File.WriteAllText(path + filename, data);
    }
}
