using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
public class Placement : MonoBehaviour
{
    public GameObject beakerToPlace;
    public GameObject placedBeaker;
    private ARPlaneManager _arPlaneManager;
    private ARRaycastManager _arRaycastManager;
    //private Liquid _liquid;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    //public TextMeshProUGUI pos;
    private void Awake()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
        _arRaycastManager = GetComponent<ARRaycastManager>();
        //_liquid = GetComponent<Liquid>();
    }
    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index == 0)
        {
            Place(finger);
            //FingerCoords(finger);
        }
    }

    // private void FingerCoords(EnhancedTouch.Finger finger)
    // {
           //pos.text = finger.currentTouch.screenPosition.ToString();
    // }
    private void Place(EnhancedTouch.Finger finger)
    {
        if (finger.currentTouch.screenPosition.y < 600)
            return;
        
        if (_arRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            if (placedBeaker == null)
            {
                placedBeaker = Instantiate(beakerToPlace, pose.position, pose.rotation);
                //_liquid.liquid = placedBeaker.transform.GetChild(0);
                //_liquid.material = _liquid.liquid.GetComponent<MeshRenderer>();
            }
            else
            {
                placedBeaker.transform.position = pose.position;
                placedBeaker.transform.rotation = pose.rotation;
            }
        }
    }
}