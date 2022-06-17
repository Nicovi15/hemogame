using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Binome/BinomeDissection")]
public class BinomeDissection : ScriptableObject
{
    public string nom;
    public Color couleur;
    public GameObject noteFleur1;
    public GameObject noteFleur2;
    public GameObject noteFleur3;

    public int gainMorale;
    public int perteMorale;

    public int gainPhysique;
    public int pertePhysique;

    public DialogueObject dialog1;
    public DialogueObject dialog2;
}
