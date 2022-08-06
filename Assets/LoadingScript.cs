using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LoadingScript : MonoBehaviour
{
    public Text _loadingTxt;
    float loadingTime;
    void Update()
    {
        loadingTime += Time.deltaTime;
        if(loadingTime > 0.5f && loadingTime < 1)
        {
            _loadingTxt.text = "Loading";
        }
        else if(loadingTime >= 1 && loadingTime <1.5f)
        {
            _loadingTxt.text = "Loading.";
        }
        else if (loadingTime >= 1.5f && loadingTime < 2)
        {
            _loadingTxt.text = "Loading..";
        }
        else if (loadingTime >= 2 && loadingTime < 2.5f)
        {
            _loadingTxt.text = "Loading...";
        }
        else if(loadingTime >= 2.5f)
        {
            _loadingTxt.text = "Loading....";
            loadingTime = 0;
        }
    }
}
