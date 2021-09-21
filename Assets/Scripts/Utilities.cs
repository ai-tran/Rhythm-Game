using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static bool InRange(float val,float min, float max)
    {
        if(val < max && val > min)
        {
            return true;
        }
        return false;
    }
    public static bool InRange(float val, float min, float max, bool inclusiveMin, bool inclusiveMax)
    {
        if (inclusiveMax && inclusiveMax)
        {
            if (val <= max && val >= min)
            {
                return true;
            }
        }

        if (inclusiveMin)
        {
            if (val < max && val >= min)
            {
                return true;
            }
        }

        if (inclusiveMax)
        {
            if (val <= max && val > min)
            {
                return true;
            }
        }
        return false;
    }
}
