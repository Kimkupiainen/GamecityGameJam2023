using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public List<Transform> gameObjects;

    public Vector3 offset;
    private Vector3 velocity;
    public float smoothtime = 0.5f;
    public float minzoom = 61.4f;
    public float maxzoom = 30f;
    public float zoomlimiter = 50f;
    private Camera cam;
    public GameObject go;
    Transform cameratransform;
    private void Start()
    {
        cam = GetComponent<Camera>();
        cameratransform = cam.transform;
    }
    void LateUpdate()
    {
        if (gameObjects.Count == 0)
        {
            return;
        }
        Move();
        Zoomiers();
    }
    void Move()
    {
        Vector3 centerpoint = Getcenterpoint();
        Vector3 newposition = centerpoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newposition, ref velocity, smoothtime);
    }
    void Zoomiers()
    {
        float newZoom = Mathf.Lerp(maxzoom, minzoom, Getgreatestdistance()/zoomlimiter);
        cam.fieldOfView = newZoom;
    }
    float Getgreatestdistance()
    {
        var bounds = new Bounds(gameObjects[0].position, Vector3.zero);
        for (int i = 0; i < gameObjects.Count; i++)
        {
            bounds.Encapsulate(gameObjects[i].position);
        }
        return bounds.size.x;
    }

    Vector3 Getcenterpoint()
    {
        if (gameObjects.Count == 1)
        {
            return gameObjects[0].position;
        }

        var bounds = new Bounds(gameObjects[0].position, Vector3.zero);
        for (int i = 0; i<gameObjects.Count; i++)
        {
            bounds.Encapsulate(gameObjects[i].position);
        }
        return bounds.center;
    }

}