using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        _camera.transform.position = new Vector3(x,y,-10);
    }
}
