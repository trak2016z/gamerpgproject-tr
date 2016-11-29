using System;
using System.Collections.Generic;


public interface IUIPickableComponent
{
    PickableObject getPickableObject();
    bool setPickableObject(PickableObject obj);

    void ActualizeViewOfField();
}

