using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "FinSeq/FinSeqBallon")]
public class FinSeqBallon : ScriptableObject
{
    [SerializeField]
    public int fin;

    public enum Cas : int
    {
        PartEtNonBless = 0,
        PartEtBless = 1,
        NonPart = 2
    }
}
