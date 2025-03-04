using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    Camera cam;
    Collider planecollider;
    RaycastHit hit;
    Ray ray;
    public Transform pickups;
    public InputField InputFieldPickupAmount;

    private int pickupsAtStart;
    private int pickupsCollected;


    void Start()

    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        planecollider = GameObject.Find("Plane").GetComponent<Collider>();

        pickupsAtStart = pickups.childCount;
        StartCoroutine("updateGameStatus");
    }

    IEnumerator updateGameStatus()
    {
        for (; ; )
        {
            pickupsCollected = pickupsAtStart - pickups.childCount;
            InputFieldPickupAmount.text = pickupsCollected.ToString();
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == planecollider)
            {
                transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 4);
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }
}
