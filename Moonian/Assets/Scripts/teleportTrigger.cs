using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportTrigger : MonoBehaviour
{
    Player player;
    public Transform portal1;
    public Transform portal2;
    public Transform portal3;
    public Transform portal4;
    public Transform portal5;
    public Transform portal6;

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

        if (other.tag == "portal 3")
        {

            StartCoroutine("Teleport34");
            FindObjectOfType<AudioManager>().Play("Blop_1");

        }

        if (other.tag == "portal 4")
        {

            StartCoroutine("Teleport43");
            FindObjectOfType<AudioManager>().Play("Blop_1");

        }

        if (other.tag == "portal 5")
        {

            StartCoroutine("Teleport56");
            FindObjectOfType<AudioManager>().Play("Blop_1");

        }

        if (other.tag == "portal 6")
        {

            StartCoroutine("Teleport65");
            FindObjectOfType<AudioManager>().Play("Blop_1");

        }

    }
    IEnumerator Teleport12()
    {
        float side = portal1.transform.position.z - player.transform.position.z;
        //Debug.Log(side);
        //Debug.Log(Mathf.Sign(side));
        player.disabled = true;
        yield return new WaitForSeconds(0.001f);

        Vector3 position = this.transform.position;
        position.x = portal2.position.x;
        position.y = portal2.position.y - 3.3f;
        position.z = portal2.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.001f);
        Debug.Log("teleport12");
        player.disabled = false;
    }

    IEnumerator Teleport21()
    {
        float side = portal2.transform.position.z - player.transform.position.z;
        //Debug.Log(side);
        //Debug.Log(Mathf.Sign(side));

        player.disabled = true;
        yield return new WaitForSeconds(0.001f);

        Vector3 position = this.transform.position;
        position.x = portal1.position.x;
        position.y = portal1.position.y - 3.3f;
        position.z = portal1.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.001f);
        Debug.Log("teleport21");
        player.disabled = false;
    }

    IEnumerator Teleport34()
    {
        //Debug.Log("teleport34");
        float side = portal3.transform.position.z - player.transform.position.z;
        //Debug.Log(side);
        //Debug.Log(Mathf.Sign(side));
        player.disabled = true;
        yield return new WaitForSeconds(0.001f);

        Vector3 position = this.transform.position;
        position.x = portal4.position.x;
        position.y = portal4.position.y - 3.2f;
        position.z = portal4.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.001f);
        Debug.Log("teleport34");
        player.disabled = false;
    }

    IEnumerator Teleport43()
    {
        float side = portal4.transform.position.z - player.transform.position.z;
        //Debug.Log(side);
        //Debug.Log(Mathf.Sign(side));
        player.disabled = true;
        yield return new WaitForSeconds(0.001f);

        Vector3 position = this.transform.position;
        position.x = portal3.position.x;
        position.y = portal3.position.y - 3.2f;
        position.z = portal3.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.001f);
        Debug.Log("teleport43");
        player.disabled = false;
    }

    IEnumerator Teleport56()
    {
        //Debug.Log("teleport34");
        float side = portal5.transform.position.z - player.transform.position.z;
        //Debug.Log(side);
        //Debug.Log(Mathf.Sign(side));
        player.disabled = true;
        yield return new WaitForSeconds(0.001f);

        Vector3 position = this.transform.position;
        position.x = portal6.position.x;
        position.y = portal6.position.y - 3.2f;
        position.z = portal6.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.001f);
        Debug.Log("teleport56");
        player.disabled = false;
    }

    IEnumerator Teleport65()
    {
        //Debug.Log("teleport34");
        float side = portal6.transform.position.z - player.transform.position.z;
        //Debug.Log(side);
        //Debug.Log(Mathf.Sign(side));
        player.disabled = true;
        yield return new WaitForSeconds(0.01f);

        Vector3 position = this.transform.position;
        position.x = portal5.position.x;
        position.y = portal5.position.y - 3.2f;
        position.z = portal5.position.z + Mathf.Sign(side) * 1f;
        gameObject.transform.position = position;

        yield return new WaitForSeconds(0.01f);
        Debug.Log("teleport65");
        player.disabled = false;
    }
}
