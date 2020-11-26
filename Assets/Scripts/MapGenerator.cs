using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] int height = 80;
    [SerializeField] int width = 80;

    [SerializeField] string seed;
    [SerializeField] bool useRandomSeed;
    [SerializeField] int smoothIterations = 5;

    [Range(0,100)]
    [SerializeField] int randomFillPercent = 60;

    int[,] map;

    void Start(){
        GenerateMap();
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            GenerateMap();
        }
    }

    void GenerateMap() {
        map = new int[width, height];
        RandomFillMap();

        for(int i = 0; i < smoothIterations; i++){
            SmoothMap();
        }

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(map, 1f);
    }

    void RandomFillMap(){
        if(useRandomSeed){
            seed = Time.time.ToString();
        }

        System.Random random = new System.Random(seed.GetHashCode());

        for(int x=0; x<width; x++){
            for(int y=0; y<height; y++){
                if (x == 0 || x == width-1 || y == 0 || y == height -1) {
                    map[x,y] = 1;
                } else {
                    map[x,y] = (random.Next(0,100) < randomFillPercent)? 1: 0;
                }
                
            }
        }
    }

    // void OnDrawGizmos() {
    //     if (map != null) {
    //         for (int x = 0; x < width; x ++) {
    //             for (int y = 0; y < height; y ++) {
    //                 Gizmos.color = (map[x,y] == 1)?Color.black:Color.white;
    //                 Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);
    //                 Gizmos.DrawCube(pos,Vector3.one);
    //             }
    //         }
    //     }
    // }


    void SmoothMap(){
        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < height; y ++) {
               int nieghbourWallTiles = GetSurroundingWallCount(x,y);

                if(nieghbourWallTiles > 4){
                    map[x,y] = 1;
                } else {
                    map[x,y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY){
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++){
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++){
                if(neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height){
                    if(neighbourX != gridX || neighbourY != gridY){
                        wallCount += map[neighbourX, neighbourY];
                    }
                } else {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }


}
