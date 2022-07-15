using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FinSeq/FinSeqSVT")]
public class FinSeqSVT : ScriptableObject
{
    [SerializeField]
    public string binome;

    [SerializeField]
    public bool blesserBinome;

    [SerializeField]
    public bool blesserTom;
}
