using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class MapLevel
    {
        public enum Direction
        {
            None = 0,
            North = 1,
            East = 2,
            South = -1,
            West = -2
        }


        private Dictionary<MapSpace, Direction> deadEnds =
            new Dictionary<MapSpace, Direction>();


        private const char HORIZONTAL = '═';
        private const char VERTICAL = '║';
        private const char CORNER_NW = '╔';
        private const char CORNER_SE = '╝';
        private const char CORNER_NE = '╗';
        private const char CORNER_SW = '╚';
        public const char ROOM_INT = '·';
        public const char ROOM_DOOR = '╬';
        public const char HALLWAY = '▒';
        public const char STAIRWAY = '≣';
        public const char GOLD = '*';
        public const char ENEMY = 'E';
        private const char EMPTY = ' ';
        private const int REGION_WD = 26;
        private const int REGION_HT = 8;
        private const int MAP_WD = 78;
        private const int MAP_HT = 24;
        private const int MAX_ROOM_WT = 24;
        private const int MAX_ROOM_HT = 6;
        private const int MIN_ROOM_WT = 4;
        private const int MIN_ROOM_HT = 4;
        private const int ROOM_CREATE_PCT = 90;
        private const int ROOM_EXIT_PCT = 90;
        private const int ROOM_GOLD_PCT = 50;
        private const int ROOM_ENEMY_PCT = 40;


        private MapSpace[,] levelMap = new MapSpace[80, 25];


        private static Random rand = new Random();

        public MapSpace[,] LevelMap
        {
            get { return levelMap; }
        }

        public MapLevel()
        {

            MapGeneration();

            while (!VerifyMap())
            {
                Debug.WriteLine(MapText());
                MapGeneration();
            }
        }

        private bool VerifyMap()
        {


            bool retValue = true;
            List<char> dirCheck = new List<char>();

            for (int y = REGION_HT - MIN_ROOM_HT; y < (REGION_HT * 2) + MIN_ROOM_HT; y++)
            {
                dirCheck.Clear();
                for (int x = 0; x <= MAP_WD - 1; x++)
                {
                    if (!dirCheck.Contains(levelMap[x, y].MapCharacter))
                        dirCheck.Add(levelMap[x, y].MapCharacter);
                }
                retValue = dirCheck.Count > 1;
                if (!retValue) { break; }
            }



            if (retValue)
            {
                for (int x = REGION_WD - MIN_ROOM_WT; x < (REGION_WD * 2) + MIN_ROOM_WT; x++)
                {
                    dirCheck.Clear();
                    for (int y = 0; y <= MAP_HT - 1; y++)
                    {
                        if (!dirCheck.Contains(levelMap[x, y].MapCharacter))
                            dirCheck.Add(levelMap[x, y].MapCharacter);
                    }
                    retValue = dirCheck.Count > 1;
                    if (!retValue) { break; }
                }
            }

            return retValue;
        }

        private void MapGeneration()
        {

            int roomWidth = 0, roomHeight = 0, roomAnchorX = 0, roomAnchorY = 0;


            levelMap = new MapSpace[80, 25];

            for (int y = 1; y < 18; y += REGION_HT)
            {
                for (int x = 1; x < 54; x += REGION_WD)
                {
                    if (rand.Next(101) <= ROOM_CREATE_PCT)
                    {
                        roomHeight = rand.Next(MIN_ROOM_HT, MAX_ROOM_HT + 1);
                        roomWidth = rand.Next(MIN_ROOM_WT, MAX_ROOM_WT + 1);


                        roomAnchorY = (int)((REGION_HT - roomHeight) / 2) + y;
                        roomAnchorX = (int)((REGION_WD - roomWidth) / 2) + x;

                        RoomGeneration(roomAnchorX, roomAnchorY, roomWidth, roomHeight);
                    }
                }
            }

            for (int y = 0; y < 25; y++)
            {
                for (int x = 0; x < 80; x++)
                {
                    if (levelMap[x, y] is null)
                        levelMap[x, y] = new MapSpace(' ', false, false, x, y);
                }
            }


            HallwayGeneration();
            AddStairway();
        }

        private void AddStairway()
        {

            int x = 1; int y = 1;

            while (levelMap[x, y].MapCharacter != ROOM_INT)
            {
                x = rand.Next(1, MAP_WD);
                y = rand.Next(1, MAP_HT);
            }

            levelMap[x, y] = new MapSpace(STAIRWAY, x, y);
        }

        private void RoomGeneration(int westWallX, int northWallY, int roomWidth, int roomHeight)
        {


            int eastWallX = westWallX + roomWidth;
            int southWallY = northWallY + roomHeight;


            int regionNumber = GetRegionNumber(westWallX, northWallY);
            int doorway = 0, doorCount = 0, goldX, goldY, enemyX, enemyY;



            for (int y = northWallY; y <= southWallY; y++)
            {
                for (int x = westWallX; x <= eastWallX; x++)
                {
                    if (y == northWallY || y == southWallY)
                    {
                        levelMap[x, y] = new MapSpace(HORIZONTAL, false, false, x, y);
                    }
                    else if (x == westWallX || x == eastWallX)
                    {
                        levelMap[x, y] = new MapSpace(VERTICAL, false, false, x, y);
                    }
                    else if (levelMap[x, y] == null)
                        levelMap[x, y] = new MapSpace(ROOM_INT, false, false, x, y);
                }
            }


            while (doorCount == 0)
            {
                if (regionNumber >= 4 && rand.Next(101) <= ROOM_EXIT_PCT)
                {
                    doorway = rand.Next(westWallX + 1, eastWallX);
                    levelMap[doorway, northWallY] = new MapSpace(ROOM_DOOR, false, false, doorway, northWallY);
                    levelMap[doorway, northWallY - 1] = new MapSpace(HALLWAY, false, false, doorway, northWallY - 1);
                    deadEnds.Add(levelMap[doorway, northWallY - 1], Direction.North);
                    doorCount++;
                }

                if (regionNumber <= 6 && rand.Next(101) <= ROOM_EXIT_PCT)
                {
                    doorway = rand.Next(westWallX + 1, eastWallX);
                    levelMap[doorway, southWallY] = new MapSpace(ROOM_DOOR, false, false, doorway, southWallY);
                    levelMap[doorway, southWallY + 1] = new MapSpace(HALLWAY, false, false, doorway, southWallY + 1);
                    deadEnds.Add(levelMap[doorway, southWallY + 1], Direction.South);
                    doorCount++;
                }

                if ("147258".Contains(regionNumber.ToString()) && rand.Next(101) <= ROOM_EXIT_PCT)
                {
                    doorway = rand.Next(northWallY + 1, southWallY);
                    levelMap[eastWallX, doorway] = new MapSpace(ROOM_DOOR, false, false, eastWallX, doorway);
                    levelMap[eastWallX + 1, doorway] = new MapSpace(HALLWAY, false, false, eastWallX + 1, doorway);
                    deadEnds.Add(levelMap[eastWallX + 1, doorway], Direction.East);
                    doorCount++;
                }

                if ("258369".Contains(regionNumber.ToString()) && rand.Next(101) <= ROOM_EXIT_PCT)
                {
                    doorway = rand.Next(northWallY + 1, southWallY);
                    levelMap[westWallX, doorway] = new MapSpace(ROOM_DOOR, false, false, westWallX, doorway);
                    levelMap[westWallX - 1, doorway] = new MapSpace(HALLWAY, false, false, westWallX - 1, doorway);
                    deadEnds.Add(levelMap[westWallX - 1, doorway], Direction.West);
                    doorCount++;
                }
            }



            levelMap[westWallX, northWallY] = new MapSpace(CORNER_NW, false, false, westWallX, northWallY);
            levelMap[eastWallX, northWallY] = new MapSpace(CORNER_NE, false, false, eastWallX, northWallY);
            levelMap[westWallX, southWallY] = new MapSpace(CORNER_SW, false, false, westWallX, southWallY);
            levelMap[eastWallX, southWallY] = new MapSpace(CORNER_SE, false, false, eastWallX, southWallY);


            if (rand.Next(1, 101) > ROOM_GOLD_PCT)
            {
                goldX = westWallX; goldY = northWallY;

                while (levelMap[goldX, goldY].MapCharacter != ROOM_INT)
                {
                    goldX = rand.Next(westWallX + 1, eastWallX);
                    goldY = rand.Next(northWallY + 1, southWallY);
                }

                levelMap[goldX, goldY].ItemCharacter = GOLD;
            }
            if (rand.Next(1, 101) > ROOM_ENEMY_PCT)
            {
                enemyX = westWallX; enemyY = northWallY;

                while (levelMap[enemyX, enemyY].MapCharacter != ROOM_INT)
                {
                    enemyX = rand.Next(westWallX + 1, eastWallX);
                    enemyY = rand.Next(northWallY + 1, southWallY);
                }

                levelMap[enemyX, enemyY].ItemCharacter = ENEMY;
            }
        }

        private void HallwayGeneration()
        {

            Direction hallDirection = Direction.None; Direction direction90; Direction direction270;
            MapSpace hallwaySpace, newSpace;
            Dictionary<Direction, MapSpace> adjacentChars = new Dictionary<Direction, MapSpace>();
            Dictionary<Direction, MapSpace> surroundingChars = new Dictionary<Direction, MapSpace>();

            for (int i = deadEnds.Count - 1; i >= 0; i--)
            {
                hallwaySpace = deadEnds.ElementAt(i).Key;

                if (SearchAdjacent(ROOM_DOOR, hallwaySpace.X, hallwaySpace.Y).Count > 1)
                    deadEnds.Remove(hallwaySpace);
            }

            while (deadEnds.Count > 0)
            {

                for (int i = deadEnds.Count - 1; i >= 0; i--)
                {
                    hallwaySpace = deadEnds.ElementAt(i).Key;

                    if (SearchAdjacent(HALLWAY, hallwaySpace.X, hallwaySpace.Y).Count > 1)
                        deadEnds.Remove(hallwaySpace);
                }

                for (int i = deadEnds.Count - 1; i >= 0; i--)
                {

                    hallwaySpace = deadEnds.ElementAt(i).Key;
                    hallDirection = deadEnds.ElementAt(i).Value;
                    direction90 = GetDirection90(hallDirection);
                    direction270 = GetDirection270(hallDirection);


                    if (hallDirection != Direction.None)
                    {
                        surroundingChars = SearchAllDirections(hallwaySpace.X, hallwaySpace.Y);

                        switch (true)
                        {

                            case true when (surroundingChars[hallDirection] != null &&
                                    surroundingChars[hallDirection].MapCharacter == HALLWAY):

                                DrawHallway(hallwaySpace, surroundingChars[hallDirection], hallDirection);
                                deadEnds.Remove(hallwaySpace);

                                break;

                            case true when (surroundingChars[direction90] != null &&
                                    surroundingChars[direction90].MapCharacter == HALLWAY):

                                DrawHallway(hallwaySpace, surroundingChars[direction90], direction90);
                                deadEnds.Remove(hallwaySpace);

                                break;

                            case true when (surroundingChars[direction270] != null &&
                                    surroundingChars[direction270].MapCharacter == HALLWAY):

                                DrawHallway(hallwaySpace, surroundingChars[direction270], direction270);
                                deadEnds.Remove(hallwaySpace);

                                break;
                            default:

                                adjacentChars = SearchAdjacent(EMPTY, hallwaySpace.X, hallwaySpace.Y);
                                if (adjacentChars.ContainsKey(hallDirection))
                                {
                                    newSpace = new MapSpace(HALLWAY, adjacentChars[hallDirection]);
                                    levelMap[adjacentChars[hallDirection].X, adjacentChars[hallDirection].Y] = newSpace;
                                    deadEnds.Remove(hallwaySpace);
                                    deadEnds.Add(newSpace, hallDirection);
                                }
                                else if (adjacentChars.ContainsKey(direction90))
                                {
                                    newSpace = new MapSpace(HALLWAY, adjacentChars[direction90]);
                                    levelMap[adjacentChars[direction90].X, adjacentChars[direction90].Y] = newSpace;
                                    deadEnds.Remove(hallwaySpace);
                                    deadEnds.Add(newSpace, direction90);
                                }
                                else if (adjacentChars.ContainsKey(direction270))
                                {
                                    newSpace = new MapSpace(HALLWAY, adjacentChars[direction270]);
                                    levelMap[adjacentChars[direction270].X, adjacentChars[direction270].Y] = newSpace;
                                    deadEnds.Remove(hallwaySpace);
                                    deadEnds.Add(newSpace, direction270);
                                }
                                break;
                        }
                    }
                    else
                    {
                        deadEnds.Remove(hallwaySpace);
                    }


                }
            }
        }

        private void DrawHallway(MapSpace start, MapSpace end, Direction hallDirection)
        {



            switch (hallDirection)
            {
                case Direction.North:
                    for (int y = start.Y; y >= end.Y; y--)
                    {
                        levelMap[end.X, y] = new MapSpace(HALLWAY, end.X, y);
                        if (SearchAdjacent(HALLWAY, end.X, y).Count > 1)
                            break;
                    }
                    break;
                case Direction.South:
                    for (int y = start.Y; y <= end.Y; y++)
                    {
                        levelMap[end.X, y] = new MapSpace(HALLWAY, end.X, y);
                        if (SearchAdjacent(HALLWAY, end.X, y).Count > 1)
                            break;
                    }
                    break;
                case Direction.East:
                    for (int x = start.X; x <= end.X; x++)
                    {
                        levelMap[x, end.Y] = new MapSpace(HALLWAY, x, end.Y);
                        if (SearchAdjacent(HALLWAY, x, end.Y).Count > 1)
                            break;
                    }
                    break;
                case Direction.West:
                    for (int x = start.X; x >= end.X; x--)
                    {
                        levelMap[x, end.Y] = new MapSpace(HALLWAY, x, end.Y);
                        if (SearchAdjacent(HALLWAY, x, end.Y).Count > 1)
                            break;
                    }
                    break;
            }
        }

        private Direction GetDirection90(Direction startingDirection)
        {

            Direction retValue = (Math.Abs((int)startingDirection) == 1) ? (Direction)2 : (Direction)1;
            return retValue;
        }

        private Direction GetDirection270(Direction startingDirection)
        {

            Direction retValue = (Math.Abs((int)startingDirection) == 1) ? (Direction)2 : (Direction)1;
            retValue = (Direction)((int)retValue * -1);
            return retValue;
        }

        public Dictionary<Direction, MapSpace> SearchAdjacent(char character, int x, int y)
        {



            Dictionary<Direction, MapSpace> retValue = new Dictionary<Direction, MapSpace>();

            if (y - 1 >= 0 && levelMap[x, y - 1].MapCharacter == character)
                retValue.Add(Direction.North, levelMap[x, y - 1]);

            if (x + 1 <= MAP_WD && levelMap[x + 1, y].MapCharacter == character)
                retValue.Add(Direction.East, levelMap[x + 1, y]);

            if (y + 1 <= MAP_HT && levelMap[x, y + 1].MapCharacter == character)
                retValue.Add(Direction.South, levelMap[x, y + 1]);

            if ((x - 1) >= 0 && levelMap[x - 1, y].MapCharacter == character)
                retValue.Add(Direction.West, levelMap[x - 1, y]);

            return retValue;
        }
        public bool SearchAdjacentBool(char character, int x, int y)
        {
            Dictionary<Direction, MapSpace> retValue = new Dictionary<Direction, MapSpace>();

            if (y - 1 >= 0 && levelMap[x, y - 1].MapCharacter == character)
                retValue.Add(Direction.North, levelMap[x, y - 1]);

            if (x + 1 <= MAP_WD && levelMap[x + 1, y].MapCharacter == character)
                retValue.Add(Direction.East, levelMap[x + 1, y]);

            if (y + 1 <= MAP_HT && levelMap[x, y + 1].MapCharacter == character)
                retValue.Add(Direction.South, levelMap[x, y + 1]);

            if ((x - 1) >= 0 && levelMap[x - 1, y].MapCharacter == character)
                retValue.Add(Direction.West, levelMap[x - 1, y]);

            if (retValue.Count > 0) return true;
            else return false;
        }

        public Dictionary<Direction, MapSpace> SearchAdjacent(int x, int y)
        {


            Dictionary<Direction, MapSpace> retValue = new Dictionary<Direction, MapSpace>();
            retValue.Add(Direction.North, levelMap[x, y - 1]);
            retValue.Add(Direction.East, levelMap[x + 1, y]);
            retValue.Add(Direction.South, levelMap[x, y + 1]);
            retValue.Add(Direction.West, levelMap[x - 1, y]);

            return retValue;
        }

        public Dictionary<Direction, MapSpace> SearchAllDirections(int currentX, int currentY)
        {

            Dictionary<Direction, MapSpace> retValue = new Dictionary<Direction, MapSpace>();

            retValue.Add(Direction.North, SearchDirection(Direction.North, currentX, currentY - 1));
            retValue.Add(Direction.South, SearchDirection(Direction.South, currentX, currentY + 1));
            retValue.Add(Direction.East, SearchDirection(Direction.East, currentX + 1, currentY));
            retValue.Add(Direction.West, SearchDirection(Direction.West, currentX - 1, currentY));

            return retValue;
        }

        public MapSpace SearchDirection(Direction direction, int startX, int startY)
        {

            int currentX = startX, currentY = startY;
            MapSpace? retValue = null;

            currentY = (currentY > MAP_HT) ? MAP_HT : currentY;
            currentY = (currentY < 0) ? 0 : currentY;
            currentX = (currentX > MAP_WD) ? MAP_WD : currentX;
            currentX = (currentX < 0) ? 0 : currentX;

            switch (direction)
            {
                case Direction.North:
                    while (levelMap[currentX, currentY].MapCharacter == EMPTY && currentY > 0)
                        currentY--;
                    break;
                case Direction.East:
                    while (levelMap[currentX, currentY].MapCharacter == EMPTY && currentX < MAP_WD)
                        currentX++;
                    break;
                case Direction.South:
                    while (levelMap[currentX, currentY].MapCharacter == EMPTY && currentY < MAP_HT)
                        currentY++;
                    break;
                case Direction.West:
                    while (levelMap[currentX, currentY].MapCharacter == EMPTY && currentX > 0)
                        currentX--;
                    break;
            }

            if (levelMap[currentX, currentY].MapCharacter != EMPTY)
                retValue = levelMap[currentX, currentY];

            return retValue;
        }

        public MapSpace PlaceMapCharacter(char MapChar, bool Living)
        {


            Random random = new Random();
            int xPos = 1, yPos = 1;

            while (levelMap[xPos, yPos].MapCharacter != ROOM_INT &&
                levelMap[xPos, yPos].DisplayCharacter == null &&
                levelMap[xPos, yPos].ItemCharacter == null)
            {
                xPos = rand.Next(1, MAP_WD);
                yPos = rand.Next(1, MAP_HT);
            }


            if (Living)
                levelMap[xPos, yPos].DisplayCharacter = MapChar;
            else
                levelMap[xPos, yPos].ItemCharacter = MapChar;

            return levelMap[xPos, yPos];
        }

        public MapSpace MoveDisplayItem(MapSpace Start, MapSpace Destination)
        {

            levelMap[Destination.X, Destination.Y].DisplayCharacter = Start.DisplayCharacter;
            levelMap[Start.X, Start.Y].DisplayCharacter = null;

            return Destination;
        }

        public int GetRegionNumber(int RoomAnchorX, int RoomAnchorY)
        {


            int returnVal;

            int regionX = ((int)RoomAnchorX / REGION_WD) + 1;
            int regionY = ((int)RoomAnchorY / REGION_HT) + 1;

            returnVal = (regionX) + ((regionY - 1) * 3);

            return returnVal;
        }

        public string MapText()
        {

            StringBuilder sbReturn = new StringBuilder();



            for (int y = 0; y <= MAP_HT; y++)
            {
                for (int x = 0; x <= MAP_WD; x++)
                    if (levelMap[x, y].Visible)
                    {
                        if (levelMap[x, y].DisplayCharacter != null)
                            sbReturn.Append(levelMap[x, y].DisplayCharacter);
                        else if (levelMap[x, y].ItemCharacter != null)
                            sbReturn.Append(levelMap[x, y].ItemCharacter);
                        else
                            sbReturn.Append(levelMap[x, y].MapCharacter);
                    }
                    else
                    {

                        sbReturn.Append(' ');
                    }
                sbReturn.Append("\n");
            }

            return sbReturn.ToString();
        }
    }


    internal class MapSpace
    {
        public char MapCharacter { get; set; }
        public char? ItemCharacter { get; set; }
        public char? DisplayCharacter { get; set; }
        public bool SearchRequired { get; set; }
        public bool Visible { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public MapSpace()
        {

            this.MapCharacter = ' ';
            this.ItemCharacter = null;
            this.DisplayCharacter = null;
            this.SearchRequired = false;
            this.Visible = true;
            X = 0;
            Y = 0;
        }

        public MapSpace(char mapChar, MapSpace oldSpace)
        {
            this.MapCharacter = mapChar;
            this.ItemCharacter = null;
            this.DisplayCharacter = null;
            this.SearchRequired = oldSpace.SearchRequired;
            this.X = oldSpace.X; this.Y = oldSpace.Y; this.Visible = oldSpace.Visible;
        }

        public MapSpace(char mapChar, int X, int Y)
        {

            this.MapCharacter = mapChar;
            this.ItemCharacter = null;
            this.DisplayCharacter = null;
            this.SearchRequired = false;
            this.Visible = true;
            this.X = X;
            this.Y = Y;
        }

        public MapSpace(char mapChar, Boolean hidden, Boolean search, int X, int Y)
        {
            this.MapCharacter = mapChar;
            this.ItemCharacter = null;
            this.DisplayCharacter = null;
            this.SearchRequired = search;
            this.Visible = !hidden;
            this.X = X;
            this.Y = Y;
        }
    }
}