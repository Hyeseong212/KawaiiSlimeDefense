using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GSceneManager : MonoSingleton<GSceneManager>
{

    public enum SCENE_TYPE
    {
        Login,
        MainMenu,
        MapSelect,
        Game,

        Max,
    }
    public SCENE_TYPE scene_type = SCENE_TYPE.Login;

    Stack<SCENE_TYPE> stackScene = new Stack<SCENE_TYPE>();
    // Use this for initialization

    public void MoveScene(SCENE_TYPE scene)
    {
        stackScene.Push(scene_type);
        scene_type = scene;
        SceneManager.LoadScene(scene_type.ToString());
    }
    public void NextScene()
    {
        SCENE_TYPE next = scene_type + 1;
        Debug.Log("NextScene " + next);
        if (next < SCENE_TYPE.Max)
        {
            MoveScene(next);
        }
    }

    public void AddScene(SCENE_TYPE scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }

    public void RemoveScene(SCENE_TYPE scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }

    public void BackScene()
    {
        if (stackScene.Count == 0)
        {
            if (SCENE_TYPE.Login < scene_type)
            {
                MoveScene(scene_type - 1);
            }
            return;
        }
        scene_type = stackScene.Pop();
        SceneManager.LoadScene(scene_type.ToString());
    }
    public void MoveSceneAsync(SCENE_TYPE scene)
    {
        stackScene.Push(scene_type);
        scene_type = scene;
        StartCoroutine("coroutineLoadScene");
    }
    public void NextSceneAsync()
    {
        SCENE_TYPE next = scene_type + 1;
        Debug.Log("NextSceneAsync " + next);
        if (next < SCENE_TYPE.Max)
        {
            MoveSceneAsync(next);
        }
    }
    public void BackSceneAsync()
    {
        if (stackScene.Count == 0)
        {
            if (SCENE_TYPE.Login < scene_type)
            {
                MoveSceneAsync(scene_type - 1);
            }
            return;
        }
        scene_type = stackScene.Pop();
        StartCoroutine("coroutineLoadScene");
    }

    IEnumerator coroutineLoadScene()
    {
        //추후 이동씬으로 대체
        PopupManager.i.ShowPopup(_type.E_POPUP.POPUP_LOADING, false, true);

        yield return null;

        //최소 대기 시간
        float waitetime = 1f;

        AsyncOperation async = SceneManager.LoadSceneAsync(scene_type.ToString());
        async.allowSceneActivation = false;

        float time = Time.realtimeSinceStartup + waitetime;
        while (async.isDone == false)
        {
            if (async.progress >= 0.9f && Time.realtimeSinceStartup >= time)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
