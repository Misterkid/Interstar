using UnityEngine;
using System.Collections;

/* 
 * Author: Eduard Meivogel
 * Website: https://www.facebook.com/EddyMeivogelProjects
 * Creation Year: 2014
 *
 *
 */
public class GUIBase : MonoBehaviour 
{
    public bool activated = false;
    public GUISkin guiSkin;
    protected virtual void OnGUI()
    {
        if (guiSkin != null)
            GUI.skin = guiSkin;
    }
}