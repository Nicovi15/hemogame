using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectDescription", order = 1)]
public class ObjectDescription : ScriptableObject
{
    public string nameObject;

    public bool isDescribed;

    [TextArea(0, 3)]
    public string description;

    public bool valid;
}