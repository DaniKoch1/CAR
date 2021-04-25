using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageManager : MonoBehaviour
{
    private ARTrackedImageManager arTrackedImageManager;

    [SerializeField] 
    private GameObject carModel;
 
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
            InstantiateCar(image);
        }

        foreach (ARTrackedImage image in eventArgs.updated) {
            
            //If coming back to the same scene, the game object was destroyed, but the image might not have been removed
            if (!placedCars.ContainsKey(image.referenceImage.name)) {
                InstantiateCar(image);
            }
            else {
                placedCars[image.referenceImage.name].SetActive(image.trackingState.Equals(TrackingState.Tracking));
            }
        }

        foreach (ARTrackedImage image in eventArgs.removed) {
            Destroy(placedCars[image.referenceImage.name]);
        }
    }

    private void InstantiateCar(ARTrackedImage image) {
        
        GameObject car = Instantiate(carModel, Vector3.zero, Quaternion.Euler(0,90,90), image.transform);
        
        string color = GetCarColorFromImageName(image);
        AssignCarColor(car, color);
        
        placedCars.Add(image.referenceImage.name, car);
    }

    //The image name format is always carColor
    private string GetCarColorFromImageName(ARTrackedImage image) {
        
        string prefix = "car";
        string color = image.referenceImage.name.Substring(prefix.Length);
        
        return color;
    }

    private void AssignCarColor(GameObject car, string color) {
        
        GameObject mask = car.transform.GetChild(0).gameObject;
        
        switch (color) {
            case "Blue":
                mask.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
                break;
            case "Red":
                mask.GetComponent<MeshRenderer>().materials[0].color = Color.red;
                break;
            default:
                mask.GetComponent<MeshRenderer>().materials[0].color = Color.gray;
                break;
            
        }
    }
    
}