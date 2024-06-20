using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGate : MonoBehaviour
{
    [Header("Gate Settings")]
    public MeshRenderer gateMeshRenderer;
    public Transform gateTrans;
    public float openingSpeed = 2f;
    public bool isOpened = false;
    //public bool isCloseGate = false;

    [Header("Bridge Meshes")]
    public List<MeshRenderer> bridgeMeshes;

    [Header("End Bridge")]
    public Collider endBridgeCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;

        if (other.tag.Contains("Player"))
        {
            Material playerMat = other.GetComponent<PlayerController>().playerProperty.m_Material;
            gateMeshRenderer.material = playerMat;
            foreach (var mesh in bridgeMeshes)
            {
                mesh.material = playerMat;
            }
            isOpened = true;
            StartCoroutine(OpenGate());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isOpened || endBridgeCollider == null) return;

        if (other.tag.Contains("Player"))
        {
            
            endBridgeCollider.gameObject.SetActive(true);
        }
    }

    public void OnEndBridgeTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            GetComponent<Collider>().enabled = false;
        }

        foreach (var mesh in bridgeMeshes)
        {
            Collider biridgeCollider = mesh.GetComponent<Collider>();
            if (biridgeCollider != null)
            {
                endBridgeCollider.enabled = false;
            }

        }
        if (endBridgeCollider != null)
        {
            endBridgeCollider.enabled = false;
        }
    }

    IEnumerator OpenGate()
    {
        while (gateTrans.localScale.x > 0f)
        {
            Vector3 temp = gateTrans.localScale;
            temp.x -= Time.deltaTime * openingSpeed;
            temp.x = Mathf.Clamp(temp.x, 0, 1);
            gateTrans.localScale = temp;
            yield return null;
        }

        Vector3 temp1 = gateTrans.localScale;
        temp1.x = 0;
        gateTrans.localScale = temp1;
    }

    //IEnumerator CloseGate()
    //{
    //    while (gateTrans.localScale.x < 2f)
    //    {
    //        Vector3 temp = gateTrans.localScale;
    //        temp.x += Time.deltaTime * openingSpeed;
    //        temp.x = Mathf.Clamp(temp.x, 0, 1);
    //        gateTrans.localScale = temp;

    //        yield return null;
    //    }

    //    Vector3 temp1 = gateTrans.localScale;
    //    temp1.x = 1;
    //    gateTrans.localScale = temp1;
    //}
}

