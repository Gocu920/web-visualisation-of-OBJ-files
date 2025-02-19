using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoom;
    float zoomMultiplier = 24f;
    float minZoom = 0f;
    float maxZoom = 192f;
    float velocity = 0f;
    float smoothTime = 0.1f;
    
    void Start()
    {
        zoom = Camera.main.orthographicSize * Camera.main.orthographicSize;
    }
    void Update()
    { 
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom -= scroll * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoom, ref velocity, smoothTime);
    }
}
