using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMeterUI : MonoBehaviour
{
    public GameObject pinball;
    public float maxHeight = 144;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float heightValue = pinball.transform.position.y / maxHeight;

        slider.value = heightValue;
    }
}
