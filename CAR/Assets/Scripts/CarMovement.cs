using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CarMovement : MonoBehaviour
{
   private Transform faceCenter;
    
   private float angularSpeed = 1;
   private float circleRad;
 
   private Vector2 fixedPoint;
   private float currentAngle;
 
    private void Awake() {
        Debug.Log("***Awake in CarMovement with " + transform.GetComponentInParent<ARFace>() + "***");
        faceCenter = transform.GetComponentInParent<ARFace>().transform;
        fixedPoint = faceCenter.position;
        circleRad = Vector2.Distance(fixedPoint, transform.position);
    }
 
   void Update () {
       currentAngle += angularSpeed * Time.deltaTime;
       Vector2 offset = new Vector2 (Mathf.Sin (currentAngle), Mathf.Cos (currentAngle)) * circleRad;
       transform.localPosition = offset + fixedPoint;
       transform.rotation = Quaternion.Euler(currentAngle * Mathf.Rad2Deg, 90, 0);
   }



}
