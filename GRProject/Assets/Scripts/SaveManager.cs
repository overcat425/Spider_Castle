using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class PlayerData
{
    public float bgmVolume;
    public float effectVolume;
    public int coins;
    public int gene;
    public int[] skillLv = new int[6];
    public int[] skillLabLv = new int[4];
    public bool[] skillEnable = new bool[3];
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
    private AudioClip alertClip;
    PlayerData playData = new PlayerData();
    string path;
    string filename = "savedata";
    public static bool savefile;
    public bool hideNotice;

    [Header("�ν��Ͻ�")]       // �ܺ� ��ũ��Ʈ ���� static ��ġ
    public static float bgmVolumeStat;
    public static float getBgmStat;
    public static float effectVolumeStat;
    public static float getEffectStat;
    public static int[] skillLvStat = new int[6];
    public int[] skillLabLvStat = new int[4];
    public static bool[] skillEnableStat = new bool[3];
    public static bool[] getSkillEnableStat = new bool[3];
    public static bool hideNoticeStat;
    public static int coinsStat;
    public static int geneStat;

    public static int getCoinStat;          // CoinManager���� �������� ��ġ
    public static int getGeneStat;

    [Header("��ȭ")]
    public int earnedCoins;
    [SerializeField]    public Text earnedCoinsCount;
    [SerializeField]    public Text earnedCoinsCountMain;
    public int earnedGene;
    [SerializeField] public Text earnedGeneCount;
    [SerializeField] public Text earnedGeneCountMain;


    [Header("��ų����")]
    public int[] skillLevel = new int[6];
    public int[] skillLabLevel = new int[4];
    public Text[] skillText;        // Text 6��
    public Text skill0Hp;
    public Text skill1Dmg;
    public Text skill2Dmg;
    public Text skill3CoolDown;
    public Text skill4CoolDown;
    public Text skill5Dmg;

    [Header("������&��Ÿ��")]
    [SerializeField] public int skill0Health;
    [SerializeField] public int baseDamage;
    [SerializeField] public int maceDamage;
    [SerializeField] public float jumpCoolDown;
    [SerializeField] public float trapCoolDown;
    [SerializeField] public int poisonDamage;

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
        if (instance == null)       // �̱��� ����
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this.gameObject);
        //path = Application.persistentDataPath+ "/";
        path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/SpiderCastle/Savedata/";
    }
    public void Start()
    {
        if (File.Exists(path+filename))             // ���̺� ������ �ִٸ� �ε�
        {
            savefile = true;
        }
        LoadData();
        for(int i = 0; i < 6; i++){skillLevel[i] = playData.skillLv[i];}
        for(int i = 0; i < 4; i++){skillLabLevel[i] = playData.skillLabLv[i];}
        for (int i = 0; i < 3; i++){getSkillEnableStat[i] = playData.skillEnable[i];}
        earnedCoins = playData.coins;
        earnedGene = playData.gene;
        hideNotice = playData.hideHelpNotice;
        //HealthGauge.canAutoSave = true;
    }
    public void Update()
    {
        //playData.bgmVolume = soundManager.musicSource.volume;
        //playData.effectVolume =  soundManager.effectSource.volume;
        Initializing();
        TextSync();
        StartCoroutine("AutoSave");
        //if(SceneManager.GetActiveScene().name == "GameOver")
        //{
        //    Destroy(gameObject);
        //}
        if (HealthGauge.isDie == true)
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
            //ForTestCoin();
            Destroy(gameObject);
            SceneManager.LoadScene("StartGame");
        }
    }
    public void LoadGame()
    {
        if (savefile == false)                      // ���̺������� ���� ��
        {
            noSaveData.SetActive(true);
            SoundManager.SoundEffect.SoundPlay("alertClip", alertClip);
        }
        else if (savefile == true)                  // ���̺����� ���� ��
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
        print(path);
    }
    public void SaveSound()
    {
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
    public IEnumerator AutoSave()
    {
        if (HealthGauge.health == 369)        // ���� Ŭ���� �� �ڵ�����
        {
            if (HealthGauge.canAutoSave == true)
            {
                Debug.Log("Ŭ���� �ڵ�����");
                SaveVolume();
                playData.coins += getCoinStat;
                playData.gene += getGeneStat;
                for (int i = 0; i < 3; i++)
                {
                    playData.skillEnable[i] = getSkillEnableStat[i];
                }
                string data = JsonUtility.ToJson(playData);
                File.WriteAllText(path + filename, data);
                HealthGauge.canAutoSave = false;
            }
        }
        yield return null;
    }
    public void OnClickSkill0LvUp()
    {
        if (earnedCoins >= 50)                  // ���� ����
        {
            if (skillLevel[0] < 10)                    // ��ų �ִ뷹�� 10
            {
                earnedCoins -= 50;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLevel[0] += 1;
                playData.skillLv[0] = skillLevel[0];
                playData.coins = earnedCoins;
            }
            else if (skillLevel[0] >= 10)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill1LvUp()             // 1�� ��ų ��ȭ
    {
        if (earnedCoins >= 10)                  // ���� ����
        {
            if (skillLevel[1] < 9)                    // ��ų �ִ뷹�� 10(9+1)
            {
                earnedCoins -= 10;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLevel[1] += 1;
                playData.skillLv[1] = skillLevel[1];
                playData.coins = earnedCoins;
            }
            else if (skillLevel[1] >= 9)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill2LvUp()             // 2�� ��ų ��ȭ
    {
        if (earnedCoins >= 50)
        {
            if (skillLevel[2] < 9)
            {
                earnedCoins -= 50;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLevel[2] += 1;
                playData.skillLv[2] = skillLevel[2];
                playData.coins = earnedCoins;
            }
            else if (skillLevel[2] >= 9)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill3LvUp()              // 3�� ��ų ��ȭ
    {
        if (earnedCoins >= 100)
        {
            if (skillLevel[3] < 2)
            {
                earnedCoins -= 100;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLevel[3] += 1;
                playData.skillLv[3] = skillLevel[3];
                playData.coins = earnedCoins;
            }
            else if (skillLevel[3] >= 2)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill4LvUp()         // 4�� ��ų ��ȭ
    {
        if (earnedCoins >= 50)
        {
            if (skillLevel[4] < 2)
            {
                earnedCoins -= 50;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLevel[4] += 1;
                playData.skillLv[4] = skillLevel[4];
                playData.coins = earnedCoins;
            }
            else if (skillLevel[4] >= 2)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill5LvUp()             // 5�� ��ų ��ȭ
    {
        if (earnedCoins >= 100)                  // ���� ����
        {
            if (skillLevel[5] < 9)                    // ��ų �ִ뷹�� 10(9+1)
            {
                earnedCoins -= 100;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLevel[5] += 1;
                playData.skillLv[5] = skillLevel[5];
                playData.coins = earnedCoins;
            }
            else if (skillLevel[5] >= 9)
            {
                mastered.SetActive(true);
                Invoke("MasterWarning", 0.5f);
                SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
            }
        }else
        {
            needCost.SetActive(true);
            Invoke("CostWarning", 0.5f);
            SoundManager.SoundEffect.SoundPlay("MaxLvSound", maxLvSound);
        }
    }
    public void OnClickSkill1LabLvUp()
    {
        if (earnedGene >= 1)                  // ���� ����
        {
            if (skillLabLevel[0] < 4)
            {
                earnedGene -= 1;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLabLevel[0] += 1;
                playData.skillLabLv[0] = skillLabLevel[0];
                playData.gene = earnedGene;
            }
            else if (skillLabLevel[0] >= 4)
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
    public void OnClickSkill2LabLvUp()
    {
        if (earnedGene >= 1)                  // ���� ����
        {
            if (skillLabLevel[1] < 4)
            {
                earnedGene -= 1;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLabLevel[1] += 1;
                playData.skillLabLv[1] = skillLabLevel[1];
                playData.gene = earnedGene;
            }
            else if (skillLabLevel[1] >= 4)
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
        if (earnedGene >= 1)                  // ���� ����
        {
            if (skillLabLevel[2] < 4)
            {
                earnedGene -= 1;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLabLevel[2] += 1;
                playData.skillLabLv[2] = skillLabLevel[2];
                playData.gene = earnedGene;
            }
            else if (skillLabLevel[2] >= 4)
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
    public void OnClickSkill4LabLvUp()
    {
        if (earnedGene >= 1)                  // ���� ����
        {
            if (skillLabLevel[3] < 4)
            {
                earnedGene -= 1;
                SoundManager.SoundEffect.SoundPlay("LvUpSound", lvUpSound);
                skillLabLevel[3] += 1;
                playData.skillLabLv[3] = skillLabLevel[3];
                playData.gene = earnedGene;
            }
            else if (skillLabLevel[3] >= 4)
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
    public void TextSync()              // �ǽð� �� To �ؽ�Ʈ
    {
        skillText[0].text = skillLevel[0].ToString();
        for (int i = 1; i < 6; i++)
        {
            skillText[i].text = (skillLevel[i] + 1).ToString();
        }
        skill0Health = 100 + (skillLevel[0] * 20);
        baseDamage = (skillLevel[1] + 1) * 50;
        maceDamage = (skillLevel[2] + 1) * 10;
        jumpCoolDown = 6 - (skillLevel[3] + 1);
        trapCoolDown = 4 - (skillLevel[4] + 1);
        poisonDamage = (skillLevel[5] + 1) * 2;
        skill0Hp.text = skill0Health.ToString();
        skill1Dmg.text = baseDamage.ToString();
        skill2Dmg.text = maceDamage.ToString();
        skill3CoolDown.text = jumpCoolDown.ToString();
        skill4CoolDown.text = trapCoolDown.ToString();
        skill5Dmg.text = poisonDamage.ToString();
        earnedCoinsCount.text = earnedCoins.ToString();
        earnedCoinsCountMain.text = earnedCoins.ToString();
        earnedGeneCount.text = earnedGene.ToString();
        earnedGeneCountMain.text = earnedGene.ToString();
    }
    public void Initializing()      // �ٸ� ��ũ��Ʈ�� �������� ����
    {
        bgmVolumeStat = playData.bgmVolume;
        effectVolumeStat = playData.effectVolume;
        for (int i = 0; i < 6; i++)
        {
            skillLvStat[i] = playData.skillLv[i];
            if (i > 0) skillLvStat[i] += 1;
        }
        for (int i = 0; i < 4; i++)
        {
            skillLabLvStat[i] = playData.skillLabLv[i];
        }
        for (int i = 0; i < 3; i++)
        {
            skillEnableStat[i] = playData.skillEnable[i];
        }
        hideNoticeStat = playData.hideHelpNotice;
        coinsStat = playData.coins;
        geneStat = playData.gene;
    }
    public void SaveVolume()
    {
        playData.bgmVolume = getBgmStat;
        playData.effectVolume = getEffectStat;
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