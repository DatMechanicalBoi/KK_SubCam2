#define DEBUG

#undef DEBUG


using System.Linq;

using System.Collections;

using System.ComponentModel;


using BepInEx;


using UnityEngine;

using UnityEngine.SceneManagement;

using ConfigurationManager;


#if DEBUG
    using BepInEx.Logging;
#endif



[BepInPlugin(nameof(KK_SubCam), nameof(KK_SubCam), "1.0")]
public class KK_SubCam : BaseUnityPlugin
{

    private Camera mainCamera;

    private GameObject subCameraObject;

    private Camera SubCamera;

    private bool SubCam_Enabled = false;

    private bool SubCam_Control_Enabled = false;

    
    #region Config properties

    [DisplayName("Activation")]
    [Description("Sets it in motion.")]
    public static SavedKeyboardShortcut SubCam_EnableKey { get; private set; }

    [DisplayName("Activation")]
    [Description("Sets it in motion.")]
    public static SavedKeyboardShortcut SubCam_Control_EnableKey { get; private set; }

    #endregion

    void Awake()
    {

        SubCam_EnableKey = new SavedKeyboardShortcut("SubCam key", this, new KeyboardShortcut(KeyCode.Keypad1));

        SubCam_Control_EnableKey = new SavedKeyboardShortcut("SubCam Control key", this, new KeyboardShortcut(KeyCode.Keypad2));

    }

    void Update()
    {

        if (SubCam_EnableKey.IsDown())
        {

            SubCam_Enabled = !SubCam_Enabled;


            if (SubCam_Enabled)
            {

                mainCamera = Camera.main;

                SubCam_Init();

            }
            else
            {
                SubCam_Kill();
            }

        }

        if (SubCam_Control_EnableKey.IsDown())
        {
            SubCam_Control_Enabled = !SubCam_Control_Enabled;

            if (SubCam_Control_Enabled)
            {

                CamControl_Init();

            }
            else
            {

            
            }

        }

    }
    void SubCam_Init()
    {

        subCameraObject = GameObject.Instantiate(mainCamera.gameObject);

        SubCamera = subCameraObject.GetComponent<Camera>();

        SubCamera.CopyFrom(mainCamera);

        SubCamera.rect = new Rect(0.75f, 0.75f, 0.75f, 1.0f);



    }

    void SubCam_Kill()
    {

        SubCam_Enabled = false;

        mainCamera.enabled = true;

        if (subCameraObject != null)
        {
            GameObject.DestroyImmediate(subCameraObject);
        }

    }

    void CamControl_Init()
    {///// Does nothing yet /////
        var ccv2 = GetComponent<CameraControl_Ver2>();

        ccv2.enabled = false;

    }


    void OnDestroy()
    {
        if (SubCam_Enabled)
        {
            SubCam_Kill();
        }
    }




}