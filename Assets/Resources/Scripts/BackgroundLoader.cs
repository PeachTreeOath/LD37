using UnityEngine;
using System.Collections;

// Takes an image and creates a grid out of it.
public class BackgroundLoader : MonoBehaviour
{

    public int rowNum;
    public int colNum;

    // Background prefab. A prefab made up of 1 sprite is sufficient.
    public GameObject bgPrefab;
    // Parent object in hierarchy to create bg objects under.
    public GameObject parentGameObject;

    // Use this for initialization
    void Start()
    {
        CreateBG();
    }

    private void CreateBG()
    {
        Vector2 size = bgPrefab.GetComponent<SpriteRenderer>().bounds.size;

        float startX = (size.x * colNum * -.5f) + size.x / 2;
        float startY = (size.y * rowNum * .5f) - size.y / 2;
        float currX = startX;
        float currY = startY;

        for (int i = 0; i < rowNum; i++)
        {
            for (int j = 0; j < colNum; j++)
            {
                GameObject bg = ((GameObject)Instantiate(bgPrefab, Vector2.zero, Quaternion.identity));
                bg.transform.position = new Vector2(currX, currY);
                bg.transform.SetParent(parentGameObject.transform);
                SpriteRenderer spriteRenderer = bg.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingLayerID = SortingLayer.NameToID("Background");

                currX += size.x;
            }
            currY -= size.y;
            currX = startX;
        }
    }
}
