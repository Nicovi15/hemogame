using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerFPMovement player);

    void Interact(PickObjects player);
    
}
