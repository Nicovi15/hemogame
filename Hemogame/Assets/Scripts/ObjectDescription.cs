using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectDescription", order = 1)]
public class ObjectDescription : ScriptableObject
{
    public string nameObject;

    public string categorie;

    public bool isDescribed;

    [TextArea(0, 3)]
    public string description;

    public bool valid;

    public bool isExplained;

    [TextArea(0, 3)]
    public string explication;

    public bool isBonGeste;

    [TextArea(0, 3)]
    public string bonGeste;

    public Vector3 inspecPos;
    public Vector3 inspecScale;
    public Vector3 inspecRot;

    public Vector3 holdingPos;
    public float holdingRadius;
    public Vector3 holdingRotPos;
    public float dropPosZ;
    public Vector3 holdingRot;
    public Vector3 holdingScale;

}