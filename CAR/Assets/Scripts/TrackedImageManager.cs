using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Debug = UnityEngine.Debug;

public class TrackedImageManager : MonoBehaviour
{
    private ARTrackedImageManager arTrackedImageManager;

    [SerializeField] 
    private GameObject carModel;

    [SerializeField] 
    private Material[] carMaterials;
 
    private Dictionary<string, GameObject> placedCars;

    private void Awake() {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        placedCars = new Dictionary<string, GameObject>();
    }
    
    private void OnEnable() {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }
    
    private void OnDisable() {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }
    
    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs) {

        foreach (ARTrackedImage image in eventArgs.added) {
            Debug.Log("***Image added with name: " + image.name + " and other name: " + image.referenceImage.name);
            InstantiateCar(image);
        }

        foreach (ARTrackedImage image in eventArgs.updated) {
            
            if (placedCars.Count == 0) {
                InstantiateCar(image);
            }
            else {
                placedCars[image.referenceImage.name].SetActive(image.trackingState == TrackingState.Tracking);
            }
        }

        foreach (ARTrackedImage image in eventArgs.removed) {
            Destroy(placedCars[image.referenceImage.name]);
        }
    }

    private void InstantiateCar(ARTrackedImage image) {
        
        string color = GetCarColorFromImageName(image);
        
        Debug.Log("***Color: " + color);

        GameObject car = Instantiate(carModel, Vector3.zero, Quaternion.Euler(0,90,90), image.transform);
        
        Debug.Log("***Instantiated***");
        
        AssignCarColor(car, color);
        
        Debug.Log("***Color assigned***");
        
        placedCars.Add(image.referenceImage.name, car);
        
        Debug.Log("***Added to list***");
    }

    private string GetCarColorFromImageName(ARTrackedImage image) {
        
        string prefix = "car";
        string color = image.referenceImage.name.Substring(prefix.Length);
        
        return color;
    }

    private void AssignCarColor(GameObject car, string color) {
        GameObject mask = car.transform.GetChild(0).gameObject;
        Debug.Log("***Mask: " + mask);
        
        //maybe as enum later with string and Color
        switch (color) {
            case "Blue":
                mask.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
                break;
            case "Red":
                mask.GetComponent<MeshRenderer>().materials[0].color = Color.red;
                break;
            case "Silver":
                mask.GetComponent<MeshRenderer>().materials[0].color = Color.gray;
                break;
        }
            
        
//        foreach (Material material in carMaterials) {
//            
//            Debug.Log("***material.name: " + material.name + "; color: " + color);
//            Debug.Log("***Contains: " + material.name.Contains(color));
//            if (material.name.Contains(color)) {
//                Debug.Log("***Material Before: " + mask.GetComponent<MeshRenderer>().materials[0]);
//                mask.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
//                Debug.Log("***Material After: " + mask.GetComponent<MeshRenderer>().materials[0]);
//                break;
//            }
//        }
    }
    
}
