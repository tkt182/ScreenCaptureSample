using UnityEngine;

public class CameraFilter : MonoBehaviour
{
    [SerializeField]
    private Material filter;

    //nanoKontrol2を宣言
    private NanoKontrol2 nanoKontrol2;
    private int property_width_ID;
    private int property_height_ID;

    void Start()
    {
        //Start()の中でコールバック関数の設定を行う
        nanoKontrol2 = GameObject.Find("NanoKontrol2").GetComponent<NanoKontrol2>();
        nanoKontrol2.valueChangedFunctions.Add(nanoKontrol2_valueChanged);
        nanoKontrol2.keyPushedFunctions.Add(nanoKontrol2_keyPushed);
        property_width_ID = Shader.PropertyToID("_Width");
        property_height_ID = Shader.PropertyToID("_Height");
    }


    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        
        Graphics.Blit(src, dest, filter);
    }

    //キー（スライダー・ノブ）の値が変更された場合に呼び出される関数
    public void nanoKontrol2_valueChanged(string keyName, int keyValue)
    {
        Debug.Log(keyName);
        Debug.Log(keyValue);
        filter.SetInt(property_width_ID, 1920 * keyValue / 255);
        filter.SetInt(property_height_ID, 1080 * keyValue / 255);
    }

    //キーが押された場合に呼び出される関数
    public void nanoKontrol2_keyPushed(string keyName)
    {
        Debug.Log(keyName);
    }

}