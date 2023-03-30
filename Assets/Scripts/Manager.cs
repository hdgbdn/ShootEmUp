using ShotEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    protected void Awake()
    {
        GameEntry.RegisterManager(this);
    }
}
