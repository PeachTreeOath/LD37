using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtParticleBuilder : MonoBehaviour {

    public float roomWidth;
    public float roomHeight;

    public int dirtPatches = 100;
    public float scale = 0.2f;

    public GameObject dirtParticlePrefab;

    // Use this for initialization
    void Start () {
        CreateDirtParticles();
    }

    private void CreateDirtParticles()
    {

        // Scale down the prefab
        dirtParticlePrefab.transform.localScale = new Vector3(scale, scale, 1);

        Vector2 size = dirtParticlePrefab.GetComponent<SpriteRenderer>().bounds.size;

        float randXMin = -roomWidth / 2f + size.x / 2f;
        float randXMax = roomWidth / 2f - size.x / 2f;
        float randYMin = -roomHeight / 2f + size.y / 2f;
        float randYMax = roomHeight / 2f - size.y / 2f;

        for (int i = 0; i < dirtPatches; i++)
        {
            float randX = Random.Range(randXMin, randXMax);
            float randY = Random.Range(randYMin, randYMax);

            GameObject dirtParticleInstance = ((GameObject)Instantiate(dirtParticlePrefab, Vector2.zero, Quaternion.identity));
            dirtParticleInstance.transform.position = new Vector2(randX, randY);
            dirtParticleInstance.transform.SetParent(this.transform);
            SpriteRenderer spriteRenderer = dirtParticleInstance.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerID = SortingLayer.NameToID("BackgroundDirt");
      
        }
    }
}
