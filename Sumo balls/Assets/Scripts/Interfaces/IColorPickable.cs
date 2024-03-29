using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IColorPickable
{
    public event ColorPickedEventHandler OnColorPicked;

    public delegate void ColorPickedEventHandler(Color color, IColorPickable caller);
}
