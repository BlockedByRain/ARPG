using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ��Ⱦģʽ
/// </summary>
public enum RenderingMode
{
    Opaque,         //��͸��
    Cutout,         //�ο�
    Fade,           //����
    Transparent,    //͸��
}

public class GhostEffect : MonoBehaviour
{
    [Header("�Ƿ�����ӰЧ��")]
    public bool openGhostEffect;

    [Header("��ʾ��Ӱ�ĳ���ʱ��")]
    public float durationTime;

    [Header("���ɲ�Ӱ���Ӱ֮���ʱ����")]
    public float spawnTimeval;

    //���ɲ�Ӱ��ʱ���ʱ��
    private float spawnTimer;

    [Header("��Ӱ��ɫ")]
    public Color ghostColor;

    private SkinnedMeshRenderer smr;
    private MeshRenderer mr;
    private MeshFilter mf;

    //��Ӱ�б�
    private List<Ghost> ghostList = new List<Ghost>();

    private void Awake()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        //print(smr.name);
        //print(smr.sharedMesh.name);


        mf = GetComponent<MeshFilter>();

        //mr = GetComponent<MeshRenderer>();


    }

    private void Update()
    {
        if (openGhostEffect == false)
        {
            return;
        }

        CreateGhost();
        DrawGhost();
    }

    /// <summary>
    /// ������Ӱ
    /// </summary>
    private void CreateGhost()
    {
        //��������Ӱ�����뵽��Ӱ�б���
        if (spawnTimer >= spawnTimeval)
        {
            spawnTimer = 0;

            //��ȡ��ǰmesh
            Mesh mesh = new Mesh();
            smr.BakeMesh(mesh);
            //mesh = mf.mesh;


            //��ȡ��ǰ����           
            Material mat = new Material(smr.material);


            print("matcolor =" + mat.color+ "ghostcolor =" + ghostColor);
            //��ɫ
            mat.color = ghostColor;

            print("matcolor =" + mat.color + "ghostcolor =" + ghostColor);



            //������Ⱦģʽ
            SetMaterialRenderingMode(mat, RenderingMode.Fade);

            //�����Ӱ�б�
            ghostList.Add(new Ghost(mesh, mat, transform.localToWorldMatrix, Time.realtimeSinceStartup));
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// ���Ʋ�Ӱ
    /// </summary>
    private void DrawGhost()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            //print(ghostList.Count);
            float time = Time.realtimeSinceStartup - ghostList[i].beginTime;
            if (time >= durationTime)
            {
                //��ʱ�Ƴ�
                Ghost _ghost = ghostList[i];
                ghostList.Remove(_ghost);
                Destroy(_ghost);
            }
            else
            {
                //��ɫЧ��
                float fadePerSecond = (ghostList[i].mat.color.a / durationTime);
                Color tempColor = ghostList[i].mat.color;
                tempColor.a -= fadePerSecond * Time.deltaTime;
                ghostList[i].mat.color = tempColor;

                Graphics.DrawMesh(ghostList[i].mesh, ghostList[i].matrix, ghostList[i].mat, gameObject.layer);
                //print(ghostList[i].mesh.name);
                //print(ghostList[i].matrix);
                //print(ghostList[i].mat);
            }
        }
    }

    /// <summary>
    /// ����������Ⱦģʽ
    /// </summary>
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

/// <summary>
/// ÿһ����Ӱ��������
/// </summary>
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