using UnityEngine;

public class CarAssembler : MonoBehaviour
{
    public delegate void carDelegate(bool enabled);
    public static carDelegate carEnabled;

    [SerializeField]
    private GameObject mask;
    [SerializeField]
    private GameObject[] wheels;

    private void OnEnable() {
        carEnabled(true);
        
        CarAssemblerSlider.sliderValueChanged += MoveMask;
        CarAssemblerSlider.sliderValueChanged += MoveWheels;
    }
    private void OnDisable() {
        carEnabled(false);
        
        CarAssemblerSlider.sliderValueChanged -= MoveMask;
        CarAssemblerSlider.sliderValueChanged -= MoveWheels;
    }


    private void MoveMask(float distance) {
        mask.transform.localPosition = new Vector3(mask.transform.localPosition.x, mask.transform.localPosition.y + distance, mask.transform.localPosition.z);
    }

    private void MoveWheels(float distance) {
        
        foreach (GameObject wheel in wheels) {

            int direction = GetWheelMovementDirection(wheel);
            wheel.transform.localPosition = new Vector3(wheel.transform.localPosition.x + direction * distance, 
                wheel.transform.localPosition.y, wheel.transform.localPosition.z);
        }
    }

    private int GetWheelMovementDirection(GameObject wheel) {
        
        Side sideOfWheel = wheel.GetComponent<Wheel>().side;
        int direction = sideOfWheel == Side.Left ? -1 : 1;
        
        return direction;
    }
}
