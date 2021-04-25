using UnityEngine;
using UnityEngine.UI;

public class CarAssemblerSlider : MonoBehaviour
{
    public delegate void sliderDelegate(float value);
    public static sliderDelegate sliderValueChanged;
    
    [SerializeField]
    private Slider slider;

    private float previousSliderValue;
    private void OnEnable() {
        CarAssembler.carEnabled += ToggleActive;
    }

    private void OnDisable() {
        CarAssembler.carEnabled -= ToggleActive;
    }

    private void ToggleActive(bool enabled) {
        slider.gameObject.SetActive(enabled);
    }

    public void OnValueChanged() {
        sliderValueChanged(slider.value - previousSliderValue);
        previousSliderValue = slider.value;
    }
}
