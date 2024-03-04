using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter
{
    public GameObject obj;
    public char name;

    public Letter(GameObject letterObject, char name)
    {
        this.obj = letterObject;
        this.name = name;
    }
}
