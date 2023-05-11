using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] GameObject grassCube;
    [SerializeField] GameObject stoneCube;
    [SerializeField] GameObject desertCube;
    [SerializeField] GameObject swampCube;
    [SerializeField] GameObject waterCube;
    [SerializeField] GameObject player;
    [SerializeField] GameObject flower;
    [SerializeField] GameObject tree;
    [SerializeField] GameObject mush;
    [SerializeField] GameObject bush;
    [SerializeField] GameObject rock;
    [SerializeField] GameObject branch;
    [SerializeField] GameObject skeletorDesert1;
    [SerializeField] GameObject skeletorDesert2;
    [SerializeField] GameObject skeletorGrass1;
    [SerializeField] GameObject skeletorGrass2;
    [SerializeField] GameObject skeletorSwamp1;
    [SerializeField] GameObject skeletorSwamp2;
    private NavigationBaker nav;


    [SerializeField] int width, height, large;
    [SerializeField] float smoothness;
    [SerializeField] int seed;

    [Range(0, 1)]
    [SerializeField] float desert;
    [Range(0, 1)]
    [SerializeField] float grass;
    float cambioAltura;
    [SerializeField] int alturaAgua;
    [Header("vegetacion gen")]
    [Range(0, 1)]
    [SerializeField] float vegetacionGen;
    [Header("vegetacion Porcentaje")]
    [Range(0, 1)]
    [SerializeField] float vegetacionPorcentaje;
    [Header("enemy gen")]
    [Range(0, 1)]
    [SerializeField] float enemyGen;
    [Header("enemy Porcentaje")]
    [Range(0, 1)]
    [SerializeField] float enemyPorcentaje;

    private void Start()
    {
        seed = Random.Range(-100000, 100000);
        GenerateMap();
        nav = GetComponent<NavigationBaker>();
    }

    public void GenerateMap()
    {
        // Recorremos las 3 dimensiones
        for (int x = 0; x < width; x++)
        {

            for (int z = 0; z < large; z++)
            {

                float calculo = 10000 / smoothness;
                int perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / calculo + seed, z / calculo + seed) * height + cambioAltura);

                for (int y = 0; y < perlinHeight; y++)
                {
                    Spawn(x, y, z, perlinHeight);

                    if (y <= perlinHeight - 2)
                    {
                        Instantiate(stoneCube, new Vector3(x, y, z), Quaternion.identity);
                    }
                    //Bioma desierto
                    if (x < width * desert && y > perlinHeight - 2)
                    {
                        Instantiate(desertCube, new Vector3(x, y, z), Quaternion.identity);
                        //Generacion de vegetacion en el desierto
                        if (Mathf.PerlinNoise(x / vegetacionGen + seed, z / vegetacionGen + seed) < vegetacionPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(rock, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }                                                    
                        }
                        if (Mathf.PerlinNoise(x + width / vegetacionGen + seed, z + large / vegetacionGen + seed) < vegetacionPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(branch, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }                          
                        }
                        //Generacion de enemigos en el desierto
                        if (Mathf.PerlinNoise(x / enemyGen + seed, z / enemyGen + seed) < enemyPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(skeletorDesert1, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }
                        }
                        if (Mathf.PerlinNoise(x + width / enemyGen + seed, z + large / enemyGen + seed) < enemyPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(skeletorDesert2, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }
                        }
                    }
                    //Bioma pradera
                    else if (x >= width * desert && x <= width * grass && y > perlinHeight - 2)
                    {
                        Instantiate(grassCube, new Vector3(x, y, z), Quaternion.identity);
                        //Generacion vegetacion en la pradera
                        if (Mathf.PerlinNoise(x / vegetacionGen + seed, z / vegetacionGen + seed) < vegetacionPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(flower, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }                         
                        }
                        if (Mathf.PerlinNoise(x + width / vegetacionGen + seed, z + large / vegetacionGen + seed) < vegetacionPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(bush, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }                          
                        }
                        //Generacion de enemigos en la pradera
                        if (Mathf.PerlinNoise(x / enemyGen + seed, z / enemyGen + seed) < enemyPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(skeletorGrass1, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }
                        }
                        if (Mathf.PerlinNoise(x + width / enemyGen + seed, z + large / enemyGen + seed) < enemyPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(skeletorGrass2, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }
                        }
                    }
                    //Bioma montaña
                    else if (x < width && y > perlinHeight - 2)
                    {
                        Instantiate(swampCube, new Vector3(x, y, z), Quaternion.identity);
                        //Generacion vegetacion en montaña
                        if (Mathf.PerlinNoise(x / vegetacionGen + seed, z / vegetacionGen + seed) < vegetacionPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(mush, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }
   
                        }
                        if (Mathf.PerlinNoise(x + width / vegetacionGen + seed, z + large / vegetacionGen + seed) < vegetacionPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(tree, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }        
                        }
                        //Generacion de enemigos en montaña
                        if (Mathf.PerlinNoise(x / enemyGen + seed, z / enemyGen + seed) < enemyPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(skeletorSwamp1, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }
                        }
                        if (Mathf.PerlinNoise(x + width / enemyGen + seed, z + large / enemyGen + seed) < enemyPorcentaje)
                        {
                            if (perlinHeight > alturaAgua)
                            {
                                Instantiate(skeletorSwamp2, new Vector3(x, perlinHeight - 0.5f, z), Quaternion.identity);
                            }
                        }
                    }
                }

                if (perlinHeight < alturaAgua)
                {
                    for (int i = perlinHeight; i < alturaAgua; i++)
                    {
                        Instantiate(waterCube, new Vector3(x, i, z), Quaternion.identity);
                    }

                }
            }
            smoothness++;
            if (x % 5 == 0)
            {
                cambioAltura += 0.3f;
            }

        }

        //nav.cooking();
    }
    private void Spawn(int x, int y, int z, int perlin)
    {
        if (x == width * 0.5 && z == large * 0.5)
        {
            player.transform.position = new Vector3(x, perlin + 2, z);
        }
    }
}