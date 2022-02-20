using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCorrection : MonoBehaviour
{
    [SerializeField]
    GameManager GM;

    /*
     * On recupère la liste des objets valide
     * on les parcourt en mettant à chaque fois à jour le titre, la description, les bon gestes, et le visuel en copiant l'objet à la bonne position en mettant le bon layer
     * bouton -> et <- pour se déplacer dans la liste et bouton "terminer" une fois que chaque objet a au moins été vu une fois
     * aussi mettre à jour le target de la cam et l'outline de l'objet concerné
     * dire si l'objet a correctement été identifié
     * 
     *
     *
     */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
