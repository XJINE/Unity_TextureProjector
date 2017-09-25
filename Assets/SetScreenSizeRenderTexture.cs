using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SetScreenSizeRenderTexture : MonoBehaviour
{
    #region Field

    /// <summary>
    /// RenderTexture に設定する深度値。
    /// 0, 16, 24 のいずれかで指定します。
    /// </summary>
    public int depthSize = 0;

    /// <summary>
    /// RenderTexture を設定するカメラ。
    /// </summary>
    protected new Camera camera;

    /// <summary>
    /// カメラに設定する RenderTexture。
    /// </summary>
    protected RenderTexture renderTexture;

    #endregion Field

    /// <summary>
    /// 初期化時に呼び出されます。
    /// </summary>
    void Awake()
    {
        this.camera = base.GetComponent<Camera>();
        this.renderTexture = new RenderTexture(Screen.width, Screen.height, this.depthSize);
        this.camera.targetTexture = this.renderTexture;
    }

    /// <summary>
    /// 破棄時に呼び出されます。
    /// </summary>
    void OnDestroy()
    {
        if (this.renderTexture != null)
        {
            GameObject.Destroy(this.renderTexture);
            this.renderTexture = null;
        }
    }
}