using UnityEngine;
using System;
using UnityEngine;

namespace Workshop.Student
{
    public class MapGenerator : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;

        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;

        public string[,] saveItemMap = new string[3, 3] {
            { " ", "Soda", " "},
            { " ", " ", " "},
            { " ", " ", "Food"},
        };

        // 1. declare Players variable
        public GameObject playerPrefab;

        // 7. declare Exit variable 
        public GameObject exitPrefab;

        public void Start()
        {
            // 1. random player at the position <0, 0> map
            if (playerPrefab != null)
            {
                Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
            }

            // 2. create obstacles
            // ตัวอย่าง: สุ่มสร้างวัตถุกีดขวางในพื้นที่ด้านใน (เว้นขอบและจุดเริ่มต้น/จุดออก)
            if (wallTiles != null && wallTiles.Length > 0)
            {
                int obstacleCount = UnityEngine.Random.Range(5, 10);
                for (int i = 0; i < obstacleCount; i++)
                {
                    int randomX = UnityEngine.Random.Range(1, columns - 1);
                    int randomY = UnityEngine.Random.Range(1, rows - 1);
                    GameObject randomWall = wallTiles[UnityEngine.Random.Range(0, wallTiles.Length)];
                    Instantiate(randomWall, new Vector2(randomX, randomY), Quaternion.identity);
                }
            }

            // 3. create floor
            if (floorTiles != null && floorTiles.Length > 0)
            {
                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        GameObject randomFloor = floorTiles[UnityEngine.Random.Range(0, floorTiles.Length)];
                        Instantiate(randomFloor, new Vector2(x, y), Quaternion.identity);
                    }
                }
            }

            // 4. create walls (กำแพงล้อมรอบแผนที่)
            if (wallTiles != null && wallTiles.Length > 0)
            {
                for (int x = -1; x <= columns; x++)
                {
                    for (int y = -1; y <= rows; y++)
                    {
                        if (x == -1 || x == columns || y == -1 || y == rows)
                        {
                            GameObject outerWall = wallTiles[UnityEngine.Random.Range(0, wallTiles.Length)];
                            Instantiate(outerWall, new Vector2(x, y), Quaternion.identity);
                        }
                    }
                }
            }

            // 5. random foods
            if (foodTiles != null && foodTiles.Length > 0)
            {
                int foodCount = UnityEngine.Random.Range(2, 5);
                for (int i = 0; i < foodCount; i++)
                {
                    int randomX = UnityEngine.Random.Range(1, columns - 1);
                    int randomY = UnityEngine.Random.Range(1, rows - 1);
                    GameObject randomFood = foodTiles[UnityEngine.Random.Range(0, foodTiles.Length)];
                    Instantiate(randomFood, new Vector2(randomX, randomY), Quaternion.identity);
                }
            }

            // 6. generate item along with the saveItemMap
            if (foodTiles != null && foodTiles.Length > 0)
            {
                int mapRows = saveItemMap.GetLength(0);
                int mapCols = saveItemMap.GetLength(1);

                for (int r = 0; r < mapRows; r++)
                {
                    for (int c = 0; c < mapCols; c++)
                    {
                        string itemType = saveItemMap[r, c];
                        if (itemType != " ")
                        {
                            // สุ่มสร้าง Food จาก Prefab ที่กำหนดไว้ใน foodTiles
                            GameObject itemToSpawn = foodTiles[UnityEngine.Random.Range(0, foodTiles.Length)];
                            Instantiate(itemToSpawn, new Vector2(c, mapRows - 1 - r), Quaternion.identity);
                        }
                    }
                }
            }

            // 7. place exit (วางทางออกไว้ที่มุมขวาบนของแผนที่)
            if (exitPrefab != null)
            {
                Instantiate(exitPrefab, new Vector2(columns - 1, rows - 1), Quaternion.identity);
            }
        }
    }
}