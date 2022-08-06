using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserBtn : MonoBehaviour
{
        [SerializeField]  GameObject UserBtnUI;
    bool isActive = false;

    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveMenu()
    {

        //  UserBtnUI.SetActive(true);
        if (!isActive)
        {
            Debug.Log("��ưȰ��");
            isActive = true;
            UserBtnUI.SetActive(true);
            spriteRenderer.sprite = sprites[1];
        }
        else
        {
            Debug.Log("��ư��Ȱ��");
            isActive = false;
            UserBtnUI.SetActive(false);
            spriteRenderer.sprite = sprites[0];
        }

    }


}
