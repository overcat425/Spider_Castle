using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int earnedCoins;                                     // 획득한 코인
    public static int earnedCoinsStat;                        // 불러오기용 정적변수
    [SerializeField]   public Text earnedCoinsCount;        // 획득코인 텍스트
    private static CoinManager instance;                    // 코인정보 인스턴스

    public int earnedGene;                                      // 획득한 유전자
    public static int earnedGeneStat;                         // 불러오기용 정적변수
    [SerializeField] public Text earnedGeneCount;        // 획득유전자 텍스트
    private static CoinManager GeneInstance;            // 유전자정보 인스턴스

    public static CoinManager count_instance        // 타 스크립트에서 인스턴스로 코인갯수 받아오기
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CoinManager>();
            }
            return instance;
        }
    }
    public static CoinManager count_geneInstance    // 타 스크립트에서 인스턴스로 유전자갯수 받아오기
    {
        get
        {
            if (GeneInstance == null)
            {
                GeneInstance = GameObject.FindObjectOfType<CoinManager>();
            }
            return GeneInstance;
        }
    }
    private void Update()
    {
        earnedCoinsCount.text = earnedCoins.ToString();
        SaveManager.getCoinStat = earnedCoins;
        earnedCoinsStat = earnedCoins;
        earnedGeneCount.text = earnedGene.ToString();
        SaveManager.getGeneStat = earnedGene;
        earnedGeneStat = earnedGene;
    }
}
