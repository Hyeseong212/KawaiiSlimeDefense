using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertScroll : MonoSingleton<AlertScroll>
{
    [SerializeField] GameObject textObject;
    [SerializeField] GameObject textObjectParent;
    List<string> textRecord;
    WaitForSeconds delay = new WaitForSeconds(7f); 
    private void Awake()
    {
        textRecord = new List<string>();
    }
    public void SlimeGeneSetter(GameObject slimename)//���߿� �ڷᱸ�� queue�� �ٲ��
    {
        if (slimename.name == "CommonSlime_Blue(Clone)")
        {
            textRecord.Insert(0, "(�Ϲ�Ÿ��) �Ķ� �������� ������ϴ�.");
        }
        else if (slimename.name == "CommonSlime_Red(Clone)")
        {
            textRecord.Insert(0, "(�Ϲ�Ÿ��) ���� �������� ������ϴ�.");
        }
        else if (slimename.name == "CommonSlime_Grey(Clone)")
        {
            textRecord.Insert(0, "(�Ϲ�Ÿ��) ȸ�� �������� ������ϴ�.");
        }
        else if (slimename.name == "CommonSlime_Green(Clone)")
        {
            textRecord.Insert(0, "(�Ϲ�Ÿ��) �ʷ� �������� ������ϴ�.");
        }
        else if (slimename.name == "CommonSlime_Yellow(Clone)")
        {
            textRecord.Insert(0, "(�Ϲ�Ÿ��) ��� �������� ������ϴ�.");
        }
        GameObject text = Instantiate(textObject, textObjectParent.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity) as GameObject;
        text.GetComponent<RectTransform>().SetParent(textObjectParent.transform);
        text.GetComponent<Text>().text = textRecord[0];
        StartCoroutine(TextFade(text.GetComponent<Text>()));
        Destroy(text, 10f);
    }
    public void GetGoldAlert(string msg)
    {
        textRecord.Insert(0, msg + "��ȭ�� ������ϴ�");
        GameObject text = Instantiate(textObject);
        text.GetComponent<RectTransform>().SetParent(textObjectParent.transform);
        text.GetComponent<Text>().text = textRecord[0];
        StartCoroutine(TextFade(text.GetComponent<Text>()));
        Destroy(text, 10f);
    }
    IEnumerator TextFade(Text text)
    {
        if (text != null)
        {
            yield return delay;
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
