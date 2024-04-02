using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickableObject : MonoBehaviour
{
    public GameObject hairpinPrefab; 
    public Transform canvasTransform; 
    public GameObject targetUIObject; 

    public void OnClickDresser()
    {
        if (targetUIObject != null)
        {
          
            GameObject instantiatedHairpin = Instantiate(hairpinPrefab, canvasTransform);

          
            Vector3 relativePosition = targetUIObject.transform.position - canvasTransform.position;
            instantiatedHairpin.transform.localPosition = relativePosition;

            
        }
    }
}