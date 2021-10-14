using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static float Map(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (from - toMin) / (fromMax - fromMin) * (toMax - toMin) + fromMin;
    }

}
