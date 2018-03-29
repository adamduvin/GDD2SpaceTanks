using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFuelBar : MonoBehaviour {
    
    public GameObject healthBar;
    public GameObject healthEndCap;
    public float scaleHealth;
    public Vector3 newScaleHealth;
    public float initialScaleHealth;
    public Vector3 positionHealth;

    public GameObject fuelBar;
    public GameObject fuelEndCap;
    public float scaleFuel;
    public Vector3 newScaleFuel;
    public float initialScaleFuel;
    public Vector3 positionFuel;
    public Vector3 positionFuelStatic;

    // Use this for initialization
    void Start () {
        healthBar = transform.Find("HealthBar").gameObject;
        healthEndCap = transform.Find("HealthEnd").gameObject;
        fuelBar = transform.Find("FuelBar").gameObject;
        fuelEndCap = transform.Find("FuelEnd").gameObject;

        newScaleHealth = healthBar.transform.localScale;
        scaleHealth = newScaleHealth.x * 0.33f;
        initialScaleHealth = healthBar.transform.localScale.x;
        positionHealth = healthBar.transform.position;

        newScaleFuel = fuelBar.transform.localScale;
        initialScaleFuel = fuelBar.transform.localScale.x;
        positionFuel = fuelBar.transform.position;
        positionFuelStatic = positionFuel;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // This is janky, don't do this. I just didn't have time
    public void Decrement(string barType, float fuelDecrement)
    {
        if(barType == "health")
        {
            if (newScaleHealth.x == initialScaleHealth)
            {
                GameObject.Destroy(healthEndCap);
            }
            newScaleHealth.x -= scaleHealth;
            healthBar.transform.localScale = newScaleHealth;
            positionHealth.x -= scaleHealth / 2;
            healthBar.transform.position = positionHealth;
        }
        
        if(barType == "fuel")
        {
            scaleFuel = fuelDecrement * 2.7f;
            if (newScaleFuel.x == initialScaleFuel)
            {
                fuelEndCap.SetActive(false);
            }
            newScaleFuel.x -= scaleFuel;
            fuelBar.transform.localScale = newScaleFuel;
            positionFuel.x -= scaleFuel / 2;
            fuelBar.transform.position = positionFuel;
        }
    }
}
