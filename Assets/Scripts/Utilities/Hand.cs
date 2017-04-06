using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;


public class Hand : MonoBehaviour {
    public enum State
    {
        EMPTY,
        TOUCHING,
        HOLDING
    };

    public OVRInput.Controller Controller = OVRInput.Controller.LTouch;
    public State mHandState = State.EMPTY;
    public Rigidbody AttachPoint = null;
    public bool IgnoreContactPoint = false;
    private Rigidbody mHeldObject;
    private FixedJoint mTempJoint;
    public float force =1;

    void Start()
    {
        if (AttachPoint == null)
        {
            AttachPoint = GetComponent<Rigidbody>();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (mHandState == State.EMPTY)
        {
            GameObject temp = collider.gameObject;
            if (temp != null && temp.layer == LayerMask.NameToLayer("grabbable") && temp.GetComponent<Rigidbody>() != null)
            {
                mHeldObject = temp.GetComponent<Rigidbody>();
                mHandState = State.TOUCHING;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (mHandState != State.HOLDING)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("grabbable"))
            {
                mHeldObject = null;
                mHandState = State.EMPTY;
            }
        }
    }

    void Update()
    {
        switch (mHandState)
        {
            case State.TOUCHING:
                if (mTempJoint == null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) >= 0.5f)
                {
                    mHeldObject.velocity = Vector3.zero;
                    mTempJoint = mHeldObject.gameObject.AddComponent<FixedJoint>();
                    mTempJoint.connectedBody = AttachPoint;
                    mHandState = State.HOLDING;
                    SeedController seed = mHeldObject.GetComponent<SeedController>();
                    mHeldObject.GetComponent<BoxCollider>().enabled = false;
                    GameDataState.LastSeedSpawnTaken = seed.SourceSpawnID;
                    EventManager.TriggerEvent("SeedTaken");
                }
                break;
            case State.HOLDING:
                if (mTempJoint != null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) < 0.5f)
                {
                    Object.DestroyImmediate(mTempJoint);
                    mTempJoint = null;
                    throwObject();
                    mHandState = State.EMPTY;
                }
                break;
        }
    }

    private void throwObject()
    {
        mHeldObject.velocity = OVRInput.GetLocalControllerVelocity(Controller) * force;
        mHeldObject.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller) * Mathf.Deg2Rad;
        mHeldObject.maxAngularVelocity = mHeldObject.angularVelocity.magnitude;
        mHeldObject.GetComponent<BoxCollider>().enabled = true;

    }

}
