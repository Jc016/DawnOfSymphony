using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalObject : MonoBehaviour {

    public GameObject m_gameobject;
    public float m_growingSpeed = 0.2f;


    


    public void Instanciate(string type,Vector3 position)
    {
        m_gameobject = (GameObject)Instantiate((GameObject)Resources.Load(type));
        m_gameobject.transform.position = position;
        Debug.Log("Spawning");
    }

}
