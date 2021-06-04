using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;
    public bool WallLeftExit = false;
    public bool WallBottomExit = false;
    public bool Floor = true;
    public bool Ceiling = true;
    public bool Light = true;

    public Color BlockLightColor = Color.red;

    public bool Visited;
    public int DistanceFromStart;
}

public class MazeGenerator
{
    public int Width;
    public int Height;

    public MazeGenerator(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public MazeGeneratorCell[,] GenerateMaze()
    {
        var maze = new MazeGeneratorCell[Width, Height];

        for (var x = 0; x < maze.GetLength(0); x++)
        {
            for (var y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratorCell { X = x, Y = y };
            }
        }

        for (var x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].WallLeft = false;
            maze[x, Height - 1].Floor = false;
            maze[x, Height - 1].Ceiling = false;
            maze[x, Height - 1].Light = false;
        }
        for (var y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].WallBottom = false;
            maze[Width - 1, y].Floor = false;
            maze[Width - 1, y].Ceiling = false;
            maze[Width - 1, y].Light = false;
        }

        RemoveWallsWithBacktracker(maze);
        PlaceMazeExit(maze);

        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();
            if (current.X > 0 && !maze[current.X - 1, current.Y].Visited)
                unvisitedNeighbours.Add(maze[current.X - 1, current.Y]);
            if (current.Y > 0 && !maze[current.X, current.Y - 1].Visited)
                unvisitedNeighbours.Add(maze[current.X, current.Y - 1]);
            if (current.X < Width - 2 && !maze[current.X + 1, current.Y].Visited)
                unvisitedNeighbours.Add(maze[current.X + 1, current.Y]);
            if (current.Y < Height - 2 && !maze[current.X, current.Y + 1].Visited)
                unvisitedNeighbours.Add(maze[current.X, current.Y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen =
                    unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);
                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;

                chosen.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }
        }
        while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell current, MazeGeneratorCell chosen)
    {
        if (current.X == chosen.X)
        {
            if (current.Y > chosen.Y)
                current.WallBottom = false;
            else chosen.WallBottom = false;
        }
        else
        {
            if (current.X > chosen.X)
                current.WallLeft = false;
            else chosen.WallLeft = false;
        }
    }

    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0, 0];

        for (var x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[x, Height - 2];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[x, 0];
        }
        for (var y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[Width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[0, y];
        }

        if (furthest.X == 0)
        {
            furthest.WallLeft = false;
            furthest.WallLeftExit = true;
            furthest.BlockLightColor = Color.green;
        }
        else if (furthest.Y == 0)
        {
            furthest.WallBottom = false;
            furthest.WallBottomExit = true;
            furthest.BlockLightColor = Color.green;
        }
        else if (furthest.X == Width - 2)
        {
            maze[furthest.X + 1, furthest.Y].WallLeft = false;
            maze[furthest.X + 1, furthest.Y].WallLeftExit = true;
            maze[furthest.X + 1, furthest.Y].BlockLightColor = Color.green;
        }
        else if (furthest.Y == Height - 2)
        {
            maze[furthest.X, furthest.Y + 1].WallBottom = false;
            maze[furthest.X, furthest.Y + 1].WallBottomExit = true;
            maze[furthest.X + 1, furthest.Y].BlockLightColor = Color.green;
        }
    }
}
