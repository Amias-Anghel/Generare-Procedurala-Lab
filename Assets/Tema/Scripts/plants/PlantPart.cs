using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Used to generate a procedural plant.
    EDITOR ONLY
    Used bt Procedural Plant script
 */
public class PlantPart : MonoBehaviour
{
    [SerializeField] private Transform nextRoot;

    public Vector3 ApplyTransform(float len, float rotate, Transform parent) {
        Vector3 newScale = transform.localScale;
        newScale.y *= len;
        transform.localScale = newScale;

        Vector3 newPos = transform.position;
        newPos.y += newScale.y/Mathf.Pow(2, 10);

        transform.position = newPos;

        transform.Rotate(rotate, 0, 0);

        Vector3 nextroot = nextRoot.position;

        
        transform.GetChild(0).SetParent(parent);
        DestroyImmediate(nextRoot.gameObject);

        return nextroot;
    }
}
