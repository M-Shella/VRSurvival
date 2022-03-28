using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour {
    private static World _instance;
    
    public TextMeshPro clock;

    public Texture2D[] terrainTextures;
    
    public Transform sun;
    
    [HideInInspector]
    public Texture2DArray terrainTexArray;

    [SerializeField] private int _timeOfDay;
    public int day = 1;
    public int dayStartTime = 240; //4 * 60
    public int dayEndTime = 1320; //22 * 60
    private int dayLength { get { return dayEndTime - dayStartTime; } }
    private float sunRotationPerMinute { get { return 180f / dayLength; } }
    private float sunNightRotationPerMinute { get { return 180f / (1440 - dayLength); } }

    [Range(4f, 0f)] public float clockSpeed = 1f;
    public static World Instance { get { return _instance; } }

    public int TimeOfDay {
        get { return _timeOfDay;}
        set {
            _timeOfDay = value;
            if (_timeOfDay > 1440) {
                _timeOfDay = 0;
                day++;
            }

            float rotationAmount;
            if (_timeOfDay > dayStartTime && _timeOfDay < dayEndTime) {
                rotationAmount = (_timeOfDay - dayStartTime) * sunRotationPerMinute;
            }else if (_timeOfDay >= dayEndTime) {
                rotationAmount = dayLength * sunRotationPerMinute;
                rotationAmount += ((_timeOfDay - dayStartTime - dayLength) * sunNightRotationPerMinute);
            }else {
                rotationAmount = dayLength * sunRotationPerMinute;
                rotationAmount += (1440 - dayEndTime) * sunNightRotationPerMinute;
                rotationAmount += _timeOfDay * sunNightRotationPerMinute;
            }
            UpdateClock();
            sun.eulerAngles = new Vector3(rotationAmount, 0f, 0f);

        }
    }
    private void UpdateClock () {

        int hours = TimeOfDay / 60;
        int minutes = TimeOfDay - (hours * 60);

        string dayText;

        dayText = day.ToString();

        clock.text = string.Format("DAY: {0} TIME: {1}:{2}", dayText + '\n', hours.ToString("D2"), minutes.ToString("D2"));

    }
    private void Awake() {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else _instance = this;

        PopulateTextureArray();
    }

    private float secondCounter = 0;

    public void Update() {
        secondCounter += Time.deltaTime;
        
        if (!(secondCounter > clockSpeed)) return;
        
        TimeOfDay++;
        secondCounter = 0;
    }

    void PopulateTextureArray() {

        terrainTexArray = new Texture2DArray(1024, 1024, terrainTextures.Length, TextureFormat.ARGB32, false);

        for (int i = 0; i < terrainTextures.Length; i++) {

            terrainTexArray.SetPixels(terrainTextures[i].GetPixels(0), i, 0);

        }

        terrainTexArray.Apply();

    }
}