using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 渲染模式
/// </summary>
public enum RenderingMode
{
    Opaque,         //不透明
    Cutout,         //镂空
    Fade,           //渐隐
    Transparent,    //透明
}

public class GhostEffect : MonoBehaviour
{
    [Header("是否开启残影效果")]
    public bool openGhostEffect;

    [Header("显示残影的持续时间")]
    public float durationTime;

    [Header("生成残影与残影之间的时间间隔")]
    public float spawnTimeval;

    [Header("渲染类型")]
    public RenderingMode mode;

    //生成残影的时间计时器
    private float spawnTimer;

    [Header("残影颜色(注意透明度)")]
    public Color ghostColor;

    private SkinnedMeshRenderer smr;
    private MeshRenderer mr;
    private MeshFilter mf;

    //残影列表
    private List<Ghost> ghostList = new List<Ghost>();

    private void Awake()
    {
        smr = GetComponent<SkinnedMeshRenderer>();

        mf = GetComponent<MeshFilter>();



    }

    private void Update()
    {
        if (openGhostEffect == false)
        {
            ghostList.Clear();
            return;
        }

        CreateGhost();
        DrawGhost();
    }


    // 创建残影
    private void CreateGhost()
    {
        //创建出残影并加入到残影列表中
        if (spawnTimer >= spawnTimeval)
        {
            spawnTimer = 0;

            //获取当前mesh
            Mesh mesh = new Mesh();
            smr.BakeMesh(mesh);


            //获取当前材质           
            Material mat = new Material(smr.material);

            //上色
            mat.color = ghostColor;

            //设置渲染模式
            SetMaterialRenderingMode(mat, mode);

            //加入残影列表
            ghostList.Add(new Ghost(mesh, mat, transform.localToWorldMatrix, Time.realtimeSinceStartup));
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    // 绘制残影
    private void DrawGhost()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            //print(ghostList.Count);
            float time = Time.realtimeSinceStartup - ghostList[i].beginTime;
            if (time >= durationTime)
            {
                //超时移除
                Ghost _ghost = ghostList[i];
                ghostList.Remove(_ghost);
                Destroy(_ghost);
            }
            else
            {
                //褪色效果
                float fadePerSecond = (ghostList[i].mat.color.a / durationTime);
                Color tempColor = ghostList[i].mat.color;
                tempColor.a -= fadePerSecond * Time.deltaTime;
                ghostList[i].mat.color = tempColor;

                Graphics.DrawMesh(ghostList[i].mesh, ghostList[i].matrix, ghostList[i].mat, gameObject.layer);

            }
        }
    }

    // 设置纹理渲染模式
    private void SetMaterialRenderingMode(Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}

//残影数据类
public class Ghost : Object
{
    public Mesh mesh;
    public Material mat;
    public Matrix4x4 matrix;
    public float beginTime;

    public Ghost(Mesh _mesh, Material _mat, Matrix4x4 _matrix, float _beginTime)
    {
        mesh = _mesh;
        mat = _mat;
        matrix = _matrix;
        beginTime = _beginTime;
    }
}