using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CosmeticData
{
    public string cosmeticId;
    public List<Color> colors = new List<Color>();

    public CosmeticData(string cosmeticId, List<Color> colors)
    {
        this.cosmeticId = cosmeticId;
        this.colors = colors;
    }

}
