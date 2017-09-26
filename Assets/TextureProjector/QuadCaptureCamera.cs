using UnityEngine;

[RequireComponent(typeof(Camera))]
public class QuadCaptureCamera : MonoBehaviour
{
    // # 設計方針
    // Quad の自動生成も、カメラの自動生成も可能ですが、このクラスでは採用しません。
    // Layer の設定や、Material, RenderTexture の受け渡し方法を環境に合わせて変更できるようにするためです。
    // 自動生成すると、アクセスするためのプロパティやメソッドを用意する必要があり、コストが高そうです。

    #region Field

    /// <summary>
    /// スクリーンをキャプチャするカメラ。
    /// </summary>
    protected Camera quadCaptureCamera;

    /// <summary>
    /// スクリーンにする Quad。
    /// </summary>
    public GameObject quad;

    /// <summary>
    /// 前回のスクリーンのサイズ。
    /// </summary>
    protected Vector3 previousQuadScale;

    #endregion Field

    #region Method

    /// <summary>
    /// 初期化時に呼び出されます。
    /// </summary>
    protected void Awake()
    {
        this.quadCaptureCamera = base.GetComponent<Camera>();
        InitializeSettings(this.quad.transform.localScale);
    }

    /// <summary>
    /// 更新時に呼び出されます。
    /// </summary>
    protected void Update()
    {
        Vector3 quadScale = this.quad.transform.localScale;

        if (this.previousQuadScale != quadScale)
        {
            InitializeSettings(quadScale);
        }
    }

    /// <summary>
    /// 設定を初期化します。
    /// </summary>
    protected virtual void InitializeSettings(Vector3 quadScale)
    {
        this.previousQuadScale = quadScale;

        // NOTE:
        // rect を設定してから、orthographicSize を設定する必要があります。
        // 任意のアスペクト比を指定して強制する必要があります。
        // 指定しないとき RenderTexture や、ScreenSize の影響を受けてしまうためです。

        this.quadCaptureCamera.orthographic = true;
        this.quadCaptureCamera.rect = new Rect(0, 0, quadScale.x, quadScale.y);
        this.quadCaptureCamera.orthographicSize = this.quadCaptureCamera.rect.height / 2;
        this.quadCaptureCamera.aspect = quadScale.x / quadScale.y;
    }

    #endregion Method
}