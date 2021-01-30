using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    private IPlayerLightLevelController[] playerLightLevelControllers;

    public GameObject BaseLight;

    // Start is called before the first frame update
    void Start()
    {
        playerLightLevelControllers = new IPlayerLightLevelController[4];
        playerLightLevelControllers[0] = this.gameObject.GetComponent<PlayerLightsMainmenu>();
        playerLightLevelControllers[1] = this.gameObject.GetComponent<PlayerLightsOne>();
        playerLightLevelControllers[2] = this.gameObject.GetComponent<PlayerLightsTwo>();
        playerLightLevelControllers[3] = this.gameObject.GetComponent<PlayerLightsThree>();
    }

    public void StartupLightForLevel(int i)
    {
        if (i >= 0 && i < playerLightLevelControllers.Length && playerLightLevelControllers[i] != null)
        {
            playerLightLevelControllers[i].Startup();
        }
    }

    public void ShutdownLightForLevel(int i)
    {
        if (i >= 0 && playerLightLevelControllers.Length < i && playerLightLevelControllers[i] != null)
        {
            playerLightLevelControllers[i].Shutdown();
        }
    }

    public void SetLightToWhite()
    {
        var light = BaseLight.GetComponent<Light>();
        light.color = Color.white;
    }
}
