using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject enemy;
    private int maxChainCount;
    SlimeData slimedata;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject HitVfx;
    [SerializeField] GameObject HitVfxFroRareGreySlime;
    [SerializeField] GameObject HitVfxFroUniqueNekoSlime;
    [SerializeField] GameObject HitVfxFroUniqueQueenSlime;
    [SerializeField] GameObject HitVfxFroUniqueSproutSlime;

    public SkillCoolTime _skillCoolTime;

    private List<GameObject> enemyList;
    private float cushion;

    public void Setup(GameObject enemy, SlimeData slimedata)
    {
        enemyList = new List<GameObject>();
        enemyList.Add(enemy);
        this.enemy = enemy;
        this.slimedata = slimedata;
        maxChainCount = 1;
        cushion = 1;
        StartCoroutine("ChaseEnemy");
    }
    IEnumerator ChaseEnemy()
    {
        while (true)
        {
            if (enemy != null)
            {
                Vector3 newPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPos, bulletSpeed * Time.deltaTime);
                transform.LookAt(enemy.transform);
                if (Vector3.Distance(transform.position, enemy.transform.position) <= 0f)
                {
                    if (gameObject.name == "ChainLightening(Clone)")
                    {
                        enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts * cushion);
                        enemy.GetComponent<Enemy>().Hit();
                        if (maxChainCount <= 3)
                        {
                            NextMoveTo(4);
                        }
                        else if(maxChainCount == 4)
                        {
                            Destroy(this.gameObject);
                        }

                    }//레어 노랑슬라임
                    else if(gameObject.name == "Fireball(Clone)")
                    {
                        GameObject BoomVfx = Instantiate(HitVfx, transform.position,Quaternion.Euler(270,0,0));
                        BoomVfx.GetComponent<Fireball>().Damage = slimedata.attackpts;
                        Destroy(this.gameObject);
                    }//레어 빨강슬라임
                    else if(gameObject.name == "Piercer(Clone)")
                    {
                        //3명을 뚫고감
                        enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                        enemy.GetComponent<Enemy>().Hit();
                        if (maxChainCount <= 2)
                        {
                            NextMoveTo(2);
                        }
                        else if (maxChainCount == 3)
                        {
                            Destroy(this.gameObject);
                        }
                    }//레어 초록슬라임
                    else if (gameObject.name == "RareSlimePunch(Clone)")
                    {
                        int random = Random.Range(0, 2);
                        if(random == 0)//일반 공격
                        {
                            enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                            enemy.GetComponent<Enemy>().Hit();
                            Instantiate(HitVfx, transform.position, Quaternion.Euler(270, 0, 0));
                        }
                        else//특수공격
                        {
                            GameObject BoomVfx = Instantiate(HitVfxFroRareGreySlime, transform.position, Quaternion.Euler(0, 0, 0));
                            BoomVfx.GetComponent<GroundPunching>().Damage = slimedata.attackpts;
                            Destroy(this.gameObject);

                        }
                        Destroy(this.gameObject);
                    }//레어 회색슬라임
                    else if(gameObject.name == "UniqueNekoBullet(Clone)")
                    {
                        int random = Random.Range(0, 101);
                        if(random <= 20 && !_skillCoolTime.isCoolTime) 
                        {
                            GameObject PoisonStage = Instantiate(HitVfxFroUniqueNekoSlime, transform.position, Quaternion.Euler(0, 0, 0));
                            PoisonStage.GetComponent<SlowStage>().Damage = slimedata.attackpts;
                            PoisonStage.GetComponent<SlowStage>().CoroutineTrigger();
                            _skillCoolTime.SkillCoolTimeCoroutineTrigger();
                            enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                            enemy.GetComponent<Enemy>().Hit();
                            Instantiate(HitVfx, transform.position, Quaternion.identity);
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                            enemy.GetComponent<Enemy>().Hit();
                            Instantiate(HitVfx, transform.position, Quaternion.identity);
                            Destroy(this.gameObject);
                        }

                    }//유니크 네코
                    else if(gameObject.name == "UniqueQueenBullet(Clone)")
                    {
                        int random = Random.Range(0, 101);
                        if (random <= 20 && !_skillCoolTime.isCoolTime)
                        {
                            GameObject CuteSmash = Instantiate(HitVfxFroUniqueQueenSlime, transform.position, Quaternion.Euler(0, 0, 0));
                            _skillCoolTime.SkillCoolTimeCoroutineTrigger();
                            enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                            enemy.GetComponent<Enemy>().Hit();
                            Instantiate(HitVfx, transform.position, Quaternion.identity);
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                            enemy.GetComponent<Enemy>().Hit();
                            Instantiate(HitVfx, transform.position, Quaternion.identity);
                            Destroy(this.gameObject);
                        }
                    }//유니크 퀸
                    else if (gameObject.name == "UniqueSproutBullet(Clone)")
                    {
                        int random = Random.Range(0, 101);
                        if (random <= 30 && !_skillCoolTime.isCoolTime)
                        {
                            GameObject MeteorShower = Instantiate(HitVfxFroUniqueSproutSlime, transform.position+ new Vector3(0,3,0), Quaternion.Euler(0, 0, 0));
                            MeteorShower.GetComponent<MeteorShower>().Damage = slimedata.attackpts;
                            MeteorShower.GetComponent<MeteorShower>().CoroutineTrigger();
                            enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                            enemy.GetComponent<Enemy>().Hit();
                            _skillCoolTime.SkillCoolTimeCoroutineTrigger();
                            Instantiate(HitVfx, transform.position, Quaternion.identity);
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                            enemy.GetComponent<Enemy>().Hit();
                            Instantiate(HitVfx, transform.position, Quaternion.identity);
                            Destroy(this.gameObject);
                        }
                    }//유니크 새싹
                    else
                    {
                        enemy.GetComponent<Enemy>().thisEnemydata.hp = enemy.GetComponent<Enemy>().thisEnemydata.hp - (slimedata.attackpts);
                        enemy.GetComponent<Enemy>().Hit();
                        Instantiate(HitVfx, transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                    }
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }

    private void NextMoveTo(int maxcount)
    {
        maxChainCount++;
        cushion = cushion * 0.5f;
        if (enemyList.Count <= maxcount)
        {
            enemy = enemyList[enemyList.Count-1];
        }
        else 
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (!other.gameObject.GetComponent<Enemy>().isDead)
            {
                if (other.gameObject != enemy)
                {
                    if (enemyList.Count <= 3)
                    {
                        enemyList.Add(other.gameObject);
                    }
                }
            }
        }
    }
}
