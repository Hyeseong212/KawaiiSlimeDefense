using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoolTime : MonoBehaviour
{
    public float _skillCoolTime;
    float _time = 0;
    public bool isCoolTime;
    public void SkillCoolTimeCoroutineTrigger()
    {
        StartCoroutine("SkillCoolTimeCoroutine");
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
}
