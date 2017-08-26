using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Floor {
    public override void Touched(MovableObject who)
    {
        who.Move(who.CurrentPos+who.MoveDir,false);
    }
}
