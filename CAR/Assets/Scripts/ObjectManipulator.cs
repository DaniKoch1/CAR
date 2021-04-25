using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    private GameObject selectedObject;
//    [SerializeField]
//    private GameObject manipulatorObject;
    [SerializeField]
    private Camera arCamera;
    private float rotationSpeed = 0.2f;
    private Vector2[] oldPositions;
    private bool wasScaledLastFrame;
    private float scaleSpeed = 0.001f;
    
    void Awake()
    {
        arCamera = FindObjectOfType<Camera>();
        //manipulatorObject.SetActive(false);
    }

    void Update()
    {
        if(Input.touches.Length == 0)
            return;
        
        if(Input.touches.Length == 1) {
            
            Touch touch = Input.GetTouch(0);
            Ray ray = arCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit)){
                if(touch.phase == TouchPhase.Began){
                    Debug.Log("***Touch began with tag " + hit.collider.gameObject.tag);
                    if(hit.collider.gameObject.CompareTag("CarForImage")){
                        selectedObject = hit.collider.gameObject;
                        Debug.Log("***Selected object assigned***");
//                        manipulatorObject.transform.position = selectedObject.transform.position;
//                        manipulatorObject.SetActive(true);
                    }
                }
                if(touch.phase == TouchPhase.Moved && selectedObject != null){
                    Debug.Log("***Touch moved***");
                    var rotation = Quaternion.Euler(touch.deltaPosition.y * rotationSpeed, -touch.deltaPosition.x * rotationSpeed, 0);
                    selectedObject.transform.rotation = rotation * selectedObject.transform.rotation;
                }
            }
        }
        if(Input.touches.Length == 2){
           // Debug.Log("***Two touches");
            Touch[] touches = Input.touches;
            
            if(touches[0].phase == TouchPhase.Moved && touches[1].phase == TouchPhase.Moved && selectedObject != null){
                
                Vector2[] newPositions = new Vector2[2]{touches[0].position, touches[1].position};
                
                if(!wasScaledLastFrame){
                    oldPositions = newPositions;
                    wasScaledLastFrame = true;
                }
                else{
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(oldPositions[0], oldPositions[1]);
                    float offset = newDistance - oldDistance;
                    ScaleObject(selectedObject.transform, offset);
                    oldPositions = newPositions;
                }
            }
            else
                wasScaledLastFrame = false;
        }
    }
    private void ScaleObject(Transform _transform, float _offset){
        _transform.localScale += _transform.localScale * _offset * scaleSpeed;
    }
}
