using System.Collections;
using System;
using UnityEngine;
using TMPro;

namespace genaralskar
{
    /// <summary>
    /// CarSelector is used to update the currently selected car and it's armor. Used when selecting a car in game.
    /// </summary>
    public class CarSelector : MonoBehaviour
    {
        [Tooltip("Array of all cars you want to be selectable")]
        public Car[] cars;

        [Tooltip("Object to parent car to\nLeave blank for not parent")]
        public Transform parentObject;
        
        private int currentCarIndex = 0;
        private int currentArmorIndex;

        private Car currentCar;
        private Armor currentArmor;

        [Header("Car Selection UI")]
        public TextMeshProUGUI carNameText;
        public TextMeshProUGUI carNameFlavorText;

        [Header("Armor Selection UI")]
        public TextMeshProUGUI armorNameText;
        public TextMeshProUGUI armorNameFlavorText;


        private GameObject currentCarObj;
        private GameObject currentArmorObj;

        private void Start()
        {
            UpdateCar(cars[0]);
        }

        #region Car Selection Functions
        //----====Car Selection Functions====----\\

        public void NextCar()
        {
            //++current car index
            currentCarIndex = (currentCarIndex + 1) % (cars.Length);
            //Debug.Log(currentCarIndex);

            //update car
            UpdateCar(cars[currentCarIndex]);
        }

        public void PreviousCar()
        {
            //--current car index
            currentCarIndex = (Mathf.Abs(currentCarIndex - 1)) % (cars.Length);

            //update car
            UpdateCar(cars[currentCarIndex]);
        }

        public void NextArmor()
        {
            //++current armor index
            currentArmorIndex = (currentArmorIndex + 1) % (currentCar.armors.Length);

            //update armor
            UpdateArmor(currentCar.armors[currentArmorIndex]);
        }

        public void PreviousArmor()
        {
            //--current armor index
            currentArmorIndex = (Mathf.Abs(currentArmorIndex - 1)) % (currentCar.armors.Length);
            UpdateArmor(currentCar.armors[currentArmorIndex]);
        }
        #endregion



        #region Update Car Functions
        //----====Update Car Functions====----\\

        public void UpdateCar(Car newCar)
        {
            //destroy current car
            if(currentCarObj != null)
            {
                Destroy(currentCarObj);
            }

            //destory current armor
            //Destroy(currentArmorObj);

            //set current car
            currentCar = newCar;
            
            //spawn new car at index
            currentCarObj = Instantiate(newCar.carPrefab);
            if (parentObject != null)
            {
                currentCarObj.transform.SetParent(parentObject);
                currentCarObj.transform.localPosition = Vector3.zero;
                currentCarObj.transform.localRotation = Quaternion.identity;
            }

            //if armor index != 0, reset index
            if (currentArmorIndex != 0) currentArmorIndex = 0;

            //update armor
            UpdateArmor(currentCar.armors[0]);

            //update text fields
            UpdateCarTextFields();
        }

        public void UpdateArmor(Armor newArmor)
        {
            //destory previous armor
            if(currentArmorObj != null)
            {
                Destroy(currentArmorObj);
            }

            //set current armor
            currentArmor = newArmor;
            
            //spawn new armor
            //probably set parent to car, then don't have to destory on car change
            currentArmorObj = Instantiate(newArmor.armorPrefab);
            
            
            currentArmorObj.transform.SetParent(currentCarObj.transform);
            currentArmorObj.transform.localPosition = Vector3.zero;
            currentArmorObj.transform.localRotation = Quaternion.identity;
            

            //update text fields
            UpdateArmorTextFields();
        }
        #endregion



        #region Update Text Functions
        private void UpdateCarTextFields()
        {
            carNameText.text = currentCar.carName;
            carNameFlavorText.text = currentCar.carFlavor;
        }

        private void UpdateArmorTextFields()
        {
            armorNameText.text = currentArmor.armorName;
            armorNameFlavorText.text = currentArmor.armorFlavor;
        }
        #endregion
    }
}