using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber { Player1, Player2, Player3, Player4 }
public class GameManager : MonoSingleton<GameManager>
{
    public PlayerNumber playerNumber;

    public float testFirstWaveTime = 0;
    public float testSecondWaveTime = 0;

    float waveTime = 0;
    float uiWaveTime = 0;
    [SerializeField] Text uiWaveTimeTxt;

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
    }
    private void Start()
    {
        StartCoroutine("WaveTimeLeft");
    }
    private void Update()//UI������ �����ð� �����ִ°�
    {
        if (EnemySpawner.i.currentWave < 27)
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
                if (EnemySpawner.i.currentWave == 0)
                {
                    //ó�������Ҷ�
                    uiWaveTime = testFirstWaveTime;
                    waveTime = testFirstWaveTime;
                    EnemySpawner.i.currentWave++;
                }
                else //���̺����
                {
                    uiWaveTime = testSecondWaveTime;
                    waveTime = testSecondWaveTime;
                    EnemySpawner.i.NextWave();
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
