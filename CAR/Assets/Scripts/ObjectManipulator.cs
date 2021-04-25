using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    
    private GameObject selectedObject;
    
    private float rotationSpeed = 0.2f;
    private float scaleSpeed = 0.001f;
    
    private Vector2[] oldPositions;
    
    private bool wasScaledLastFrame;

    void Update() {
        if (Input.touches.Length == 0)
            return;

        if (Input.touches.Length == 1) {

            Touch touch = Input.GetTouch(0);
            
            Ray ray = arCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {

                if (touch.phase == TouchPhase.Began) {
                    selectedObject = hit.collider.gameObject;
                }

                if (touch.phase == TouchPhase.Moved && selectedObject != null) {
                    RotateObject(touch);
                }
            }
        }

        if (Input.touches.Length == 2) {

            Touch[] touches = Input.touches;
            
            if (touches[0].phase == TouchPhase.Moved && touches[1].phase == TouchPhase.Moved && selectedObject != null) {
                TryScaleObject(touches);
            }
            
            else {
                wasScaledLastFrame = false;
            }
        }
    }

    private void RotateObject(Touch touch) {
        
        float xRotation = touch.deltaPosition.y * rotationSpeed;
        float yRotation = -touch.deltaPosition.x * rotationSpeed;
        
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        
        selectedObject.transform.rotation *= rotation;
    }

    private void TryScaleObject(Touch[] touches) {
        
        Vector2[] newPositions = new Vector2[] {touches[0].position, touches[1].position};

        if (!wasScaledLastFrame) {
            wasScaledLastFrame = true;
        }
        
        else {
            float scaleFactor = GetScaleFactor(newPositions);
            selectedObject.transform.localScale += selectedObject.transform.localScale * scaleFactor * scaleSpeed;
        }
        
        oldPositions = newPositions;
    }

    private float GetScaleFactor(Vector2[] newPositions) {
        
        float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
        float oldDistance = Vector2.Distance(oldPositions[0], oldPositions[1]);
        float scaleFactor = newDistance - oldDistance;

        return scaleFactor;
    }
    
}
