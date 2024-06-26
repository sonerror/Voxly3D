using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ultilities
{

    public static bool AreColorsApproximatelyEqual(Color color1, Color color2)
    {
        return (color1.r == color2.r) &&
               (color1.g == color2.g) &&
               (color1.b == color2.b) &&
               (color1.a == color2.a);
    }
    public static bool CheckColorsInList(List<Color> colors, Color color)
    {
        if (colors.Count == 0) return false;
        int i = 0;
        foreach (Color c in colors)
        {
            if (AreColorsApproximatelyEqual(c, color))
            {
                i++;
            }

        }
        return i >= 1;
    }

}