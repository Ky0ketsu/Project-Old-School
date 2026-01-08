using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerActionService
{
    public void Slap();
    public void EnterTabletView();
    public void ExitTabletView();
    public void SetSlapAction(SlapAction slap);
    public void SetTabletAction(TabletAction tablet);
}
