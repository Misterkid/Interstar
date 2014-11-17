using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

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
        //SoundManager.Instance.
       // musicVol = SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC];
        //SoundManager.Instance.Load();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("Test Volumes");
            EditorGUILayout.HelpBox("You can play around with the volumes here. Be aware the volumes won't be same as the build(if put on another computer)", MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            {
                SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC] = EditorGUILayout.Slider("Music:", SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC], 0, 1);
                SoundManager.Instance.ChangeVolume(SoundManager.Instance.soundValues[SoundManager.SoundTypes.MUSIC], SoundManager.SoundTypes.MUSIC);
                //SoundManager.Instance.MUSIC_VOLUME = EditorGUILayout.Slider("Music:", SoundManager.Instance.MUSIC_VOLUME, 0, 1);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            {

                SoundManager.Instance.soundValues[SoundManager.SoundTypes.EFFECT] = EditorGUILayout.Slider("Effect:", SoundManager.Instance.soundValues[SoundManager.SoundTypes.EFFECT], 0, 1);
                SoundManager.Instance.ChangeVolume(SoundManager.Instance.soundValues[SoundManager.SoundTypes.EFFECT], SoundManager.SoundTypes.EFFECT);
            
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            {
                SoundManager.Instance.soundValues[SoundManager.SoundTypes.VOICE] = EditorGUILayout.Slider("Voice:", SoundManager.Instance.soundValues[SoundManager.SoundTypes.VOICE], 0, 1);
                SoundManager.Instance.ChangeVolume(SoundManager.Instance.soundValues[SoundManager.SoundTypes.VOICE], SoundManager.SoundTypes.VOICE);

                // SoundManager.Instance.VOICE_VOLUME = EditorGUILayout.Slider("Voice:",SoundManager.Instance.VOICE_VOLUME, 0, 1);
            
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                SoundManager.Instance.soundValues[SoundManager.SoundTypes.AMBIENT] = EditorGUILayout.Slider("Ambient:", SoundManager.Instance.soundValues[SoundManager.SoundTypes.AMBIENT], 0, 1);
                SoundManager.Instance.ChangeVolume(SoundManager.Instance.soundValues[SoundManager.SoundTypes.AMBIENT], SoundManager.SoundTypes.AMBIENT);

                //SoundManager.Instance.AMBIENT_VOLUME = EditorGUILayout.Slider("Ambient:",SoundManager.Instance.AMBIENT_VOLUME, 0, 1);
            
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
