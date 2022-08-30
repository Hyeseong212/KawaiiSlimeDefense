using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject vfx;

    private int wayPointCount;  // 이동 경로 갯수
    public Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스

    Material outlineSeleted;
    Material outlineClicked;

    NavMeshAgent navMeshAgent;

    Renderer renderers;
    List<Material> materialList = new List<Material>();
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    WaitForSeconds delay = new WaitForSeconds(0.9f);

    public bool isDead;

    public EnemyData thisEnemydata;
    public float TotalHP;

    public GameObject HpBar;

    [SerializeField] GameObject rewardGold;
    Animator animator;
    Camera m_cam;
    private void OnMouseEnter()
    {
        if (!isDead)
        {
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outlineSeleted);

            renderers.materials = materialList.ToArray();
        }
    }

    private void OnMouseExit()
    {
        if (!isDead)
        {
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Remove(outlineSeleted);
            materialList.Remove(outlineClicked);

            renderers.materials = materialList.ToArray();
        }
    }
    private void OnMouseDown()
    {
        if (!isDead)
        {
            OnMouseExit();

            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(outlineClicked);

            renderers.materials = materialList.ToArray();
        }
    }

    private void OnMouseUp()
    {
        if (!isDead)
        {
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Remove(outlineSeleted);
            materialList.Remove(outlineClicked);

            renderers.materials = materialList.ToArray();
            OnMouseEnter();
        }
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        m_cam = Camera.main;
        isDead = false;
        outlineSeleted = new Material(Shader.Find("Draw/OutlineShaderSelected"));
        outlineClicked = new Material(Shader.Find("Draw/OutlineShaderClicked"));
        renderers = skinnedMeshRenderer.GetComponent<Renderer>();
        wayPointCount = EnemySpawner.i.P1wayPoints.Length;
        wayPoints = new Transform[EnemySpawner.i.P1wayPoints.Length];
        wayPoints = EnemySpawner.i.P1wayPoints; //웨이포인트 저장
        StartCoroutine("Moveto");
    }
    private void Update()
    {
        thisEnemydata = new EnemyData(thisEnemydata.wave, thisEnemydata.index, thisEnemydata.type, thisEnemydata.name, thisEnemydata.speed,
            thisEnemydata.hp, this.gameObject);

    }
    IEnumerator Moveto()
    {
        if (!isDead)
        {
            currentIndex++;
            navMeshAgent.SetDestination(wayPoints[currentIndex].transform.position);
            while (true)
            {
                if (Vector3.Distance(transform.position, wayPoints[currentIndex].transform.position) < 0.7f) NextMoveTo();
                yield return null;
            }
        }
    }

    private void NextMoveTo()
    {
        if (!isDead)
        {
            if (currentIndex < wayPointCount - 1)
            {
                currentIndex++;
                navMeshAgent.SetDestination(wayPoints[currentIndex].transform.position);
            }
            else
            {
                currentIndex = 0;
            }
        }
    }
    public void SetActive()
    {
        vfx.SetActive(true);
        StartCoroutine("DelayedSetActiveFalse");
    }
    IEnumerator DelayedSetActiveFalse()
    {
        yield return delay;
        vfx.SetActive(false);
    }
    public void Setup(EnemyData enemydata)
    {
        thisEnemydata = enemydata;
        SpeedChange(thisEnemydata.speed);
        TotalHP = thisEnemydata.hp;
    }
    void ToEnemyController()
    {
        for (int i = 0; i < EnemySpawner.i.P1enemyInThisWaveList.Count; i++)
        {
            if (thisEnemydata.index == EnemySpawner.i.P1enemyInThisWaveList[i].index)
            {
                EnemySpawner.i.P1enemyInThisWaveList[i] = thisEnemydata;
            }
        }
    }
    public void Hit()
    {
        //맞는 오브젝트 풀링 시켜서 처리할까..?
        if (!isDead)
        {
            ToEnemyController();
            EnemySpawner.i.HPbarDecrease(this.GetComponent<Enemy>());
            if (thisEnemydata.hp <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        Destroy(navMeshAgent);
        //navMeshAgent.isStopped = true;
        animator.SetBool("IsDead", true);
        StartCoroutine("DieAnimation");
        GameManager.i.currentGold += EnemySpawner.i.currentWave;//돈주고 해당돈만큼 UI에 표시
        GameManager.i.Gold.text = GameManager.i.currentGold.ToString();

        for (int i = 0; i < EnemySpawner.i.P1enemyInThisWaveList.Count; i++)
        {
            if (thisEnemydata.index == EnemySpawner.i.P1enemyInThisWaveList[i].index && thisEnemydata.wave == EnemySpawner.i.P1enemyInThisWaveList[i].wave)
            {
                EnemySpawner.i.P1enemyInThisWaveList.Remove(EnemySpawner.i.P1enemyInThisWaveList[i]);
                GameManager.i.P1currentMonster.text = GameManager.i.PlayerID + " 남은 몬스터 수 : " + EnemySpawner.i.P1enemyInThisWaveList.Count.ToString();
            }
        }
        GameObject gold = Instantiate(rewardGold, transform.position, Quaternion.identity);
        gold.transform.position = m_cam.WorldToScreenPoint(this.transform.position);
        gold.transform.SetParent(GameObject.Find("HPRenderer").transform);
        gold.GetComponentInChildren<RewardGold>().monstertr = transform;

        gold.GetComponentInChildren<Text>().text = "+" + EnemySpawner.i.currentWave + " Gold";

        EnemySpawner.i.HPbarDestroy(HpBar);
        Invoke("DestroyObject", 3f);
    }
    IEnumerator DieAnimation()
    {
        yield return delay;
        while (true) 
        {
            transform.Translate(new Vector3(0,-2,0) * Time.deltaTime);
            yield return null;
        } 
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void HPbarSetup(GameObject Hpbar)
    {
        HpBar = Hpbar;
    }
    public void SpeedChange(float _speed)
    {
        if (navMeshAgent != null)
        {
            if (navMeshAgent.speed != 0)
            {
                navMeshAgent.speed = _speed;
            }
        }
    }
    public void Stunned(float _speed)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = _speed;
        }
    }
}
