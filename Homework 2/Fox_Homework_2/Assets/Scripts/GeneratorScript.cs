﻿/*
* Copyright (c) 2018 Razeware LLC
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* Notwithstanding the foregoing, you may not use, copy, modify, merge, publish,
* distribute, sublicense, create a derivative work, and/or sell copies of the
* Software in any work that is designed, intended, or marketed for pedagogical or
* instructional purposes related to programming, coding, application development,
* or information technology.  Permission for such use, copying, modification,
* merger, publication, distribution, sublicensing, creation of derivative works,
* or sale is expressly withheld.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private float screenWidthInPoints;

    public GameObject[] availableObjects;
    public List<GameObject> objects;

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;

    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;

    public float objectsMinRotation = -45.0f;
    public float objectsMaxRotation = 45.0f;
    // Because the long room (room3) is off if the room1 or room2 comes before it
    private int lastIndexRoom = 1;

    // Use this for initialization
    private void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void AddRoom(float farthestRoomEndX)
    {
        //1
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        //randomRoomIndex = 2;

        //2
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

        //3
        float roomWidth = room.transform.Find("floor").localScale.x;

        //4
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;
        // NOTE: I added this because the script wouldn't correctly position
        // room3 otherwise
        if (randomRoomIndex == 2 && (lastIndexRoom == 1 || lastIndexRoom == 0))
        {
            roomCenter = roomCenter - 4.8f;
            farthestRoomEndX = farthestRoomEndX - 4.8f;
        }
        if ((randomRoomIndex == 1 || randomRoomIndex == 0) && lastIndexRoom == 2)
        {
            roomCenter = roomCenter + 4.8f;
            farthestRoomEndX = farthestRoomEndX + 4.8f;
        }
        lastIndexRoom = randomRoomIndex;


        //5
        room.transform.position = new Vector3(roomCenter, 0, 0);


        //6
        currentRooms.Add(room);
    }

    public float farthestRoomEndX = 0;

    private void GenerateRoomIfRequired()
    {
        //1
        List<GameObject> roomsToRemove = new List<GameObject>();
        //2
        bool addRooms = true;
        //3
        float playerX = transform.position.x;
        //4
        float removeRoomX = playerX - screenWidthInPoints;
        //5
        float addRoomX = playerX + screenWidthInPoints;
        //6
        farthestRoomEndX = 0;
        foreach (var room in currentRooms)
        {
            //7
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;
            //8
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            //9
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            //10
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        //11
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            // This attempts to destroy a prefab which is not allowed
            // see https://answers.unity.com/questions/164283/destroying-assets-is-not-permitted-to-avoid-data-l.html
            Destroy(room);
        }
        //12
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            GenerateObjectsIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void AddObject(float lastObjectX)
    {
        //1
        int randomIndex = Random.Range(0, availableObjects.Length);

        //2
        GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);

        //3
        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(objectsMinY, objectsMaxY);

        // Ensure spikes are only on floor or ceiling
        if (randomIndex == 1)
        {
            float rand = Random.Range(0.0f, 1.0f);
            if (rand > 0.5)
            {
                randomY = -2.9f;
            }
            else
            {
                randomY = 3.2f;
            }
        }

        // Ensure Thwomps start at top
        if(randomIndex == 4)
        {
            randomY = 2.55f;
        }

        obj.transform.position = new Vector3(objectPositionX, randomY, 0);


        //4
        float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
        // Ensure Thwomps are not rotated
        if (randomIndex == 4)
        {
            rotation = 0;
        }
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

        //5
        objects.Add(obj);
    }

    private void GenerateObjectsIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;

        //2
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (var obj in objects)
        {
            //3
            float objX = obj.transform.position.x;

            //4
            farthestObjectX = Mathf.Max(farthestObjectX, objX);

            //5
            if (objX < removeObjectsX)
            {
                objectsToRemove.Add(obj);
            }
        }

        //6
        foreach (var obj in objectsToRemove)
        {
            objects.Remove(obj);
            Destroy(obj);
        }

        //7
        if (farthestObjectX < addObjectX)
        {
            AddObject(farthestObjectX);
        }
    }
}