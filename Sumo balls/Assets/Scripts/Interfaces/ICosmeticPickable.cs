using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICosmeticPickable
{
    public event CosmeticPickedEventHandler OnCosmeticPicked;

    public delegate void CosmeticPickedEventHandler(CosmeticSO cosmetic);
}
