using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieState
{
    void EnterState(ZomManager zombie);
    void UpdateState(ZomManager zombie);
    void ExitState(ZomManager zombie);
}
