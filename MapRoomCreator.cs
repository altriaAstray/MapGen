﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoomCreator : MonoBehaviour {
    // Id assigned to newly created rooms. Incremented on each assignment
    private int id;

    private int numberToCreate;

    private int maxHeight;
    private int maxWidth;

    private int meanHeight;
    private int meanWidth;

    private int minHeight;
    private int minWdith;

    private int deviation;

    private int spawnHeight;
    private int spawnWidth;

    /// <summary>
    /// Creator of Map rooms in given spawn ellipsis area.
    /// </summary>
    public MapRoomCreator(MapSettings mapSettings)
    {
        numberToCreate = mapSettings.numberOfRoomsToCreate;

        spawnHeight = mapSettings.roomSpawnEllipsisAreaHeight;
        spawnWidth = mapSettings.roomSpawnEllipsisAreaWidth;

        meanHeight = mapSettings.roomMeanHeight;
        meanWidth = mapSettings.roomMeanWidth;

        deviation = mapSettings.roomStandardDeviation;

        maxHeight = mapSettings.roomMaxHeight;
        maxWidth = mapSettings.roomMaxWidth;

        minHeight = mapSettings.roomMinHeight;
        minWdith = mapSettings.roomMinWidth;
    }

    /// <summary>
    /// Create a number of rooms inside a circle in 2d space
    /// </summary>
    /// <returns></returns>
    public List<MapRoom> CreateRooms()
    {
        // Create rooms inside spawn area
        List<MapRoom> rooms = new List<MapRoom>();
        for (int i = 0; i < numberToCreate; i++)
        {
            rooms.Add(CreateRoom());
        }

        return rooms;
    }

    /// <summary>
    /// Create a room inside a given spawn area.
    /// </summary>
    /// <returns></returns>
    private MapRoom CreateRoom()
    {
        Vector2 randomPointInElipsis = MathHelpers.RandomPointInElipsis(spawnWidth, spawnHeight);

        // Round point to lock to 'grid' space
        Point loc = new Point(Mathf.RoundToInt(randomPointInElipsis.x), Mathf.RoundToInt(randomPointInElipsis.y));

        // Get normal distribution of room sizes
        int width = MathHelpers.FindValueInDistributionRange(meanWidth, deviation, maxWidth, minWdith);
        int height = MathHelpers.FindValueInDistributionRange(meanHeight, deviation, maxHeight, minHeight);

        return new MapRoom(loc, width, height, id++);
    }
}
