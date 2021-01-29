using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MusicSliderBehaviour : MonoBehaviour
{
    
    public float SoundSliderValue;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = Vector3.zero;
        transform.localPosition = new Vector3(-2, 15.45f, -18);
    }

    // Update is called once per frame
    void Update()
    {
      
        var clampedX = Mathf.Clamp(transform.localPosition.x, -9.0f, 6f);
        transform.localPosition = new Vector3(clampedX, transform.localPosition.y, transform.localPosition.z);
        SoundSliderValue = ((transform.localPosition.x + 9) / 15.0f); // clamp between [0; 1]
        AudioListener.volume = SoundSliderValue;
    }
}
