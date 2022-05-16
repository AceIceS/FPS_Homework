using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Player;

namespace FPS_Homework_Item
{

    public interface IPlayerInteractable
    {
        bool OnPlayerInteract(PlayerEntity playerEntity);

    }

}
