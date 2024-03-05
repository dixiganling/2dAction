using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkSetting : MonoBehaviour
{
    private Camera camera;
    public GameObject Mountains;
    public GameObject G1;
    private Material moun;
    private Material g1;
    private float offsetx;
    public float mounSpeed;
    public float g1Speed;
    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponent<Camera>();
        moun = Mountains.GetComponent<MeshRenderer>().material;
        g1 = G1.GetComponent<MeshRenderer>().material;
        offsetx = Camera.main.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float a = Camera.main.transform.position.x - offsetx;
        moun.mainTextureOffset = new Vector2(a*mounSpeed,moun.mainTextureOffset.y);
        g1.mainTextureOffset = new Vector2(a * g1Speed, moun.mainTextureOffset.y);
    }
}
