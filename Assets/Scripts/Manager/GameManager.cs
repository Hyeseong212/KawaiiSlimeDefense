using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber { Player1, Player2, Player3, Player4 }
public class GameManager : MonoSingleton<GameManager>
{
    bool isfirst;
    public PlayerNumber playerNumber;
    public string PlayerID;
    [Header("���̺�")]
    public float testFirstWaveTime = 0;
    public float testSecondWaveTime = 0;

    float waveTime = 0;
    float uiWaveTime = 0;
    [SerializeField] Text uiWaveTimeTxt;
    [SerializeField] Text currentWave;
    public Text P1currentMonster;
    public Text P2currentMonster;
    public Text P3currentMonster;
    public Text P4currentMonster;

    [Header("���,��ū,���ڼ� ����")]
    public int currentGold = 200;
    public int currentToken = 0;

    public Text Gold;
    public Text Token;
    private void Awake()
    {
        playerNumber = PlayerNumber.Player1;

        if (playerNumber == PlayerNumber.Player1)
        {
            Camera.main.transform.position = new Vector3(-40, GlobalOptions.i.options.cameraYvalue, 70);
        }
        else if(playerNumber == PlayerNumber.Player2)
        {

        }
        else if (playerNumber == PlayerNumber.Player3)
        {

        }
        else
        {

        }
        P1currentMonster.text = PlayerID + " ���� ���� �� : " + EnemySpawner.i.P1enemyInThisWaveList.Count.ToString();
    }
    private void Start()
    {
        isfirst = true;
        StartCoroutine("WaveTimeLeft");
        Gold.text = currentGold.ToString();
        Token.text = currentToken.ToString();
    }
    private void Update()//UI������ �����ð� �����ִ°�
    {
        if (EnemySpawner.i.currentWave < 26)
        {
            uiWaveTime -= Time.deltaTime;
            uiWaveTimeTxt.text = ((int)uiWaveTime).ToString();
        }
        else
        {
            uiWaveTime = 0;
            uiWaveTimeTxt.text = ((int)uiWaveTime).ToString();
        }
    }
    IEnumerator WaveTimeLeft()//���� ���ӿ��� ���ư��� �ð� (�������� Update������ 1����ó���ǰ� �ϴ°� ������ ���������Ͱ��Ƽ�)
    {
        while (true)
        {
            if (EnemySpawner.i.currentWave < 26)
            {
                if (isfirst)
                {
                    //ó�������Ҷ�
                    isfirst = false;
                    currentWave.text = "Round : " + EnemySpawner.i.currentWave.ToString();
                    uiWaveTime = testFirstWaveTime;
                    waveTime = testFirstWaveTime;
                }
                else //���̺����
                {
                    uiWaveTime = testSecondWaveTime;
                    waveTime = testSecondWaveTime;
                    EnemySpawner.i.NextWave();
                    currentWave.text = "Round : " + (EnemySpawner.i.currentWave).ToString();
                }
                uiWaveTime = waveTime;
                //UI������ �����ִ½ð� 60���� �ʱ�ȭ
                yield return new WaitForSeconds(waveTime);
            }
            else
            {
                GameClear();
                yield break;
            }
        }
    }
    private void GameClear()
    {
        Debug.Log("����Ŭ����");
    }
}
