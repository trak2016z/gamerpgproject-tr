using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    Sprite getImage();
}

public struct PickableObject
{
    public IPickable pickableObject;
    public int count;
}