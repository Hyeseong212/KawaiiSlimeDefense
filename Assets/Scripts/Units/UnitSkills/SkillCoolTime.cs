using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoolTime : MonoBehaviour
{
    [Header("½ºÅ³1ÄðÅ¸ÀÓ")]
    public float _skillCoolTime;
    float _time = 0;
    public bool isCoolTime;

    [Header("½ºÅ³2ÄðÅ¸ÀÓ")]
    public float _skillCoolTime2;
    float _time2 = 0;
    public bool isCoolTime2;
    public void SkillCoolTimeCoroutineTrigger()
    {
        StartCoroutine("SkillCoolTimeCoroutine");
    }
    public void SkillCoolTimeCoroutineTrigger2()
    {
        StartCoroutine("SkillCoolTimeCoroutine2");
    }
    IEnumerator SkillCoolTimeCoroutine()
    {
        isCoolTime = true;
        while (true)
        {
            _time += Time.deltaTime;
            if(_time >= _skillCoolTime)
            {
                isCoolTime = false;
                _time = 0;
                StopCoroutine("SkillCoolTimeCoroutine");
            }
            yield return null;
        }
    }
    IEnumerator SkillCoolTimeCoroutine2()
    {
        isCoolTime2 = true;
        while (true)
        {
            _time2 += Time.deltaTime;
            if (_time2 >= _skillCoolTime2)
            {
                isCoolTime2 = false;
                _time2 = 0;
                StopCoroutine("SkillCoolTimeCoroutine2");
            }
            yield return null;
        }
    }
}
