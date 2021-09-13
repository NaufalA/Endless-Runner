using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGeneratorController : MonoBehaviour
{
    [Header("Templates")]
    public List<TerrainTemplateController> terrainTemplates;
    public float terrainTemplateWidth;

    [Header("Generator Area")]
    public Camera gameCamera;

    public float areaStartOffset;
    public float areaEndOffset;
    
    [Header("Force Early Template")]
    public List<TerrainTemplateController> earlyTerrainTemplates;

    public const float DebugLineHeight = 10f;

    [SerializeField] private List<GameObject> _spawnedTerrain;
    private float _lastGeneratedPosX;
    private float _lastRemovedPosX;

    private void Start()
    {
        _spawnedTerrain = new List<GameObject>();
        
        _lastGeneratedPosX = GetHorizontalPositionStart();
        _lastRemovedPosX = _lastGeneratedPosX - terrainTemplateWidth;

        foreach (TerrainTemplateController terrain in earlyTerrainTemplates)
        {
            GenerateTerrain(_lastGeneratedPosX, terrain);
            _lastGeneratedPosX += terrainTemplateWidth;
        }
        
        while (_lastGeneratedPosX < GetHorizontalPositionEnd())
        {
            GenerateTerrain(_lastGeneratedPosX);
            _lastGeneratedPosX += terrainTemplateWidth;
        }
    }

    private void Update()
    {
        while (_lastGeneratedPosX < GetHorizontalPositionEnd())
        {
            GenerateTerrain(_lastGeneratedPosX);
            _lastGeneratedPosX += terrainTemplateWidth;
        }
        while (_lastRemovedPosX + terrainTemplateWidth < GetHorizontalPositionStart())
        {
            _lastRemovedPosX += terrainTemplateWidth;
            RemoveTerrain(_lastRemovedPosX);
        }
    }

    private void GenerateTerrain(float posX, TerrainTemplateController forceTerrain = null)
    {
        var terrainTemplate = forceTerrain != null ? forceTerrain : terrainTemplates[Random.Range(0, terrainTemplates.Count)];
        
        GameObject newTerrain = Instantiate(terrainTemplate.gameObject, transform);

        newTerrain.transform.position = new Vector2(posX, 0f);
        
        _spawnedTerrain.Add(newTerrain);
    }

    private void RemoveTerrain(float posX)
    {
        GameObject terrainToRemove = null;
        foreach (GameObject spawned in _spawnedTerrain)
        {
            if (spawned.transform.position.x == posX)
            {
                terrainToRemove = spawned;
                break;
            }
        }

        if (terrainToRemove != null)
        {
            _spawnedTerrain.Remove(terrainToRemove);
            Destroy(terrainToRemove);
        }
    }

    private float GetHorizontalPositionStart()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(0f, 0f)).x + areaStartOffset;
    }

    private float GetHorizontalPositionEnd()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(1f, 0f)).x + areaEndOffset;
    }

    private void OnDrawGizmos()
    {
        Vector3 areaStartPos = transform.position;
        Vector3 areaEndPos = areaStartPos;

        areaStartPos.x = GetHorizontalPositionStart();
        areaEndPos.x = GetHorizontalPositionEnd();
        
        Debug.DrawLine(areaStartPos+ Vector3.up * DebugLineHeight / 2, areaStartPos+ Vector3.down * DebugLineHeight / 2, Color.red);
        Debug.DrawLine(areaEndPos+ Vector3.up * DebugLineHeight / 2, areaEndPos+ Vector3.down * DebugLineHeight / 2, Color.red);
    }
}
