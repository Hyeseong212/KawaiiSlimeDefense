using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureSlime : MonoBehaviour
{
    public Face faces;
    public GameObject SmileBody;
    private Material faceMaterial;
    private Animator aniController;
    private Transform slimePos;

    WaitForSeconds cachedSeconds = new WaitForSeconds (2);
    private void Start()
    {
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        aniController = GetComponent<Animator>();
        slimePos = GetComponent<Transform>();
    }
    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    //랜덤한 애니메이션 재생

    void SetRandomAnimation()
    {
        int randomInt = Random.Range(0, 3);
        aniController.SetInteger("RandomIdle", randomInt);
        if (randomInt == 1)
        {
            SetFace(faces.attackFace);
        }
        else if (randomInt == 2)
        {
            SetFace(faces.WalkFace);
        }
        else if(randomInt == 0)
        {
            SetFace (faces.jumpFace);
        }
    }
    public IEnumerator RandomCoroutine()
    {
        yield return cachedSeconds;
        SetRandomAnimation();
        StartCoroutine("RandomCoroutine");
    }

}
