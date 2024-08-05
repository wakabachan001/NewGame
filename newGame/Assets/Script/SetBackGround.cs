using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBackGround : MonoBehaviour
{
    RawImage rawImage;
    public List<Texture> bg = new List<Texture>();
    
    // Start is called before the first frame update
    void Start()
    {
        int textureNum = StageSelect.int_StageNumber;
        rawImage = GetComponent<RawImage>();
        rawImage.texture = bg[textureNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
