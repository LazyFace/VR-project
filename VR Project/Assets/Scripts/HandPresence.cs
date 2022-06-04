using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    [SerializeField] private bool showController = false;
    [SerializeField] private List<GameObject> controllerPrefabs;
    [SerializeField] InputDeviceCharacteristics controllerCharacteristics;
    [SerializeField] private GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;

    private Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetDevice());
    }

    // Update is called once per frame
    void Update()
    {
        if(targetDevice != null)
        {
            UpdateHandAnimation();
        }
    }

    IEnumerator GetDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        while(devices.Count == 0)
        {
            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
            
            yield return null;
        }
        targetDevice = devices[0];
        GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
        Debug.Log(targetDevice.name);
        if(prefab)
        {
            spawnedController = Instantiate(prefab, transform);
        }
        else
        {
            Debug.LogError("Did not find corresponding controller model");
            spawnedController = Instantiate(controllerPrefabs[0], transform);
        }

        spawnedHandModel = Instantiate(handModelPrefab, transform);

        if(showController)
        {
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
        }
        else
        {
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);
        }

        handAnimator = spawnedHandModel.GetComponent<Animator>();
    }

    void UpdateHandAnimation()
    {
        if(handAnimator != null)
        {
            if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                handAnimator.SetFloat("Trigger", triggerValue);
            }
            else
            {
                handAnimator.SetFloat("Trigger",0);
            }

            if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                handAnimator.SetFloat("Grip", gripValue);
            }
            else
            {
                handAnimator.SetFloat("Grip", 0);
            }
        }
    }
}
