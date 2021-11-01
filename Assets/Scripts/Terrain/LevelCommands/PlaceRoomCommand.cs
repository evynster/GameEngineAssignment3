using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaceRoomCommand : LevelCommand
{
    
    Structures.Room room;
    GameObject floor;
    GameObject levelStart;

    public PlaceRoomCommand(Structures.Room r,GameObject f,GameObject l)
    {
        room = r;
        floor = f;
        levelStart = l;
    }
    public void Execute()
    {
        for (int i = 0; i<room.size.x; i++)
        {
            for (int n = 0; n <room.size.y; n++)
            {
                GameObject newFloor = BuildingFactory.createFloor(room.colour);
                
                newFloor.transform.position = new Vector3((room.pos.x + i) * 4, 0, (room.pos.y + n) * 4) + levelStart.transform.position;
                room.roomComponents.Add(newFloor);
                if (n == 0)
                {
                    GameObject newWall = BuildingFactory.createWall(room.colour);
                    newWall.transform.position = new Vector3((room.pos.x + i) * 4 , 0, (room.pos.y + n - 0.5f) * 4) + levelStart.transform.position;
                    room.roomComponents.Add(newWall);
                }
                else if(n == room.size.y - 1)
                {
                    GameObject newWall = BuildingFactory.createWall(room.colour);
                    newWall.transform.position = new Vector3((room.pos.x + i) * 4, 0, (room.pos.y + n + 0.5f) * 4 ) + levelStart.transform.position;
                    room.roomComponents.Add(newWall);
                }
                if (i == 0)
                {
                    GameObject newWall = BuildingFactory.createWall(room.colour);
                    newWall.transform.position = new Vector3((room.pos.x + i - 0.5f) * 4, 0, (room.pos.y + n) * 4) + levelStart.transform.position;
                    newWall.transform.Rotate(Vector3.forward, 90f);
                    room.roomComponents.Add(newWall);
                }
                else if (i == room.size.x - 1)
                {
                    GameObject newWall = BuildingFactory.createWall(room.colour);
                    newWall.transform.position = new Vector3((room.pos.x + i + 0.5f) * 4, 0, (room.pos.y + n) * 4) + levelStart.transform.position;
                    newWall.transform.Rotate(Vector3.forward, 90f);
                    room.roomComponents.Add(newWall);
                }
            }
            
        }
    }

    public void Undo()
    {
        for(int i =0;i< room.roomComponents.Count; i++)
        {
            room.roomComponents[i].SetActive(false);
            room.roomComponents[i].transform.rotation = Quaternion.identity;
            room.roomComponents[i].transform.Rotate(Vector3.left, 90);
        }
        room.roomComponents.Clear();
    }
}
