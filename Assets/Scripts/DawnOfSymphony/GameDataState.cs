using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SymphonyOfDawn
{
    public  static class GameDataState 
    {
        private static List  <MusicalObject> m_musicalObjectList  = new List<MusicalObject>();
        public static int LastSeedSpawnTaken { get; set; }

        public static void AddMusicalObject(string type, Vector3 position)
        {
            Debug.Log("Spawning: " + type + " at " + position.ToString());
            MusicalObject mo = new MusicalObject();
            mo.Instanciate(type, position);
            m_musicalObjectList.Add(mo);
            
        }

  
    }

}
