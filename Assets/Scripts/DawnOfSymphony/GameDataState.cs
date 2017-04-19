using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SymphonyOfDawn
{
    public  static class GameDataState 
    {
        private static List  <MusicalObject> m_musicalObjectList  = new List<MusicalObject>();
        public static int LastSeedSpawnTaken { get; set; }
        public static ScannerEffectDemo scannerEffect;
        public static Color currentScannerColor { get; set; }
        public static Color currentScannerEdgeColor { get; set; }

        public static void Init()
        {
            scannerEffect = (Object.FindObjectsOfType(typeof(ScannerEffectDemo)) as ScannerEffectDemo[])[0];
        }
        
        public static void AddScannableObject(GameObject go)
        {
            scannerEffect.AddScannable(go.GetComponent<Scannable>());
        }

        public static void AddMusicalObject(string type, Vector3 position)
        {
            MusicalObject mo = new MusicalObject();
            mo.Instanciate(type, position);
            m_musicalObjectList.Add(mo);
            scannerEffect.AddScannable(mo.linkedGameObject.GetComponent<Scannable>());

            if(m_musicalObjectList.Count >=20)
            {
                EventManager.TriggerEvent("StartCrash");
            }


        }

        public static void DeleteAllMusicalObjects()
        {
            for(int i = m_musicalObjectList.Count - 1; i >= 0; i--)
            {
                m_musicalObjectList[i].Destroy();
                m_musicalObjectList.RemoveAt(i);
            }

            scannerEffect.ClearScannables();
        }

        

  
    }

}
