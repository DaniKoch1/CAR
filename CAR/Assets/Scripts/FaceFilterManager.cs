using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FaceFilterManager : MonoBehaviour
{
    private ARFaceManager arFaceManager;

    [SerializeField] 
    private GameObject carModel;

    private GameObject car;
    
//    void Awake()
//    {
//        arFaceManager = GetComponent<ARFaceManager>();
//    }
//    
//    private void OnEnable() {
//        arFaceManager.facesChanged += OnFaceChanged;
//    }
//    
//    private void OnDisable() {
//        arFaceManager.facesChanged -= OnFaceChanged;
//    }
//    
//    private void OnFaceChanged(ARFacesChangedEventArgs eventArgs) {
//        foreach (ARFace face in eventArgs.added) {
//            car = Instantiate(carModel, Vector3.zero, Quaternion.Euler(0,-90,0), face.transform);
//        }
//        foreach (ARFace face in eventArgs.updated) {
//            if (car == null) {
//                car = Instantiate(carModel, Vector3.zero, Quaternion.Euler(0,-90,0), face.transform);
//            }
//            else {
//                car.SetActive(face.trackingState == TrackingState.Tracking);
//            }
//        }
//
//        foreach (ARFace face in eventArgs.removed) {
//            Destroy(car);
//        }
//    }
}
