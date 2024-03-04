using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Be
{
    public GameObject obj;
    public BeType type;

    public Be(GameObject beObject, BeType type)
    {
        this.obj = beObject;
        this.type = type;
    }
}

public enum BeType
{
    IS,
    ARE
}