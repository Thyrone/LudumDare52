using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

   /// [SerializeField]
    /*[EventRef]
    private string music = null;*/

    [SerializeField]
    EventReference music;

    public TimelineInfo timelineInfo = null;
    public GCHandle timelineHandle;

    private FMOD.Studio.EventInstance musicInstance;
    FMOD.Studio.EventInstance carInstance;
    FMOD.Studio.EventInstance guyInstance;
    FMOD.Studio.EventInstance watermelonInstance;
    FMOD.Studio.EventInstance cowInstance;
    FMOD.Studio.EventInstance treeInstance; 
    FMOD.Studio.EventInstance errorInstance; 
    FMOD.Studio.EventInstance buttonInstance; 
    FMOD.Studio.EventInstance particleInstance; 

    private FMOD.Studio.EVENT_CALLBACK beatCallback;

    public static int lastBeat=0;
    public static string lastMarkString=null;
    public static float lastTempo=0f;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate beatUpdated;

    public delegate void MarkerListenerDelegate();
    public static event MarkerListenerDelegate markerUpdated;

    public delegate void TempoListenerDelegate();
    public static event TempoListenerDelegate tempoUpdated;


    public class TimelineInfo
    {
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
        public float currentTempo =0f;
        public int currentPosition = 0;
    }

    private void Awake()
    {
        instance = this;
        /*
        if(!music.IsNull)
        {
            musicInstance = RuntimeManager.CreateInstance(music);
            musicInstance.start();
        }*/
    }
    public void PlayMainMusic()
    {
        Debug.Log("PlayMusic");
            
            musicInstance = RuntimeManager.CreateInstance(music);
            musicInstance.start();
            timelineInfo = new TimelineInfo();
            beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
            timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        
    }

    void Start()
    {
        if(!music.IsNull)
        {
            timelineInfo = new TimelineInfo();
            beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
            timelineHandle = GCHandle.Alloc(timelineInfo,GCHandleType.Pinned);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
            cowInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Cow");
            carInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Car");
            watermelonInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Watermelon");
            treeInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Tree");
            guyInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Guy");

            errorInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Error");
            buttonInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Button");
            particleInstance= FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Particle");
        }
        
    }

    private void Update()
    {
       // Debug.Log(timelineInfo.currentPosition);

        if(!music.IsNull)
        {
            if (lastTempo != timelineInfo.currentTempo)
            {
                lastTempo = timelineInfo.currentTempo;

                if (tempoUpdated != null)
                {
                    tempoUpdated();
                }
            }

            if (lastMarkString != timelineInfo.lastMarker)
            {
                lastMarkString = timelineInfo.lastMarker;

                if (markerUpdated != null)
                {
                    markerUpdated();
                }
            }

            if (lastBeat != timelineInfo.currentBeat)

            {
                lastBeat = timelineInfo.currentBeat;

                if (beatUpdated != null)
                {
                    beatUpdated();
                }
            }
        }
       
    }

    private void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        timelineHandle.Free();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
       GUILayout.Box($"Current Beat = {timelineInfo.currentBeat}, Last Marker = {(string)timelineInfo.lastMarker}");
    }
#endif

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type,IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);

        if(result!=FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }else if(timelineInfoPtr!= IntPtr.Zero)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch(type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                        timelineInfo.currentTempo = parameter.tempo;
                        timelineInfo.currentPosition = parameter.position;
                    }
                    break;

                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;

            }
        }
        return FMOD.RESULT.OK;
    }


    #region SFX

    public void PlayCar()
    {
        carInstance.start();
    }

    public void PlayCow()
    {
        cowInstance.start();
    }

    public void PlayTree()
    {
        treeInstance.start();
    }

    public void PlayGuy()
    {
        guyInstance.start();
    }

    public void PlayWatermelon()
    {
        watermelonInstance.start();
    }

    public void PlayError()
    {
        errorInstance.start();
    }

    public void PlayButton()
    {
        buttonInstance.start();
    }

    public void PlayParticleSnd()
    {
        particleInstance.start();
    }



    #endregion
}
