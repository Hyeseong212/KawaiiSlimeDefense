using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleManager : MonoSingleton<GambleManager>
{
    int i = 1;
    public void GoldGamble()//��� ����
    {
        int spendGold;
        if (EnemySpawner.i.currentWave == 0)
        {
            i = 1;
        }
        else 
        {
            i = EnemySpawner.i.currentWave;
        }
        spendGold = i * 10;
        if (GameManager.i.currentGold >= spendGold)
        {
            int random = (Random.Range(-(i * 15) - 10, (i * 35) + 10));
            int rewardGold = random; //������
            GameManager.i.currentGold -= spendGold; //�Ҹ� ��常ŭ ����
            GameManager.i.currentGold += rewardGold; //�����常ŭ �ִ´�
            AlertScroll.i.GetGoldAlert((rewardGold-spendGold).ToString());
            if (GameManager.i.currentGold < 0)
            {
                GameManager.i.currentGold = 0;
            }
            GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
        }
        else
        {
            UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
        }
    }
    public void TokenGamble()//��ū ����
    {
        int spendGold = 5000;
        int rewardToken = Random.Range(0, 2); //������
        if (GameManager.i.currentGold >= spendGold)
        {
            GameManager.i.currentGold -= spendGold; //�Ҹ� ��常ŭ ����
            GameManager.i.currentToken += rewardToken;
            GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
            GameManager.i.Token.text = GameManager.i.currentToken.ToString();
        }
        else
        {
            UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
        }
    }
}
