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

            StartCoroutine("Teleport12");
            FindObjectOfType<AudioManager>().Play("Blop_1");
            
        }
            
        if(other.tag == "portal 2")
        {

            StartCoroutine("Teleport21");
            FindObjectOfType<AudioManager>().Play("Blop_1");
            
        }

    }
    IEnumerator Teleport12()
    {
        float side = portal1.transform.position.z - player.transform.position.z;
        Debug.Log(side);
        Debug.Log(Mathf.Sign(side));
        player.disabled = true;
        yield return new WaitForSeconds(0.01f);

        Vector3 position = this.transform.position;
        position.x = portal2.position.x;
        position.y = portal2.position.y - 3f;
        position.z = portal2.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.01f);
        Debug.Log("teleport12");
        player.disabled = false;
    }

    IEnumerator Teleport21()
    {
        float side = portal2.transform.position.z - player.transform.position.z;
        Debug.Log(side);
        Debug.Log(Mathf.Sign(side));

        player.disabled = true;
        yield return new WaitForSeconds(0.01f);

        Vector3 position = this.transform.position;
        position.x = portal1.position.x;
        position.y = portal1.position.y - 3f;
        position.z = portal1.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.01f);
        Debug.Log("teleport21");
        player.disabled = false;
    }
}
