using UnityEngine;
using UnityEditor;
using System.Collections;

public class SndMngrWindow : EditorWindow 
{
    private string audioName = "NamedAudioSource(Fill In a name please)";
    private bool looping = false;
    private AudioClip audioClip;
    private SoundManager.SoundTypes audioType;
    [MenuItem("Eddy Tools/Sound Manager")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SndMngrWindow),true,"Sound Manager Tool");
        SoundManager.Load();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("Test Volumes");
            EditorGUILayout.HelpBox("You can play around with the volumes here. Be aware the volumes won't be same as the build(if put on another computer)", MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            {
                SoundManager.MUSIC_VOLUME = EditorGUILayout.Slider("Music:", SoundManager.MUSIC_VOLUME, 0, 1);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            {
                SoundManager.EFFECT_VOLUME = EditorGUILayout.Slider("Effect:", SoundManager.EFFECT_VOLUME, 0, 1);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            {
                SoundManager.VOICE_VOLUME = EditorGUILayout.Slider("Voice:",SoundManager.VOICE_VOLUME, 0, 1);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                SoundManager.AMBIENT_VOLUME = EditorGUILayout.Slider("Ambient:",SoundManager.AMBIENT_VOLUME, 0, 1);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.HelpBox("Add a named audio source ", MessageType.Info);
            EditorGUILayout.LabelField("Add a sound!");

            audioName = EditorGUILayout.TextField("Name:", audioName);
            audioClip = EditorGUILayout.ObjectField("AudioClip:", audioClip, typeof(AudioClip)) as AudioClip;
            looping = EditorGUILayout.Toggle("Looping:", looping);
            audioType = (SoundManager.SoundTypes)EditorGUILayout.EnumPopup("Audio Type:", audioType);
            if (GUILayout.Button("Add Sound"))
            {
                GameObject newGameObject = new GameObject(audioName);
                NamedAudioSource namedAudioSource = newGameObject.AddComponent<NamedAudioSource>();
                AudioSource audioSource = newGameObject.GetComponent<AudioSource>();
                audioSource.clip = audioClip;
                namedAudioSource._type = audioType;
            }
        }
        EditorGUILayout.EndVertical();
    }
}
