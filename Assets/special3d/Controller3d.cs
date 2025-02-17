using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller3d : MonoBehaviour
{
    public Texture2D tilemapTexture;
    public float rotateSpeed;
    public float maxRotation;

    float mouseX, mouseY;
    private void Start()
    {
        Texture2D tileTexture = new Texture2D(16, 16);
        tileTexture.SetPixels(tilemapTexture.GetPixels(1 * 16, 1 * 16, 16, 16));
        tileTexture.Apply();


        GetComponent<Renderer>().material.mainTexture = tileTexture;
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            RotateMaze();

        }
    }

    private void RotateMaze()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        float rotateX = mouseX * rotateSpeed;
        float rotateZ = mouseY * rotateSpeed;

        rotateX=Mathf.Clamp(rotateX, -maxRotation, maxRotation);
        rotateZ=Mathf.Clamp(rotateZ, -maxRotation, maxRotation);

        transform.Rotate(new Vector3(rotateZ, 0, -rotateX));
    }
}
