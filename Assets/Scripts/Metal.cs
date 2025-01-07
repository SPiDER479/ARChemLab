using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
public class Metal : MonoBehaviour
{
    [SerializeField] private GameObject beakerToPlace;
    private GameObject placedBeaker;
    private ARPlaneManager _arPlaneManager;
    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [SerializeField] private Material material;
    [SerializeField] private GameObject liquid, rock;
    [SerializeField] private Chemical chemical;
    private void Awake()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
        _arRaycastManager = GetComponent<ARRaycastManager>();
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
            PlaceBeaker(finger);
    }
    private void PlaceBeaker(EnhancedTouch.Finger finger)
    {
        if (finger.currentTouch.screenPosition.y < 800)
            return;
        if (_arRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            if (placedBeaker == null)
            {
                placedBeaker = Instantiate(beakerToPlace, pose.position, pose.rotation);
                GetComponent<UI>().Popup("Use the menu below to add metal to the beaker");
            }
            else
            {
                placedBeaker.transform.position = pose.position;
                placedBeaker.transform.rotation = pose.rotation;
                placedBeaker.transform.rotation = Quaternion.Euler(placedBeaker.transform.rotation.x, 
                    placedBeaker.transform.rotation.y, placedBeaker.transform.rotation.z + 180);
                
                if (GameObject.FindGameObjectWithTag("Liquid") != null)
                    GameObject.FindGameObjectWithTag("Liquid").transform.position = pose.position 
                        + new Vector3(0, 0.5f, 0);
            }
        }
    }
    public void AddWater()
    {
        if (GameObject.FindGameObjectWithTag("Liquid") == null)
        {
            Instantiate(liquid, 
                GameObject.FindGameObjectWithTag("Beaker").transform.position + new Vector3(0, 0.5f, 0), 
                Quaternion.identity);
            material.color = chemical.color;
        }
    }
    public void DropMetal()
    {
        Instantiate(rock, GameObject.FindGameObjectWithTag("Beaker").transform.position + 
                          new Vector3(0, 3, -4), Quaternion.identity);
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}