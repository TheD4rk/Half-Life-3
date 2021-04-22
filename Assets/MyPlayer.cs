using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;

public class MyPlayer : MonoBehaviourPun, IPunObservable
{
    public Camera cam;
    public GameObject healthBar;
    public GameObject sunglasses;
    public GameObject bulletPrefab;
    private float health = 1f;

    void UpdateHealthbar()
    {
        Vector3 oldScale = healthBar.transform.localScale;
        healthBar.transform.localScale = new Vector3(health, oldScale.y, oldScale.z);
    }

    public void Hit()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        health -= 0.2f;

        if (health <= 0)
        {
            health = 0f;
            Debug.Log("Player Died :(");
        }
        UpdateHealthbar();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            healthBar.layer = LayerMask.NameToLayer("Character");
            sunglasses.layer = LayerMask.NameToLayer("Character");
        }
        else
        {
            cam.enabled = false;
            cam.gameObject.GetComponent<AudioListener>().enabled = false;
            gameObject.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, cam.transform.position + cam.transform.forward, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 10, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                gameObject.GetComponent<FirstPersonController>().enabled =
                    !gameObject.GetComponent<FirstPersonController>().enabled;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float) stream.ReceiveNext();
            UpdateHealthbar();
        }
    }
}
