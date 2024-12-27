using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int earnedCoins;                                     // ȹ���� ����
    public static int earnedCoinsStat;                        // �ҷ������ ��������
    [SerializeField]   public Text earnedCoinsCount;        // ȹ������ �ؽ�Ʈ
    private static CoinManager instance;                    // �������� �ν��Ͻ�

    public int earnedGene;                                      // ȹ���� ������
    public static int earnedGeneStat;                         // �ҷ������ ��������
    [SerializeField] public Text earnedGeneCount;        // ȹ�������� �ؽ�Ʈ
    private static CoinManager GeneInstance;            // ���������� �ν��Ͻ�

    public static CoinManager count_instance        // Ÿ ��ũ��Ʈ���� �ν��Ͻ��� ���ΰ��� �޾ƿ���
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
    public static CoinManager count_geneInstance    // Ÿ ��ũ��Ʈ���� �ν��Ͻ��� �����ڰ��� �޾ƿ���
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
