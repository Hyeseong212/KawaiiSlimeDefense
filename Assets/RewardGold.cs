using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardGold : MonoBehaviour
{
    public Transform monstertr;
    Camera m_cam;
    void Start()
    {
        m_cam = Camera.main;
        StartCoroutine(TextFade(this.GetComponent<Text>()));
        Invoke("DestroyThis", 2f);
    }
    private void Update()
    {
        if(monstertr != null)
        {
            transform.position = m_cam.WorldToScreenPoint(monstertr.transform.position + new Vector3(0, 1.15f,0));
        }
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
    IEnumerator TextFade(Text text)
    {
        if (text != null)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

            while (text.color.a > 0.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
                yield return null;
            }
        }
        else
        {
            yield break;
        }
    }
}
