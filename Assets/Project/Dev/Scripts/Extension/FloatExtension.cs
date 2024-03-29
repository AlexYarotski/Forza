﻿using UnityEngine;

public static class FloatExtension
{
    public static bool AlmostEquals(this float fl, float target, float value)
    {
        return Mathf.Abs(target - value) < Mathf.Epsilon;
    }
}
