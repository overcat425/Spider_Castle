using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonMenu : MonoBehaviour
{
    [SerializeField] private GameObject dungeonUi;

    [Header("스테이지")]
    [SerializeField] private GameObject stage1info;
    [SerializeField] private GameObject stage2info;
    [SerializeField] private GameObject stage3info;

    [Header("Warining")]
    private bool warning;
    private int soundOnce;
    public float percent;
    public float alpha;
    [SerializeField] private AudioClip warningSound;
    [SerializeField] private GameObject hardWarning;
    [SerializeField] private Image warningImage;
    [SerializeField] private AnimationCurve curveWarning;


    [Header("스테이지 모드")]
    [SerializeField] private GameObject stage1Normal;
    [SerializeField] private GameObject stage1NormalText;
    [SerializeField] private GameObject stage1Hard;
    [SerializeField] private GameObject stage1HardText;
    [SerializeField] private GameObject stage1ChangeToHard;
    [SerializeField] private GameObject stage1ChangeToNormal;
    [SerializeField] private GameObject stage2Normal;
    [SerializeField] private GameObject stage2NormalText;
    [SerializeField] private GameObject stage2Hard;
    [SerializeField] private GameObject stage2HardText;
    [SerializeField] private GameObject stage2ChangeToHard;
    [SerializeField] private GameObject stage2ChangeToNormal;
    [SerializeField] private GameObject stage3Normal;
    [SerializeField] private GameObject stage3NormalText;
    [SerializeField] private GameObject stage3Hard;
    [SerializeField] private GameObject stage3HardText;
    [SerializeField] private GameObject stage3ChangeToHard;
    [SerializeField] private GameObject stage3ChangeToNormal;
    void Start()
    {
        stage1info.SetActive(false);
        stage2info.SetActive(false);
        stage3info.SetActive(false);
        warning = false;
        soundOnce = 0;
    }
    void Update()
    {
        if (warning == true)
        {
            StartCoroutine("HardWarning");
            WarningSoundOnce();
        }else if (warning == false)
        {
            percent = 0f;
            alpha = 0f;
            soundOnce = 0;
        }
    }
    public void OnClickStage1()
    {
        stage1info.SetActive(true);
        stage2info.SetActive(false);
        stage3info.SetActive(false);
    }
    public void OnClickStage2()
    {
        stage1info.SetActive(false);
        stage2info.SetActive(true);
        stage3info.SetActive(false);
    }
    public void OnClickStage3()
    {
        stage1info.SetActive(false);
        stage2info.SetActive(false);
        stage3info.SetActive(true);
    }
    public void OnClickStage1HardBtn()
    {
        if(stage1Normal.activeSelf == true)
        {
            stage1Normal.SetActive(false);
            stage1NormalText.SetActive(false);
            stage1Hard.SetActive(true);
            stage1HardText.SetActive(true);
            stage1ChangeToHard.SetActive(false);
            stage1ChangeToNormal.SetActive(true);
        }else if (stage1Hard.activeSelf == true)
        {
            stage1Hard.SetActive(false);
            stage1HardText.SetActive(false);
            stage1Normal.SetActive(true);
            stage1NormalText.SetActive(true);
            stage1ChangeToNormal.SetActive(false);
            stage1ChangeToHard.SetActive(true);
        }
    }
    public void OnClickStage2HardBtn()
    {
        if (stage2Normal.activeSelf == true)
        {
            stage2Normal.SetActive(false);
            stage2NormalText.SetActive(false);
            stage2Hard.SetActive(true);
            stage2HardText.SetActive(true);
            stage2ChangeToHard.SetActive(false);
            stage2ChangeToNormal.SetActive(true);
        }
        else if (stage2Hard.activeSelf == true)
        {
            stage2Hard.SetActive(false);
            stage2HardText.SetActive(false);
            stage2Normal.SetActive(true);
            stage2NormalText.SetActive(true);
            stage2ChangeToNormal.SetActive(false);
            stage2ChangeToHard.SetActive(true);
        }
    }
    public void OnClickStage3HardBtn()
    {
        if (stage3Normal.activeSelf == true)
        {
            stage3Normal.SetActive(false);
            stage3NormalText.SetActive(false);
            stage3Hard.SetActive(true);
            stage3HardText.SetActive(true);
            stage3ChangeToHard.SetActive(false);
            stage3ChangeToNormal.SetActive(true);
        }
        else if (stage3Hard.activeSelf == true)
        {
            stage3Hard.SetActive(false);
            stage3HardText.SetActive(false);
            stage3Normal.SetActive(true);
            stage3NormalText.SetActive(true);
            stage3ChangeToNormal.SetActive(false);
            stage3ChangeToHard.SetActive(true);
        }
    }
    public void OnClickHardWarning()
    {
        warning = true;
        hardWarning.SetActive(true);
    }
    private IEnumerator HardWarning()
    {
        if (percent <= 1f)
        {
            percent += Time.deltaTime / 0.5f;
            alpha += Time.deltaTime / 0.5f;
            Color color = warningImage.color;
            color.a = alpha;
            warningImage.color = color;
        }
        else if ((percent > 1f)&&(percent <=2f))
        {
            percent += Time.deltaTime / 0.5f;
            alpha -= Time.deltaTime / 0.5f;
            Color color = warningImage.color;
            color.a = alpha;
            warningImage.color = color;
        }else if (percent > 2f)
        {
            percent = 0f;
            alpha = 0f;
        }
        yield return new WaitForSeconds(3f);
        StopCoroutine("HardWarning");
        warning = false;
        hardWarning.SetActive(false);
    }
    private void WarningSoundOnce()
    {
        if(soundOnce == 0)
        {
            SoundManager.SoundEffect.SoundPlay("WarningSound", warningSound);
            soundOnce = 1;
        }
    }
    public void StartStage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void StartStage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void StartStage3()
    {
        SceneManager.LoadScene("Stage3");
    }
    public void StartStage1Hard()
    {
        SceneManager.LoadScene("Stage1Hard");
    }
    public void StartStage2Hard()
    {
        SceneManager.LoadScene("Stage2Hard");
    }
    public void StartStage3Hard()
    {
        SceneManager.LoadScene("Stage3Hard");
    }
}
