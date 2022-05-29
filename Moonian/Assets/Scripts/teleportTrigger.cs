using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportTrigger : MonoBehaviour
{
    Player player;
    public Transform portal2;
    public Transform portal1;

    private bool portal1_enter = false;
    private bool portal2_enter = false;

    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "portal 1")
        {
            if(portal2_enter == false)
            {
                StartCoroutine("Teleport12");
                portal1_enter = true;
            }
            else
            {
                portal2_enter = false;
            }
            
        }
            
        if(other.tag == "portal 2")
        {
            if(portal1_enter == false)
            {
                StartCoroutine("Teleport21");
                portal2_enter = true;
            }
            else
            {
                portal1_enter = false;
            }
            
        }

    }
    IEnumerator Teleport12()
    {
        player.disabled = true;
        yield return new WaitForSeconds(0.01f);

        Vector3 position = this.transform.position;
        position.x = portal2.position.x;
        position.y = portal2.position.y - 5f;
        position.z = portal2.position.z;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.01f);
        Debug.Log("teleport12");
        player.disabled = false;
    }

    IEnumerator Teleport21()
    {
        player.disabled = true;
        yield return new WaitForSeconds(0.01f);

        Vector3 position = this.transform.position;
        position.x = portal1.position.x;
        position.y = portal1.position.y - 5f;
        position.z = portal1.position.z;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.01f);
        Debug.Log("teleport21");
        player.disabled = false;
    }
}
