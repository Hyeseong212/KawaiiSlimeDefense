using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoSingleton<AuthManager>
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passField;


    void Awake()
    {
    }
   
}
