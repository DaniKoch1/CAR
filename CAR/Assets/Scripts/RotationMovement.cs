using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RotationMovement : MonoBehaviour
{
   private Transform faceCenter;

   private float speed;
   private float radious;
   private float angle;
   
   private Quaternion rotation;
 
   private Vector2 center;
   private Vector2 offset;
 
    private void Awake() {
        
        faceCenter = transform.GetComponentInParent<ARFace>().transform;
        center = faceCenter.position;

        speed = 1;
        radious = Vector2.Distance(center, transform.position);
    }
 
   void FixedUpdate() {
       
       GetAngle();
       Move();
       Rotate();
   }

   private void GetAngle() {
       angle += speed * Time.deltaTime;
   }

   private void Move() {
       offset = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle)) * radious;
       transform.localPosition = offset + center;
   }

   private void Rotate() {
       rotation = Quaternion.Euler(angle * Mathf.Rad2Deg, 90, 0);
       transform.rotation = rotation;
   }




}
