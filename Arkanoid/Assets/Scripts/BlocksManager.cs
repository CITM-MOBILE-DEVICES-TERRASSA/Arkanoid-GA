using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float spawnPositionY = 3.0f;
    [SerializeField] private int rows = 1;
    [SerializeField] private int columns = 1;

    private void Awake()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        float blockWidth = blockPrefab.GetComponent<Renderer>().bounds.size.x;
        float blockHeight = blockPrefab.GetComponent<Renderer>().bounds.size.y;

        float totalWidth = columns * blockWidth;
        float startY = spawnPositionY;

        for (int row = 0; row < rows; row++)
        {
            int healthForRow = rows - row;

            float startX = -(totalWidth / 2.0f) + (blockWidth / 2.0f);

            for (int column = 0; column < columns; column++)
            {
                Vector3 spawnPosition = new Vector3(startX + column * blockWidth, startY - row * blockHeight, 0);
                GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

                Block blockScript = block.GetComponent<Block>();
                blockScript.SetHealth(healthForRow);
            }
        }
    }
}
