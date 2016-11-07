using System;
using System.Collections.Generic;


public interface IUIPickableComponent
{
    PickableObject getPickableObject();
    void setPickableObject(PickableObject obj);

    void ActualizeViewOfField();
}

