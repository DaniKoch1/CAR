using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementManager : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    
    [SerializeField]
    private GameObject carModel;
    [SerializeField]
    private GameObject carPlacementRoot;
    [SerializeField]
    private GameObject stain;

    private List<ARRaycastHit> hits;
    
    private Vector2 touchPosition;

    private bool isCarPlaced;

    private void Awake() {
        arRaycastManager = GetComponent<ARRaycastManager>();
        hits = new List<ARRaycastHit>();
        isCarPlaced = false;
    }

    private void Update() {
        
        if(Input.touchCount > 0) {
            
            if(!isCarPlaced) {
                
                touchPosition = Input.GetTouch(0).position;
                TryPlaceCar();
                
                if(isCarPlaced) {
                    DisableGroundPlaneVisibility();
                }
            }
            else {
                TryPlaceStain();
            }
        }
    }

    private void TryPlaceCar() {
        
        if(Input.GetTouch(0).phase == TouchPhase.Ended) {
            
            if(DidTouchHitTarget(TrackableType.PlaneWithinPolygon)) {
                
                Pose hitPose = hits[0].pose;
                PlaceObject(hitPose, carModel, carPlacementRoot);
                isCarPlaced = true;
            }
        }
    }

    private void TryPlaceStain() {
        
        foreach(Touch touch in Input.touches) {
            
            if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
                
                touchPosition = touch.position;
                
                if(DidTouchHitTarget(TrackableType.Planes)) {
                    
                    foreach (ARRaycastHit hit in hits) {
                        
                        Pose hitPose = hit.pose;
                        PlaceObject(hitPose, stain);
                    }
                }
            }
        }
    }

    private bool DidTouchHitTarget(TrackableType target) {
        return arRaycastManager.Raycast(touchPosition, hits, target);
    }
    
    private GameObject PlaceObject(Pose hitPose, GameObject objectToPlace){
        
        Vector3 position = hitPose.position;
        Quaternion orientation = hitPose.rotation;
        
        GameObject placedObject = Instantiate(objectToPlace, position, orientation);
        
        return placedObject;
    }
    
    private void PlaceObject(Pose hitPose, GameObject objectToPlace, GameObject objectsRoot) {
        GameObject placedObject = PlaceObject(hitPose, objectToPlace);
        placedObject.transform.parent = objectsRoot.transform;
    }

    private void DisableGroundPlaneVisibility() {
        
        GetComponent<ARPlaneManager>().enabled = false;
        
        foreach(GameObject plane in GameObject.FindGameObjectsWithTag("PlaneVisualizer")) {
            
            Renderer renderer = plane.GetComponent<Renderer>();
            ARPlaneMeshVisualizer visualizer = plane.GetComponent<ARPlaneMeshVisualizer>();
            
            renderer.enabled = false;
            visualizer.enabled = false;
        }
    }
    
}
