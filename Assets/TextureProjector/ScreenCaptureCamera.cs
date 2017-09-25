using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenCaptureCamera : MonoBehaviour
{

    // # 設計方針
    // Quad の自動生成も、カメラの自動生成も可能ですが辞めました。
    // Layer の設定や、Material, RenderTexture の受け渡し方法を環境に合わせて変更できるようにするためです。
    // 自動生成すると、アクセスするためのプロパティやメソッドを用意する必要があり、コストが高そうです。

    #region Field

    /// <summary>
    /// スクリーンをキャプチャするカメラ。
    /// </summary>
    protected Camera screenCaptureCamera;

    /// <summary>
    /// スクリーンにする Quad。
    /// </summary>
    public GameObject screenQuad;

    /// <summary>
    /// 前回のスクリーンのサイズ。
    /// </summary>
    protected Vector3 previsouScreenQuadScale;

    #endregion Field

    #region Method

    /// <summary>
    /// 初期化時に呼び出されます。
    /// </summary>
    protected void Awake()
    {
        this.screenCaptureCamera = base.GetComponent<Camera>();
        InitializeSettings();
    }

    /// <summary>
    /// 更新時に呼び出されます。
    /// </summary>
    protected void Update()
    {
        if (this.previsouScreenQuadScale != this.screenQuad.transform.localScale)
        {
            InitializeSettings();
        }
    }

    /// <summary>
    /// 設定を初期化します。
    /// </summary>
    protected virtual void InitializeSettings()
    {
        Vector3 screenQuadScale = this.screenQuad.transform.localScale;
        this.previsouScreenQuadScale = screenQuadScale;

        // NOTE:
        // rect を設定してから、orthographicSize を設定する必要があります。
        // 任意のアスペクト比を指定して強制する必要があります。
        // 指定しないとき RenderTexture や、ScreenSize の影響を受けてしまうためです。

        this.screenCaptureCamera.orthographic = true;
        this.screenCaptureCamera.rect = new Rect(0, 0, screenQuadScale.x, screenQuadScale.y);
        this.screenCaptureCamera.orthographicSize = this.screenCaptureCamera.rect.height / 2;
        this.screenCaptureCamera.aspect = screenQuadScale.x / screenQuadScale.y;
    }

    #endregion Method
}