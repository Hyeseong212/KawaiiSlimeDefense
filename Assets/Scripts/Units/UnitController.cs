using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		unitMarker;
	private	NavMeshAgent	navMeshAgent;
    public Face faces;
    Transform slimeVector;

    [SerializeField] Animator animator;
    public GameObject mainSlime;
    private Material faceMaterial;

    float distance;
    float remainTime;

    //public Button idleBut, walkBut, jumpBut, attackBut, damageBut0, damageBut1, damageBut2;
    private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

    private void Start()
    {
        slimeVector = GetComponent<Transform>();
        faceMaterial = mainSlime.GetComponent<Renderer>().materials[1];
        Idle();
        //idleBut.onClick.AddListener(delegate { Idle(); });
        //walkBut.onClick.AddListener(delegate { ChangeStateTo(SlimeAnimationState.Walk); });
        //jumpBut.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Jump); });
        //attackBut.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Attack); });
        //damageBut0.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Damage); mainSlime.GetComponent<EnemyAi>().damType = 0; });
        //damageBut1.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Damage); mainSlime.GetComponent<EnemyAi>().damType = 1; });
        //damageBut2.onClick.AddListener(delegate { LookAtCamera(); ChangeStateTo(SlimeAnimationState.Damage); mainSlime.GetComponent<EnemyAi>().damType = 2; });
    }
    public void SelectUnit()
	{
		unitMarker.SetActive(true);
	}

	public void DeselectUnit()
	{
		unitMarker.SetActive(false);
	}

	public void MoveTo(Vector3 end)
	{
        StopCoroutine("StopCheck");
        navMeshAgent.isStopped = false;
        animator.SetBool("IsMove", true);
        SetFace(faces.WalkFace);
        navMeshAgent.SetDestination(end);
        distance  = Vector3.Distance(slimeVector.position ,end);
        remainTime = distance/ navMeshAgent.speed;
        StartCoroutine("StopCheck");
    }
    IEnumerator StopCheck()
    {
        yield return new WaitForSeconds(remainTime);
        animator.SetBool("IsMove", false);
        navMeshAgent.isStopped = true;
    }
    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }
    void Idle()
    {

    }
    public void ChangeStateTo(SlimeAnimationState state)
    {

    }
}

