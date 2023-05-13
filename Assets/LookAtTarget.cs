using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 axis = Vector3.up;

    void Update()
    {
        RotateChildren(transform);
    }

    void RotateChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Vector3 direction = target.position - child.position;
            Quaternion rotation = Quaternion.LookRotation(direction, axis);
            child.rotation = rotation;
            RotateChildren(child);
        }
    }
}