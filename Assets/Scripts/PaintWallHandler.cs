using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintWallHandler : MonoBehaviour
{
    public ManagerSOScript managerSO;
    public int resolution = 512;
    Texture2D whiteMap;
    public float brushSize;
    public Texture2D brushTexture;
    public GameObject wallPaint;

    Vector2 stored;
    Renderer rend;

    public static Dictionary<Collider, RenderTexture> paintTextures = new Dictionary<Collider, RenderTexture>();

    Texture2D texture2D;
    RenderTexture currentRT;
    RenderTexture renderTexture;
    Texture mainTexture;

    private int changedPixelCount;
    private float paintPercantage;
    private float currentPaintPercantage;
    public Text precantageText;
    void Start()
    {

        CreateClearTexture();// clear white texture to draw on
        rend = wallPaint.GetComponent<Renderer>();
        paintTextures.Add(wallPaint.GetComponent<Collider>(), getWhiteRT());
        rend.material.SetTexture("_PaintMap", paintTextures[wallPaint.GetComponent<Collider>()]);
        mainTexture = rend.material.GetTexture("_PaintMap");

        texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
        currentRT = RenderTexture.active;

        renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);

    }

    void Update()
    {

        Debug.DrawRay(transform.position, Vector3.down * 20f, Color.magenta);
        RaycastHit hit;
        if(Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) // delete previous and uncomment for mouse painting
            {
                Collider coll = hit.collider;
                if (coll != null)
                {
                    if (stored != hit.lightmapCoord) // stop drawing on the same point
                    {
                        stored = hit.lightmapCoord;
                        Vector2 pixelUV = hit.lightmapCoord;
                        pixelUV.y *= resolution;
                        pixelUV.x *= resolution;
                        DrawTexture(paintTextures[coll], pixelUV.x, pixelUV.y);
                    }

                    CalculateDrawingPercantage();
                }
            }
        }
    }

    /// <summary>
    /// Coverts texture provided by shader and casts to it to the texture2d, in this way it provides iterating pixels and check color of each.
    /// </summary>
    void CalculateDrawingPercantage()
    {
        Graphics.Blit(mainTexture, renderTexture);
        changedPixelCount = 0;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        Color[] pixelColors = texture2D.GetPixels();

        foreach(Color pixelColor in pixelColors)
        {
            if (pixelColor.b == 0)
            {
                changedPixelCount++;
            }
        }

        currentPaintPercantage = (100 * changedPixelCount) / pixelColors.Length;
        if (paintPercantage != currentPaintPercantage)
        {
            paintPercantage = currentPaintPercantage;
            managerSO.UpdatePaintingPercentage((int)(paintPercantage));
            //precantageText.text = "%" + paintPercantage + " painted";
        }
        
    }
    
    void DrawTexture(RenderTexture rt, float posX, float posY)
    {
        RenderTexture.active = rt; // activate rendertexture for drawtexture;
        GL.PushMatrix();                       // save matrixes
        GL.LoadPixelMatrix(0, resolution, resolution, 0);      // setup matrix for correct size
        // draw brushtexture
        Graphics.DrawTexture(new Rect(posX - brushTexture.width / brushSize, (rt.height - posY) - brushTexture.height / brushSize, brushTexture.width / (brushSize * 0.5f), brushTexture.height / (brushSize * 0.5f)), brushTexture);
        GL.PopMatrix();
        RenderTexture.active = null;// turn off rendertexture


    }

    RenderTexture getWhiteRT()
    {
        RenderTexture rt = new RenderTexture(resolution, resolution, 32);
        Graphics.Blit(whiteMap, rt);
        return rt;
    }

    void CreateClearTexture()
    {
        whiteMap = new Texture2D(1, 1);
        whiteMap.SetPixel(0, 0, Color.white);
        whiteMap.Apply();
    }

    
}
