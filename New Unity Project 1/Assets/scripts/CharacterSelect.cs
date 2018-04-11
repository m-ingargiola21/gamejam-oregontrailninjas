using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {

    public static CharacterSelect instance;



#region Test Variables
    public string hello = "I'm a singlton and I'm proud";
    
#endregion

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
        print(Input.GetJoystickNames()[i].ToString());

        }
    }

}
