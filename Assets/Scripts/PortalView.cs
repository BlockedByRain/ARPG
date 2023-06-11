using UnityEngine;

public class PortalView : MonoBehaviour
{
    public PortalView otherPortal;
    public Camera portalView;
    public Shader portalShader;

    [SerializeField]
    private MeshRenderer portalMesh;

    private Material portalMaterial;


    // Start is called before the first frame update
    void Start()
    {
        //将另一个portal的相机渲染到这个portal的材质上
        otherPortal.portalView.targetTexture = new RenderTexture(Screen.width,Screen.height,24);
        portalMaterial= new Material(portalShader);    
        portalMaterial.mainTexture = otherPortal.portalView.targetTexture;
        portalMesh.material = portalMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        //将相机的位置转换到另一个portal的坐标系下
        Vector3 lookerPostion = otherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        lookerPostion = new Vector3(-lookerPostion.x,lookerPostion.y,-lookerPostion.z);
        portalView.transform.localPosition = lookerPostion;


        //将相机的旋转转换到另一个portal的坐标系下
        Quaternion diffrence = transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation * Quaternion.Euler(0, 180, 0)));
        portalView.transform.rotation = diffrence * Camera.main.transform.rotation;

        //cliiping

        portalView.nearClipPlane = lookerPostion.magnitude;
    }
}
