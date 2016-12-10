using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Builds dirt tiles on top of carpet.
    /// </summary>
    public class DirtTileBuilder : MonoBehaviour
    {
        /// <summary>
        /// Number of rows to be created. This should match the background loader.
        /// </summary>
        public float rowNum;

        /// <summary>
        /// Number of columns to be created. This should match the background loader.
        /// </summary>
        public float colNum;

        /// <summary>
        /// Setting for the scale of the dirt tile, relative to a carpet tile. 1
        /// is same scale, .5 is half, etc.
        /// </summary>
        public float scale = 0.02f;

        /// <summary>
        /// Dirt prefab.
        /// </summary>
        public GameObject dirtPrefab;

        // Use this for initialization
        private void Start()
        {
            CreateDirtTiles();
        }

        /// <summary>
        /// Generates the tiles
        /// </summary>
        private void CreateDirtTiles()
        {
            // Adjust the number of rows/columns based on the scale of the dirt tile.
            rowNum = rowNum / scale;
            colNum = colNum / scale;

            // Scale down the prefab
            dirtPrefab.transform.localScale = new Vector3(scale, scale, 1);

            Vector2 size = dirtPrefab.GetComponent<SpriteRenderer>().bounds.size;

            float startX = (size.x * colNum * -.5f) + size.x / 2;
            float startY = (size.y * rowNum * .5f) - size.y / 2;
            float currX = startX;
            float currY = startY;

            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    GameObject bg = ((GameObject)Instantiate(dirtPrefab, Vector2.zero, Quaternion.identity));
                    bg.transform.position = new Vector2(currX, currY);
                    bg.transform.SetParent(this.transform);
                    currX += size.x;
                }
                currY -= size.y;
                currX = startX;
            }
        }
    }
}