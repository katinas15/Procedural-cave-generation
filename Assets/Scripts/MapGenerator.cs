using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] int height;
    [SerializeField] int width;

    [SerializeField] string seed;
    [SerializeField] bool useRandomSeed;

    [Range(0,100)]
    [SerializeField] int randomFillPercent;

    int[,] map;

    void Start(){
        GenerateMap();
        RandomFillMap();

        for(int i = 0; i < smoothIterations; i++){
            SmoothMap();
        }
    }

    void GenerateMap() {
        map = new int[width, height];
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

    void OnDrawGizmos() {
        if (map != null) {
            for (int x = 0; x < width; x ++) {
                for (int y = 0; y < height; y ++) {
                    Gizmos.color = (map[x,y] == 1)?Color.black:Color.white;
                    Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);
                    Gizmos.DrawCube(pos,Vector3.one);
                }
            }
        }
    }


    void SmoothMap(){
        for (int x = 0; x < width; x ++) {
            for (int y = 0; y < height; y ++) {


            }
        }
    }


}
