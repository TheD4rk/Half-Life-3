using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private bool buildMode;
    private Camera cam;
    private float maxDistance;
    
    public LayerMask interactionMask;

    [SerializeField]
    public Transform CamChild;

    public Transform FloorBuild;
    public Transform WallBuild;
    public Transform RampBuild;
    public Transform FloorPrefab;
    public Transform WallPrefab;
    public Transform RampPrefab;

    private Transform currentPreview;
    private Transform currentBuild;

    private RaycastHit hit;
    
    public GameObject bombPrefab;
    private float throwForce;

    private int hp;
    public Image hpBar;

    private void Start()
    {
        buildMode = false;
        maxDistance = 30f;
        currentPreview = FloorBuild;
        currentBuild = FloorPrefab;
        throwForce = 15f;
        hp = 10;
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (buildMode)
        {
            if (Physics.Raycast(CamChild.position, CamChild.forward, out hit, 7f))
            {
                currentPreview.position = new Vector3(
                    Mathf.RoundToInt(hit.point.x) != 0 ? Mathf.RoundToInt(hit.point.x / 3) * 3 : 3
                    , (Mathf.RoundToInt(hit.point.y) != 0 ? Mathf.RoundToInt(hit.point.y / 3) * 3 : 0) +
                      FloorBuild.localScale.y
                    , Mathf.RoundToInt(hit.point.z) != 0 ? Mathf.RoundToInt(hit.point.z / 3) * 3 : 3);
            }

            currentPreview.eulerAngles = new Vector3(0, Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(transform.eulerAngles.y / 90f) * 90f : 0,0);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (buildMode)
            {
                Build(currentBuild);
            }
            else
            {
                Shoot();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchBuildMode();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            buildMode = true;
            currentPreview = WallBuild;
            currentBuild = WallPrefab;
            HidePreviews(FloorBuild, RampBuild);
            EnablePreview(currentPreview);
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            buildMode = true;
            currentPreview = FloorBuild;
            currentBuild = FloorPrefab;
            HidePreviews(WallBuild, RampBuild);
            EnablePreview(currentPreview);
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            buildMode = true;
            currentPreview = RampBuild;
            currentBuild = RampPrefab;
            HidePreviews(FloorBuild, WallBuild);
            EnablePreview(currentPreview);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position + cam.transform.forward, Quaternion.identity);
            bomb.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, interactionMask))
        {
            if (hit.collider.CompareTag("Destructable"))
            {
                Destroy(hit.collider.gameObject);
            } else if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<BotController>().GetDamage();
            }
        }
    }

    void Build(Transform currentlySelectedType)
    {
        Instantiate(currentlySelectedType, currentPreview.position, currentPreview.rotation);
    }

    void SwitchBuildMode()
    {
        buildMode = !buildMode;

        if (!buildMode)
        {
            HidePreviews();
        }
    }

    void EnablePreview(Transform a)
    {
        a.gameObject.SetActive(true);
    }

    void HidePreviews()
    {
        WallBuild.gameObject.SetActive(false);
        FloorBuild.gameObject.SetActive(false);
        RampBuild.gameObject.SetActive(false);
    }

    void HidePreviews(Transform a, Transform b)
    {
        a.gameObject.SetActive(false);
        b.gameObject.SetActive(false);
    }

    public void GetHit()
    {
        if (hp > 1)
        {
            hp--;
            RefreshHealthbar();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void RefreshHealthbar()
    {
        Debug.Log(hpBar.fillAmount);
        hpBar.fillAmount = (float) hp / 10;
    }
}
