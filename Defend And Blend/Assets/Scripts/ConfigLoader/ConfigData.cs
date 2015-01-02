using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ConfigData  
{
    //public mo
    public static List<ConfMonster> monsterDatas = new List<ConfMonster>();
    public static List<Wave> waveDatas = new List<Wave>();
    public static GameConfig gameConfig = new GameConfig();
    public struct ConfMonster
    {
        public int monsterId;
        public float speed;
        public int damage;
        public int minSueezePower;
        public int maxSqueezePower;
        public int fruitSize;
    }
    public struct GameConfig
    {
        public float timeBetweenSpawns;
        public float timeBetweenWaves;
    }
    /*
    public struct ConfWaves
    {
        public int no;
        public int[] monsters;
    }
     */ 
}
