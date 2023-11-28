using System;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    [SerializeField] bool destroyOnTriggerEnter;
    [SerializeField] string tagFilter; 
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    
    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.GameObject.CompareTag(tagFilter)) return;
        onTriggerEnter.Invoke();
        if (destroyOnTriggerEnter)
        {
            destroyOnTriggerEnter(gameObject);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.GameObject.CompareTag(tagFilter)) return;
        onTriggerExit.Invoke();
    }
}
