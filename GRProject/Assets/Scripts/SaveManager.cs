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
    public int gene;
    public int skill0Lv;
    public int skill1Lv;
    public int skill2Lv;
    public int skill3Lv;
    public int skill4Lv;
    public int skill5Lv;
    public int skill1LabLv;
    public int skill2LabLv;
    public int skill3LabLv;
    public int skill4LabLv;
    public bool skill3Enable;
    public bool skill4Enable;
    public bool skill5Enable;
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
    public GameObject needCostLab;
    public GameObject masteredLab;

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
    public static int skill0LvInstance;
    public static int skill1LvInstance;
    public static int skill2LvInstance;
    public static int skill3LvInstance;
    public static int skill4LvInstance;
    public static int skill5LvInstance;
    public static int skill1LabLvInstance;
    public static int skill2LabLvInstance;
    public static int skill3LabLvInstance;
    public static int skill4LabLvInstance;
    public static bool skill3EnableInstance;
    public static bool skill4EnableInstance;
    public static bool skill5EnableInstance;
    public static bool getSkill3EnableInstance;
    public static bool getSkill4EnableInstance;
    public static bool getSkill5EnableInstance;
    public static bool hideNoticeInstance;
    public static int coinsInstance;
    public static int geneInstance;

    public static int getCoinInstance;          // CoinManager에서 가져오는 수치
    public static int getGeneInstance;

    [Header("재화")]
    public int earnedCoins;
    [SerializeField]    public Text earnedCoinsCount;
    [SerializeField]    public Text earnedCoinsCountMain;
    public int earnedGene;
    [SerializeField] public Text earnedGeneCount;
    [SerializeField] public Text earnedGeneCountMain;


    [Header("스킬레벨")]
    public int skill0Level;
    public int skill1Level;
    public int skill2Level;
    public int skill3Level;
    public int skill4Level;
    public int skill5Level;
    public int skill1LabLevel;
    public int skill2LabLevel;
    public int skill3LabLevel;
    public int skill4LabLevel;
    [SerializeField]    public Text skill0;
    [SerializeField]    public Text skill1;
    [SerializeField]    public Text skill2;
    [SerializeField]    public Text skill3;
    [SerializeField]    public Text skill4;
    [SerializeField]    public Text skill5;
    [SerializeField]    public Text skill0Hp;
    [SerializeField]    public Text skill1Dmg;
    [SerializeField]    public Text skill2Dmg;
    [SerializeField]    public Text skill3CoolDown;
    [SerializeField]    public Text skill4CoolDown;
    [SerializeField]    public Text skill5Dmg;

    [Header("데미지&쿨타임")]
    [SerializeField] public int skill0Health;
    [SerializeField] public int baseDamage;
    [SerializeField] public int maceDamage;
    [SerializeField] public float jumpCoolDown;
    [SerializeField] public float trapCoolDown;
    [SerializeField] public int poisonDamage;

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
        skill0Level = playData.skill0Lv;
        skill1Level = playData.skill1Lv;
        skill2Level = playData.skill2Lv;
        skill3Level = playData.skill3Lv;
        skill4Level = playData.skill4Lv;
        skill5Level = playData.skill5Lv;
        skill1LabLevel = playData.skill1LabLv;
        skill2LabLevel = playData.skill2LabLv;
        skill3LabLevel = playData.skill3LabLv;
        skill4LabLevel = playData.skill4LabLv;
        earnedCoins = playData.coins;
        earnedGene = playData.gene;
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
            Destroy(gameObject);
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
            Destroy(gameObject);
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
        if (HealthGauge.health == 369)        // 던전 클리어 시 자동저장
        {
            if (HealthGauge.canAutoSave == true)
            {
                Debug.Log("클리어 자동저장");
                SaveVolume();
                playData.coins += getCoinInstance;
                playData.gene += getGeneInstance;
                playData.skill3Enable = getSkill3EnableInstance;
                playData.skill4Enable = getSkill4EnableInstance;
                playData.skill5Enable = getSkill5EnableInstance;
                string data = JsonUtility.ToJson(playData);
                File.WriteAllText(path + filename, data);
                HealthGauge.canAutoSave = false;
            }
        }
        yield return null;
    }
    public void OnClickSkill0LvUp()
    {
        if (earnedCoins >= 50)                  // 코인 제한
        {
            if (skill0Level < 10)                    // 스킬 최대레벨 10
            {
                earnedCoins -= 50;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill0Level += 1;
                playData.skill0Lv = skill0Level;
                playData.coins = earnedCoins;
            }
            else if (skill0Level >= 10)
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
    public void OnClickSkill4LvUp()
    {
        if (earnedCoins >= 50)
        {
            if (skill4Level < 2)
            {
                earnedCoins -= 50;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill4Level += 1;
                playData.skill4Lv = skill4Level;
                playData.coins = earnedCoins;
            }
            else if (skill4Level >= 2)
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
    public void OnClickSkill5LvUp()
    {
        if (earnedCoins >= 200)                  // 코인 제한
        {
            if (skill5Level < 9)                    // 스킬 최대레벨 10(9+1)
            {
                earnedCoins -= 200;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill5Level += 1;
                playData.skill5Lv = skill5Level;
                playData.coins = earnedCoins;
            }
            else if (skill5Level >= 9)
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
    public void OnClickSkill1LabLvUp()
    {
        if (earnedGene >= 1)                  // 코인 제한
        {
            if (skill1LabLevel < 4)
            {
                earnedGene -= 1;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill1LabLevel += 1;
                playData.skill1LabLv = skill1LabLevel;
                playData.gene = earnedGene;
            }
            else if (skill1LabLevel >= 4)
            {
                masteredLab.SetActive(true);
                Invoke("MasterWarningLab", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }
        else
        {
            needCostLab.SetActive(true);
            Invoke("CostWarningLab", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill2LabLvUp()
    {
        if (earnedGene >= 1)                  // 코인 제한
        {
            if (skill2LabLevel < 4)
            {
                earnedGene -= 1;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill2LabLevel += 1;
                playData.skill2LabLv = skill2LabLevel;
                playData.gene = earnedGene;
            }
            else if (skill2LabLevel >= 4)
            {
                masteredLab.SetActive(true);
                Invoke("MasterWarningLab", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }
        else
        {
            needCostLab.SetActive(true);
            Invoke("CostWarningLab", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill3LabLvUp()
    {
        if (earnedGene >= 1)                  // 코인 제한
        {
            if (skill3LabLevel < 4)
            {
                earnedGene -= 1;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skill3LabLevel += 1;
                playData.skill3LabLv = skill3LabLevel;
                playData.gene = earnedGene;
            }
            else if (skill3LabLevel >= 4)
            {
                masteredLab.SetActive(true);
                Invoke("MasterWarningLab", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }else
        {
            needCostLab.SetActive(true);
            Invoke("CostWarningLab", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void Sync()
    {
        skill0.text = skill0Level.ToString();
        skill1.text = (skill1Level + 1).ToString();
        skill2.text = (skill2Level + 1).ToString();
        skill3.text = (skill3Level + 1).ToString();
        skill4.text = (skill4Level + 1).ToString();
        //skill5.text = (skill5Level + 1).ToString();
        skill0Health = 100 + (skill0Level * 20);
        baseDamage = (skill1Level + 1) * 30;
        maceDamage = (skill2Level + 1) * 20;
        jumpCoolDown = 6 - (skill3Level + 1);
        trapCoolDown = 4 - (skill4Level + 1);
        //poisonDamage = (skill5Level + 1) * 20;
        skill0Hp.text = skill0Health.ToString();
        skill1Dmg.text = baseDamage.ToString();
        skill2Dmg.text = maceDamage.ToString();
        skill3CoolDown.text = jumpCoolDown.ToString();
        skill4CoolDown.text = trapCoolDown.ToString();
        //skill5Dmg.text = poisonDamage.ToString();
        earnedCoinsCount.text = earnedCoins.ToString();
        earnedCoinsCountMain.text = earnedCoins.ToString();
        earnedGeneCount.text = earnedGene.ToString();
        earnedGeneCountMain.text = earnedGene.ToString();
    }
    public void Instancing()
    {
        bgmVolumeInstance = playData.bgmVolume;
        effectVolumeInstance = playData.effectVolume;
        skill0LvInstance = playData.skill0Lv;
        skill1LvInstance = playData.skill1Lv+1;
        skill2LvInstance = playData.skill2Lv+1;
        skill3LvInstance = playData.skill3Lv+1;
        skill4LvInstance = playData.skill4Lv + 1;
        skill5LvInstance = playData.skill5Lv + 1;
        skill1LabLvInstance = playData.skill1LabLv;
        skill2LabLvInstance = playData.skill2LabLv;
        skill3LabLvInstance = playData.skill3LabLv;
        skill4LabLvInstance = playData.skill4LabLv;
        skill3EnableInstance = playData.skill3Enable;
        skill4EnableInstance = playData.skill4Enable;
        skill5EnableInstance = playData.skill5Enable;
        hideNoticeInstance = playData.hideHelpNotice;
        coinsInstance = playData.coins;
        geneInstance = playData.gene;
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
    public void CostWarningLab()
    {
        needCostLab.SetActive(false);
    }
    public void MasterWarningLab()
    {
        masteredLab.SetActive(false);
    }
    public void ForTestCoin()
    {
        playData.coins = 100;
        string data = JsonUtility.ToJson(playData);
        File.WriteAllText(path + filename, data);
    }
}
