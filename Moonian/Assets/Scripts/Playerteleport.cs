using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerteleport : MonoBehaviour
{
    Player player; 
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Teleport");
        }
    }

    IEnumerator Teleport()
    {
        player.disabled = true;
        yield return new WaitForSeconds(0.01f);
        gameObject.transform.position = new Vector3(-26f, 0f, 0f);
        yield return new WaitForSeconds(0.01f);
        Debug.Log("teleport");
        player.disabled = false;
    }
}
