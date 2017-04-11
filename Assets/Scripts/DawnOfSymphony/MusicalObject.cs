using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalObject : MonoBehaviour {

    public GameObject m_gameobject;
    
    public void Instanciate(string type,Vector3 position)
    {
        
        m_gameobject = (GameObject)Instantiate((GameObject)Resources.Load(type));
        Vector3 bounds = m_gameobject.GetComponent<BoxCollider>().bounds.size;
        m_gameobject.transform.position = new Vector3(position.x, -bounds.y,position.z);
        Debug.Log("Spawning: "+ type);

    }

    public void Destroy()
    {
        Destroy(m_gameobject);
    }


}
