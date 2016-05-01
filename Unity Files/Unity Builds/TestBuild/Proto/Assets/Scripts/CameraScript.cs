using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    //The offset of the camera to centrate the player in the X axis
    public float offsetX = 0;
    //The offset of the camera to centrate the player in the Z axis
    public float offsetZ = -7;
    //The offset of the camera to centrate the player in the Y axis
    public float offsetY = 3;
    //The maximum distance permited to the camera to be far from the player, its used to     make a smooth movement
    public float maximumDistance = 2;
    //The velocity of your player, used to determine que speed of the camera
    public float playerVelocity = 10;
    // Hight of character
    public float heroHeight = 2.0f;
    public float minHieght = 4; // for zoom in and out
    public float maxHieght = 10;

    public float minDistance = -2.5f;
    public float maxDistance = -8;
    private float _movementX;
    private float _movementZ;
    private float _movementY;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        var hiddenObjects = new Dictionary<Transform, Shader>();
    }


    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        _movementX = ((player.transform.position.x + offsetX - this.transform.position.x)) / maximumDistance;
        _movementZ = ((player.transform.position.z + offsetZ - this.transform.position.z)) / maximumDistance;
        _movementY = ((player.transform.position.y + offsetY - this.transform.position.y)) / maximumDistance;

        // Next position of camera
        Vector3 targetPos = this.transform.position + new Vector3((_movementX * playerVelocity * Time.deltaTime),
                                                               _movementY,
                                                               (_movementZ * playerVelocity * Time.deltaTime));
        // Distance between old position and new position
        float distanceVec = Vector3.Distance(this.transform.position, targetPos);
        // Linearly interpolates between two vectors.
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, distanceVec);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // zoom out
        {
            if (offsetY < maxHieght)
            {
                offsetY += 0.3f;
            }
            if (offsetZ > maxDistance)
            {
                offsetZ -= 0.3f;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (offsetY > minHieght)
            {
                offsetY -= 0.3f;
            }
            if (offsetZ < minDistance)
            {
                offsetZ += 0.22f;
            }
        }

        }
    }
