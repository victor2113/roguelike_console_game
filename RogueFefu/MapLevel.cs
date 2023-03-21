using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class MapLevel
    {
        private enum Direction
        {
            None = 0,
            North = 1,
            East = 2,
            South = -1,
            West = -2
        }

        
        private Dictionary<MapSpace, Direction> deadEnds = new Dictionary<MapSpace, Direction>();

        //drwng symbols
        private const char HORIZONTAL = '═';
        private const char VERTICAL = '║';
        private const char CORNER_NW = '╔';
        private const char CORNER_SE = '╝';
        private const char CORNER_NE = '╗';
        private const char CORNER_SW = '╚';
        private const char ROOM_INT = '.';
        private const char ROOM_DOOR = '╬';
        private const char HALLWAY = '▓';
        private const char STAIRWAY = '≣';
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

        private MapSpace[,] levelMap = new MapSpace[80, 25];

        public MapLevel()
        {
            MapGeneration();

            while (!VerifyMap())
                MapGeneration();
        }
        public MapSpace[,] LevelMap
        {
            get { return levelMap; }
        }

        private bool VerifyMap()
        {
            throw new NotImplementedException();
        }

        private void MapGeneration() //Генерация всей карты
        {
            var rand = new Random();
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
            for (int y = 0; y < 25; y++)//заполнение пустых мест на карте
            {
                for (int x = 0; x < 80; x++)
                {
                    if (levelMap[x, y] is null)
                        levelMap[x, y] = new MapSpace(' ', false, false, x, y);
                }
            }


        }
        private void AddStairway()
        {
            var rand = new Random();
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
            int southWallY = northWallY + roomHeight;       // Получаем начало нижней и правой стен
            int regionNumber = GetRegionNumber(westWallX, northWallY);// Часть карты в которой расположена комната
            int doorway = 0, doorCount = 0;
            var rand = new Random();

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
                // Верхние двери
                if (regionNumber >= 4 && rand.Next(101) <= ROOM_EXIT_PCT)
                {
                    doorway = rand.Next(westWallX + 1, eastWallX);
                    levelMap[doorway, northWallY] = new MapSpace(ROOM_DOOR, false, false, doorway, northWallY);
                    levelMap[doorway, northWallY - 1] = new MapSpace(HALLWAY, false, false, doorway, northWallY - 1);
                    deadEnds.Add(levelMap[doorway, northWallY - 1], Direction.North);
                    doorCount++;
                }
                // Нижние
                if (regionNumber <= 6 && rand.Next(101) <= ROOM_EXIT_PCT)
                {
                    doorway = rand.Next(westWallX + 1, eastWallX);
                    levelMap[doorway, southWallY] = new MapSpace(ROOM_DOOR, false, false, doorway, southWallY);
                    levelMap[doorway, southWallY + 1] = new MapSpace(HALLWAY, false, false, doorway, southWallY + 1);
                    deadEnds.Add(levelMap[doorway, southWallY + 1], Direction.South);
                    doorCount++;
                }
                // Право
                if ("147258".Contains(regionNumber.ToString()) &&
                    rand.Next(101) <= ROOM_EXIT_PCT)
                {
                    doorway = rand.Next(northWallY + 1, southWallY);
                    levelMap[eastWallX, doorway] = new MapSpace(ROOM_DOOR, false, false, eastWallX, doorway);
                    levelMap[eastWallX + 1, doorway] = new MapSpace(HALLWAY, false, false, eastWallX + 1, doorway);
                    deadEnds.Add(levelMap[eastWallX + 1, doorway], Direction.East);
                    doorCount++;
                }
                // Лево
                if ("258369".Contains(regionNumber.ToString()) &&
                    rand.Next(101) <= ROOM_EXIT_PCT)
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
        }

        private int GetRegionNumber(int RoomAnchorX, int RoomAnchorY)//функция номер части карты
        {

            int returnVal;

            int regionX = ((int)RoomAnchorX / REGION_WD) + 1;
            int regionY = ((int)RoomAnchorY / REGION_HT) + 1;

            returnVal = (regionX) + ((regionY - 1) * 3);

            return returnVal;
        }
    }
    internal class MapSpace // базовый класс клетки карты
    {
        public char MapCharacter { get; set; }
        public bool SearchRequired { get; set; }
        public char DisplayCharacter { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public MapSpace()
        {
            this.MapCharacter = ' ';
            this.SearchRequired = false;
            this.DisplayCharacter = ' ';
            this.X = 0;
            this.Y = 0;
        }

        public MapSpace(char mapChar, int X, int Y)
        {
            this.MapCharacter = mapChar;
            this.SearchRequired = false;
            this.DisplayCharacter = mapChar;
            this.X = X;
            this.Y = Y;
        }

        public MapSpace(char mapChar, MapSpace oldSpace)
        {
            this.MapCharacter = mapChar;
            this.DisplayCharacter = mapChar;
            this.SearchRequired = oldSpace.SearchRequired;
            this.X = oldSpace.X;
            this.Y = oldSpace.Y;
        }

        public MapSpace(char mapChar, Boolean hidden, Boolean search, int X, int Y)
        {
            this.MapCharacter = mapChar;
            this.DisplayCharacter = hidden ? ' ' : mapChar;
            this.SearchRequired = search;
            this.X = X;
            this.Y = Y;
        }

    }
}


        
        

       

    
    

    

