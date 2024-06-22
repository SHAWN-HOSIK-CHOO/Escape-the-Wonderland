/*
 * CopyRight @2020 SunnyValleyStudio
 * Modified for University Project by Ho-Sik Choo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkPara", menuName = "PCG/RandomWalkData")]
public class RandomWalkSO : ScriptableObject
{
    public int  iterations                 = 10;
    public int  walkLength                 = 10;
    public bool startRandomlyEachIteration = true;
}
