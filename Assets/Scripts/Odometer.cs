using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Odometer : MonoBehaviour
{
    public CarController carController;
    public float speed;
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        speed = carController.getSpeed();
        this.textMeshPro.text= "Speed: "+ speed.ToString("F2")+ " Km/h";
    }
}
