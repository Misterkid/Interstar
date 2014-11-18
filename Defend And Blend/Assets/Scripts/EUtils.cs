using UnityEngine;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Linq;
using System.IO;
/* 
 * Author: Eduard Meivogel
 * Website: https://www.facebook.com/EddyMeivogelProjects
 * Creation Year: 2014
 * E for Eddy! 
 *
 */
/// <summary>
/// E for Eddy!
/// This is a class containing some awesome functions!
/// </summary> 
public static class EUtils
{
    //Unity color to hex
    /// <summary>
    /// Convert Unity3d Color32 to Color Hex
    /// </summary>
    ///
    public static string ColorToHex(Color32 color)
    {
        //Unity colors goes from 0-1 but we want 0 to 255(Hex goes from 0 to 255(00 to FF) ) so colors * will make it go from 0 to 255
        //X2 means hex size is 2! so 0 is 00 and not 0
        byte r = color.r;
        byte g = color.g;
        byte b = color.b;
        byte a = color.a;
        return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + a.ToString("X2");
    }
    
    public static string ColorToHex(Color color)
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
    public static Color32 HexToColor(string hex)
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
        return new Color32(r , g , b , a );
    }
    /// <summary>
    /// Put colors in your GUI text using this function!
    /// </summary> 
    public static String UnityColoredText(string text,Color32 color)
    {
        string retText = "<color=" + ColorToHex(color) + ">" + text + "</color>";
        return retText;
    }
    /// <summary>
    /// Put colors in your GUI text using this function!
    /// </summary> 
    public static String UnityColoredText(string text,string htmlHex)
    {
        string retText = "<color=" + htmlHex + ">" + text + "</color>";
        return retText;
    }
    /*
    public static Vector3 GetObjectUnitSize(GameObject gameObject)
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        Mesh objectMesh = null;
        if (meshFilter != null)
        {
            objectMesh = meshFilter.sharedMesh;
        }
        else if (objectMesh == null)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null)
            {
                objectMesh = skinnedMeshRenderer.sharedMesh;
                //Debug.Log(objectMesh.bounds);
            }
        }
        if (objectMesh != null)
            return new Vector3(objectMesh.bounds.size.x * gameObject.transform.localScale.x, objectMesh.bounds.size.y * gameObject.transform.localScale.y, objectMesh.bounds.size.z * gameObject.transform.localScale.z);
        else
        {
            Debug.LogError("No mesh found!");
           // return new Vector3(2, 2, 2);
            return Vector3.zero;
        }
    }
     * */
    public static Vector3 GetObjectCollUnitSize(GameObject gameObject)
    {
        Collider collider = gameObject.GetComponent<Collider>();
        if (collider != null)
            return collider.bounds.size;
        else
        {
            // return (new Vector3(2, 2, 2));
            Debug.LogError("No col mesh found!");
            return Vector3.zero;
        }
        //return new Vector3(collider.bounds.size.x * collider.transform.localScale.x, collider.bounds.size.y * collider.transform.localScale.y, collider.bounds.size.z * collider.transform.localScale.z);
    }
    
    /// <summary>
    /// Get the objects unitsize (scale is counted too!)
    /// </summary> 
    public static Vector3 GetObjectUnitSize(GameObject gameObject)
    {
        Mesh objectMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        return new Vector3(objectMesh.bounds.size.x * gameObject.transform.localScale.x, objectMesh.bounds.size.y * gameObject.transform.localScale.y, objectMesh.bounds.size.z * gameObject.transform.localScale.z);
    }


    /// <summary>
    /// Get the Sprite unitsize (scale is counted too!)
    /// </summary> 
    public static Vector2 GetSpriteUnitSize(GameObject gameObject)
    {
        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        return new Vector2(sprite.bounds.size.x * gameObject.transform.localScale.x, sprite.bounds.size.y * gameObject.transform.localScale.y);
    }
    /// <summary>
    /// Get the string size
    /// </summary> 
    public static Vector2 GetStringSize(string text, GUIStyle guiStyle = null)
    {
        GUIContent guiContent = new GUIContent(text);
        if (guiStyle == null)
        {
            guiStyle = new GUIStyle();
        }
        Vector2 size = new Vector2(guiStyle.CalcSize(guiContent).x, guiStyle.CalcSize(guiContent).y * 2);
        return size;
    }
    /// <summary>
    /// Get the nearest object of type
    /// </summary>
    public static GameObject GetNearestObjectOfType<T>(Vector3 from) where T:MonoBehaviour
    {
        MonoBehaviour[] scripts = GameObject.FindObjectsOfType(typeof(T)) as MonoBehaviour[];
        //Faster
        if (scripts.Length > 0)
        {
            GameObject nearestGameObject = null;//The nearest pickup object
            for (int i = 0; i < scripts.Length; i++)//Loop trough it
            {
                if (nearestGameObject != null)//not null? then we could look which pickup object is closer to move to!
                {
                    //distance check.
                    if (Vector3.Distance(nearestGameObject.transform.position, from) > Vector3.Distance(scripts[i].transform.position, from))
                    {
                        //set new nearest pickup object
                        nearestGameObject = scripts[i].gameObject;
                    }
                }
                else
                {
                    //Its null then I guess the next object in here is the closest.
                    nearestGameObject = scripts[i].gameObject;
                }
            }
            return nearestGameObject;//Return nearest Gameobject
        }

        return null;
    }
    /// <summary>
    /// Get the nearest object from list
    /// </summary>
    //    public static GameObject GetNearestObjectOfType<T>(Vector3 from) where T:MonoBehaviour
     public static GameObject GetNearestObject(List<GameObject> gameObjects,Vector3 from,GameObject skip = null)
    {
        //MonoBehaviour[] scripts = GameObject.FindObjectsOfType(typeof(T)) as MonoBehaviour[];
        //Faster
        if (gameObjects.Count > 0)
        {
            GameObject nearestGameObject = null;//The nearest pickup object
            for (int i = 0; i < gameObjects.Count; i++)//Loop trough it
            {
                if (nearestGameObject != null)//not null? then we could look which pickup object is closer to move to!
                {
                    //distance check.
                    if (Vector3.Distance(nearestGameObject.transform.position, from) > Vector3.Distance(gameObjects[i].transform.position, from))
                    {
                        //set new nearest pickup object
                        if (skip != gameObjects[i])
                            nearestGameObject = gameObjects[i].gameObject;
                    }
                }
                else
                {
                    //Its null then I guess the next object in here is the closest.
                    nearestGameObject = gameObjects[i].gameObject;
                }
            }
            return nearestGameObject;//Return nearest Gameobject
        }

        return null;
    }

    /// <summary>
    /// Get the Farrest object of type
    /// </summary>
    public static GameObject GetFarestObjectOfType<T>(Vector3 from) where T : MonoBehaviour
    {
        MonoBehaviour[] scripts = GameObject.FindObjectsOfType(typeof(T)) as MonoBehaviour[];
        //LinQ (It is slower!)
        //NEVER EVER USE THIS
        return scripts.OrderBy(go => Vector3.Distance(go.transform.position, from)).LastOrDefault().gameObject;
        //Didn't use it yet... So. Nothing to make it faster.
        //return null;
    }
    //Alternative Unity3d Logging.
    //Params? What is it? check here: http://msdn.microsoft.com/en-us/library/w5zay9db.aspx
    /// <summary>
    /// Alternative unity3d logging. Shorter then "Debug.Log(string.Format(text, args))"
    /// </summary> 
    public static void Log(String text,params object[] args)
    {
        Debug.Log(string.Format(text, args));
    }
    /// <summary>
    /// Alternative unity3d logging.
    /// </summary> 
    public static void Log(object message)
    {
        Debug.Log(message);
    }

    /// <summary>
    /// Alternative unity3d Error Logging. Shorter then "Debug.LogError(string.Format(text, args))"
    /// </summary> 
    public static void LogError(String text, params object[] args)
    {
        Debug.LogError(string.Format(text, args));
    }
    /// <summary>
    /// Alternative unity3d Error logging.
    /// </summary> 
    public static void LogError(object message)
    {
        Debug.LogError(message);
    }

    /// <summary>
    /// Alternative unity3d warning logging. Shorter then "Debug.LogWarning(string.Format(text, args))"
    /// </summary> 
    public static void LogWarning(String text, params object[] args)
    {
        Debug.LogWarning(string.Format(text, args));
    }
    /// <summary>
    /// Alternative unity3d warning logging.
    /// </summary> 
    public static void LogWarning(object message)
    {
        Debug.LogWarning(message);
    }
}
