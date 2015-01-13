using UnityEngine;
using System.Collections;
using System;
using System.Globalization;
public static class EExtension 
{
    //Unity color to hex
    /// <summary>
    /// Convert Unity3d Color32 to Color Hex
    /// </summary>
    public static string ToHex(this Color32 color)
    {
        //Unity colors goes from 0-1 but we want 0 to 255(Hex goes from 0 to 255(00 to FF) ) so colors * will make it go from 0 to 255
        //X2 means hex size is 2! so 0 is 00 and not 0
        byte r = color.r;
        byte g = color.g;
        byte b = color.b;
        byte a = color.a;
        return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + a.ToString("X2");
    }
    //Unity color to hex
    /// <summary>
    /// Convert Unity3d Color to Color Hex
    /// </summary>
    public static string ToHex(this Color color)
    {
        //Unity colors goes from 0-1 but we want 0 to 255(Hex goes from 0 to 255(00 to FF) ) so colors * will make it go from 0 to 255
        //X2 means hex size is 2! so 0 is 00 and not 0
        int r = (int)(color.r * 255);
        int g = (int)(color.g * 255);
        int b = (int)(color.b * 255);
        int a = (int)(color.a * 255);
        return "#" + Mathf.FloorToInt(r).ToString("X2") + Mathf.FloorToInt(g).ToString("X2") + Mathf.FloorToInt(b).ToString("X2") + Mathf.FloorToInt(a).ToString("X2");
    }

    /// <summary>
    /// Convert Color Hex(#RRGGBBAA) to Unity3d Color32!
    /// </summary> 
    public static void HexToColor32(this Color32 color32,string hex)
    {
        hex = hex.Replace("#", string.Empty);//We don't need #
        if (hex.Length != 8)// The length should be 8 !
        {
            throw new Exception("Are you missing transparancy? Please add it it should look like #RRGGBBAA");
        }
        //Convert hex to int
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);//01
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);//23
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);//45
        byte a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);//67

        //Unity wants 0 - 1 so we devide it by 255.
        color32 = new Color32(r, g, b, a);
    }

    /// <summary>
    /// Convert Color Hex(#RRGGBBAA) to Unity3d Color!
    /// </summary> 
    public static void HexToColor(this Color color, string hex)
    {
        hex = hex.Replace("#", string.Empty);//We don't need #
        if (hex.Length != 8)// The length should be 8 !
        {
            throw new Exception("Are you missing transparancy? Please add it it should look like #RRGGBBAA");
        }
        //Convert hex to int
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);//01
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);//23
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);//45
        byte a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);//67

        color = new Color(r, g, b, a);
    }
}
