using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Player
{


    public class PlayerCompassElement : MonoBehaviour
    {
        [Tooltip("The marker on the compass for this element")]
        public PlayerCompassMarker CompassMarkerPrefab;

        [Tooltip("Text override for the marker, if it's a direction")]
        public string TextDirection;

        PlayerHUD mCompass;

        void Awake()
        {
            mCompass = FindObjectOfType<PlayerHUD>();

            var markerInstance = Instantiate(CompassMarkerPrefab);

            markerInstance.Initialize(this, TextDirection);
            mCompass.RegisterCompassElement(transform, markerInstance);
        }

        void OnDestroy()
        {
            mCompass.UnregisterCompassElement(transform);
        }
    }

}
