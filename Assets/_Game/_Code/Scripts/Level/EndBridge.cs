using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBridge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BridgeGate bridgeGate = FindObjectOfType<BridgeGate>();
        if (bridgeGate != null)
        {
            bridgeGate.OnEndBridgeTriggerEnter(other);
        }
    }
}
