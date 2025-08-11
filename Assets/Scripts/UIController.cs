using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private ProceduralGenerator proGen;

    [SerializeField]
    private InstructionsController instructionsController;

    [SerializeField]
    private GameObject verticalUI;

    [SerializeField] 
    private GameObject horizontalUI;

    [SerializeField]
    private GameObject verticalToggleUIButton;

    [SerializeField]
    private GameObject horizontalToggleUIButton;

    [SerializeField]
    private GameObject directionalLight;

    [SerializeField]
    private GameObject instructions;

    [SerializeField]
    private bool isInitialized = false;

    [SerializeField]
    private bool isOutOfScene = false;

    [SerializeField]
    private bool isPortrait = true;

    [SerializeField]
    private bool isToggleUI = true;

    [SerializeField]
    private bool isLightOn = true;

    [SerializeField]
    private bool isShadowOn = true;

    [SerializeField]
    private bool isInstructionsEnabled = false;

    [SerializeField]
    private bool isVerticalInfoEnabled = false;

    [SerializeField]
    private bool isHorizontalInfoEnabled = false;

    [Header("Vertical UI")]
    [SerializeField]
    private Slider verticalRowsSlider;

    [SerializeField]
    private Slider verticalColumnsSlider;

    [SerializeField]
    private Slider verticalNumberOfFloorsSlider;

    [SerializeField]
    private Slider verticalScaleSlider;

    [SerializeField]
    private Slider verticalInstructionsSizeSlider;

    [SerializeField]
    private Toggle verticalRoofToggle;

    [SerializeField]
    private Toggle verticalLightToggle;

    [SerializeField]
    private Toggle verticalShadowToggle;

    [SerializeField]
    private GameObject verticalLimitSlider1;

    [SerializeField]
    private GameObject verticalLimitSlider2;

    [SerializeField]
    private GameObject verticalLimitSlider3;

    [SerializeField]
    private GameObject verticalLimitSlider4;

    [SerializeField]
    private GameObject verticalInfoPanel;

    [SerializeField]
    private GameObject verticalIndicators1;

    [SerializeField]
    private GameObject verticalIndicators2;

    [SerializeField]
    private GameObject verticalInstructionsSizeSliderGameObject;

    [Header("Horizontal UI")]
    [SerializeField]
    private Slider horizontalRowsSlider;

    [SerializeField]
    private Slider horizontalColumnsSlider;

    [SerializeField]
    private Slider horizontalNumberOfFloorsSlider;

    [SerializeField]
    private Slider horizontalScaleSlider;

    [SerializeField]
    private Slider horizontalInstructionsSizeSlider;

    [SerializeField]
    private Toggle horizontalRoofToggle;

    [SerializeField]
    private Toggle horizontalLightToggle;

    [SerializeField]
    private Toggle horizontalShadowToggle;

    [SerializeField]
    private GameObject horizontalLimitSlider1;

    [SerializeField]
    private GameObject horizontalLimitSlider2;

    [SerializeField]
    private GameObject horizontalLimitSlider3;

    [SerializeField]
    private GameObject horizontalLimitSlider4;

    [SerializeField]
    private GameObject horizontalInfoPanel;

    [SerializeField]
    private GameObject horizontalIndicators1;

    [SerializeField]
    private GameObject horizontalIndicators2;

    [SerializeField]
    private GameObject horizontalInstructionsSizeSliderGameObject;

    public Slider VerticalRowsSlider
    {
        get
        {
            return verticalRowsSlider;
        }
    }

    public Slider VerticalColumnsSlider
    {
        get
        {
            return verticalColumnsSlider;
        }
    }

    public Slider VerticalNumberOfFloorsSlider
    {
        get
        {
            return verticalNumberOfFloorsSlider;
        }
    }

    private void Awake()
    {
        InitializeUIValues();
    }

    void Update()
    {
        if (Screen.orientation != ScreenOrientation.AutoRotation)
        {
            //Debug.Log("Device orientation: " + Screen.orientation);

            switch (Screen.orientation)
            {
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    isPortrait = true;
                    if (isToggleUI) verticalUI.SetActive(true);
                    horizontalUI.SetActive(false);
                    verticalToggleUIButton.gameObject.SetActive(true);
                    horizontalToggleUIButton.gameObject.SetActive(false);
                    SetAnotherUIValues();
                    break;

                case ScreenOrientation.LandscapeRight:
                case ScreenOrientation.LandscapeLeft:
                    isPortrait = false;
                    verticalUI.SetActive(false);
                    if (isToggleUI) horizontalUI.SetActive(true);
                    verticalToggleUIButton.gameObject.SetActive(false);
                    horizontalToggleUIButton.gameObject.SetActive(true);
                    SetAnotherUIValues();
                    break;
            }
        }
    }

    public void InitializeUIValues()
    {
        // Switch rows and columns for consistent orientation and better UX
        this.verticalRowsSlider.value = proGen.Columns;
        this.verticalColumnsSlider.value = proGen.Rows;
        this.verticalNumberOfFloorsSlider.value = proGen.NumberOfFloors;
        this.verticalScaleSlider.value = proGen.ScaleMultiplier;
        this.verticalInstructionsSizeSlider.value = proGen.InstructionsSize;
        this.verticalRoofToggle.isOn = proGen.IncludeRoof;
        this.verticalLightToggle.SetIsOnWithoutNotify(isLightOn);
        this.verticalShadowToggle.SetIsOnWithoutNotify(isShadowOn);

        this.verticalLimitSlider1.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.TopRowsUpperLimit);
        this.verticalLimitSlider1.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        this.verticalLimitSlider2.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.RightColumnsUpperLimit);
        this.verticalLimitSlider2.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        this.verticalLimitSlider3.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.BottomRowsUpperLimit);
        this.verticalLimitSlider3.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        this.verticalLimitSlider4.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.LeftColumnsUpperLimit);
        this.verticalLimitSlider4.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        // Switch rows and columns for consistent orientation and better UX
        this.horizontalRowsSlider.value = proGen.Columns;
        this.horizontalColumnsSlider.value = proGen.Rows;
        this.horizontalNumberOfFloorsSlider.value = proGen.NumberOfFloors;
        this.horizontalScaleSlider.value = proGen.ScaleMultiplier;
        this.horizontalInstructionsSizeSlider.value = proGen.InstructionsSize;
        this.horizontalRoofToggle.isOn = proGen.IncludeRoof;
        this.horizontalLightToggle.SetIsOnWithoutNotify(isLightOn);
        this.horizontalShadowToggle.SetIsOnWithoutNotify(isShadowOn);

        this.horizontalLimitSlider1.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.TopRowsUpperLimit);
        this.horizontalLimitSlider1.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        this.horizontalLimitSlider2.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.RightColumnsUpperLimit);
        this.horizontalLimitSlider2.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        this.horizontalLimitSlider3.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.BottomRowsUpperLimit);
        this.horizontalLimitSlider3.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        this.horizontalLimitSlider4.GetComponent<DoubleSlider>().setSlidersUpperLimit(proGen.LeftColumnsUpperLimit);
        this.horizontalLimitSlider4.GetComponent<DoubleSlider>().setSlidersLowerLimit(0);

        this.instructionsController.UpdatePositions();

        this.isInitialized = true;
    }

    public void SetAnotherUIValues()
    {
        DoubleSlider verticalLimitSlider1DS = verticalLimitSlider1.GetComponent<DoubleSlider>();
        DoubleSlider verticalLimitSlider2DS = verticalLimitSlider2.GetComponent<DoubleSlider>();
        DoubleSlider verticalLimitSlider3DS = verticalLimitSlider3.GetComponent<DoubleSlider>();
        DoubleSlider verticalLimitSlider4DS = verticalLimitSlider4.GetComponent<DoubleSlider>();

        DoubleSlider horizontalLimitSlider1DS = horizontalLimitSlider1.GetComponent<DoubleSlider>();
        DoubleSlider horizontalLimitSlider2DS = horizontalLimitSlider2.GetComponent<DoubleSlider>();
        DoubleSlider horizontalLimitSlider3DS = horizontalLimitSlider3.GetComponent<DoubleSlider>();
        DoubleSlider horizontalLimitSlider4DS = horizontalLimitSlider4.GetComponent<DoubleSlider>();

        if (isPortrait)
        {
            verticalLimitSlider1DS.setSlidersUpperLimit(proGen.Rows);
            verticalLimitSlider2DS.setSlidersUpperLimit(proGen.Rows);
            verticalLimitSlider3DS.setSlidersUpperLimit(proGen.Columns);
            verticalLimitSlider4DS.setSlidersUpperLimit(proGen.Columns);

            this.horizontalRowsSlider.value = this.verticalRowsSlider.value;
            this.horizontalColumnsSlider.value = this.verticalColumnsSlider.value;
            this.horizontalNumberOfFloorsSlider.value = this.verticalNumberOfFloorsSlider.value;
            this.horizontalScaleSlider.value = this.verticalScaleSlider.value;
            this.horizontalInstructionsSizeSlider.value = this.verticalInstructionsSizeSlider.value;
            this.horizontalRoofToggle.isOn = this.verticalRoofToggle.isOn;
            this.horizontalLightToggle.SetIsOnWithoutNotify(isLightOn);
            this.horizontalShadowToggle.SetIsOnWithoutNotify(isShadowOn);

            horizontalLimitSlider1DS.setMaxSliderValue(verticalLimitSlider1DS.getMaxSliderValue());
            horizontalLimitSlider2DS.setMaxSliderValue(verticalLimitSlider2DS.getMaxSliderValue());
            horizontalLimitSlider3DS.setMaxSliderValue(verticalLimitSlider3DS.getMaxSliderValue());
            horizontalLimitSlider4DS.setMaxSliderValue(verticalLimitSlider4DS.getMaxSliderValue());

            horizontalLimitSlider1DS.setMinSliderValue(verticalLimitSlider1DS.getMinSliderValue());
            horizontalLimitSlider2DS.setMinSliderValue(verticalLimitSlider2DS.getMinSliderValue());
            horizontalLimitSlider3DS.setMinSliderValue(verticalLimitSlider3DS.getMinSliderValue());
            horizontalLimitSlider4DS.setMinSliderValue(verticalLimitSlider4DS.getMinSliderValue());

            horizontalLimitSlider1DS.setSlidersUpperLimit(proGen.Rows);
            horizontalLimitSlider2DS.setSlidersUpperLimit(proGen.Rows);
            horizontalLimitSlider3DS.setSlidersUpperLimit(proGen.Columns);
            horizontalLimitSlider4DS.setSlidersUpperLimit(proGen.Columns);
        } 
        else
        {
            horizontalLimitSlider1DS.setSlidersUpperLimit(proGen.Rows);
            horizontalLimitSlider2DS.setSlidersUpperLimit(proGen.Rows);
            horizontalLimitSlider3DS.setSlidersUpperLimit(proGen.Columns);
            horizontalLimitSlider4DS.setSlidersUpperLimit(proGen.Columns);

            this.verticalRowsSlider.value = this.horizontalRowsSlider.value;
            this.verticalColumnsSlider.value = this.horizontalColumnsSlider.value;
            this.verticalNumberOfFloorsSlider.value = this.horizontalNumberOfFloorsSlider.value;
            this.verticalScaleSlider.value = this.horizontalScaleSlider.value;
            this.verticalInstructionsSizeSlider.value = this.horizontalInstructionsSizeSlider.value;
            this.verticalRoofToggle.isOn = this.horizontalRoofToggle.isOn;
            this.verticalLightToggle.SetIsOnWithoutNotify(isLightOn);
            this.verticalShadowToggle.SetIsOnWithoutNotify(isShadowOn);

            verticalLimitSlider1DS.setMaxSliderValue(horizontalLimitSlider1DS.getMaxSliderValue());
            verticalLimitSlider2DS.setMaxSliderValue(horizontalLimitSlider2DS.getMaxSliderValue());
            verticalLimitSlider3DS.setMaxSliderValue(horizontalLimitSlider3DS.getMaxSliderValue());
            verticalLimitSlider4DS.setMaxSliderValue(horizontalLimitSlider4DS.getMaxSliderValue());

            verticalLimitSlider1DS.setMinSliderValue(horizontalLimitSlider1DS.getMinSliderValue());
            verticalLimitSlider2DS.setMinSliderValue(horizontalLimitSlider2DS.getMinSliderValue());
            verticalLimitSlider3DS.setMinSliderValue(horizontalLimitSlider3DS.getMinSliderValue());
            verticalLimitSlider4DS.setMinSliderValue(horizontalLimitSlider4DS.getMinSliderValue());

            verticalLimitSlider1DS.setSlidersUpperLimit(proGen.Rows);
            verticalLimitSlider2DS.setSlidersUpperLimit(proGen.Rows);
            verticalLimitSlider3DS.setSlidersUpperLimit(proGen.Columns);
            verticalLimitSlider4DS.setSlidersUpperLimit(proGen.Columns);
        }
    }

    public void SetBuildingValues()
    {
        DoubleSlider verticalLimitSlider1DS = verticalLimitSlider1.GetComponent<DoubleSlider>();
        DoubleSlider verticalLimitSlider2DS = verticalLimitSlider2.GetComponent<DoubleSlider>();
        DoubleSlider verticalLimitSlider3DS = verticalLimitSlider3.GetComponent<DoubleSlider>();
        DoubleSlider verticalLimitSlider4DS = verticalLimitSlider4.GetComponent<DoubleSlider>();

        DoubleSlider horizontalLimitSlider1DS = horizontalLimitSlider1.GetComponent<DoubleSlider>();
        DoubleSlider horizontalLimitSlider2DS = horizontalLimitSlider2.GetComponent<DoubleSlider>();
        DoubleSlider horizontalLimitSlider3DS = horizontalLimitSlider3.GetComponent<DoubleSlider>();
        DoubleSlider horizontalLimitSlider4DS = horizontalLimitSlider4.GetComponent<DoubleSlider>();

        if (isPortrait)
        {
            // Switch rows and columns for consistent orientation and better UX
            proGen.Columns = (int)this.verticalRowsSlider.value;
            proGen.Rows = (int)this.verticalColumnsSlider.value;
            proGen.NumberOfFloors = (int)this.verticalNumberOfFloorsSlider.value;
            proGen.ScaleMultiplier = (float)this.verticalScaleSlider.value;
            proGen.InstructionsSize = (float)this.verticalInstructionsSizeSlider.value;
            proGen.IncludeRoof = (bool)this.verticalRoofToggle.isOn;

            proGen.TopRowsUpperLimit = verticalLimitSlider1DS.getMaxSliderValue();
            proGen.BottomRowsLowerLimit = verticalLimitSlider1DS.getMinSliderValue();

            proGen.BottomRowsUpperLimit = verticalLimitSlider2DS.getMaxSliderValue();
            proGen.TopRowsLowerLimit = verticalLimitSlider2DS.getMinSliderValue();

            proGen.RightColumnsUpperLimit = verticalLimitSlider3DS.getMaxSliderValue();
            proGen.LeftColumnsLowerLimit = verticalLimitSlider3DS.getMinSliderValue();

            proGen.LeftColumnsUpperLimit = verticalLimitSlider4DS.getMaxSliderValue();
            proGen.RightColumnsLowerLimit = verticalLimitSlider4DS.getMinSliderValue();
        } 
        else
        {
            // Switch rows and columns for consistent orientation and better UX
            proGen.Columns = (int)this.horizontalRowsSlider.value;
            proGen.Rows = (int)this.horizontalColumnsSlider.value;
            proGen.NumberOfFloors = (int)this.horizontalNumberOfFloorsSlider.value;
            proGen.ScaleMultiplier = (float)this.horizontalScaleSlider.value;
            proGen.InstructionsSize = (float)this.horizontalInstructionsSizeSlider.value;
            proGen.IncludeRoof = (bool)this.horizontalRoofToggle.isOn;

            proGen.TopRowsUpperLimit = horizontalLimitSlider1DS.getMaxSliderValue();
            proGen.BottomRowsLowerLimit = horizontalLimitSlider1DS.getMinSliderValue();

            proGen.BottomRowsUpperLimit = horizontalLimitSlider2DS.getMaxSliderValue();
            proGen.TopRowsLowerLimit = horizontalLimitSlider2DS.getMinSliderValue();

            proGen.RightColumnsUpperLimit = horizontalLimitSlider3DS.getMaxSliderValue();
            proGen.LeftColumnsLowerLimit = horizontalLimitSlider3DS.getMinSliderValue();

            proGen.LeftColumnsUpperLimit = horizontalLimitSlider4DS.getMaxSliderValue();
            proGen.RightColumnsLowerLimit = horizontalLimitSlider4DS.getMinSliderValue();
        }
    }

    public void RegenerateBuilding()
    {
        if (isInitialized && !isOutOfScene)
        {   
            SetBuildingValues();
            proGen.ReGenerate();
        }
    }

    public void SetOutOfSceneTrue()
    {
        this.isOutOfScene = true;
    }

    public void SetOutOfSceneFalse()
    {
        this.isOutOfScene = false;
    }

    public void RandomizeAll()
    {
        int randomNumber1 = Random.Range(1, 10);
        int randomNumber2 = Random.Range(1, 10);
        int randomNumber3 = Random.Range(1, 10);

        SetBuildingRows(randomNumber1);
        SetBuildingRows(randomNumber2);
        SetBuildingNumberOfFloors(randomNumber3);

        proGen.ReGenerate();
    }

    public void SetBuildingRows(float value)
    {
        proGen.Rows = (int)value;
    }

    public void SetBuildingColumns(float value)
    {
        proGen.Columns = (int)value;
    }

    public void SetBuildingNumberOfFloors(float value)
    {
        proGen.NumberOfFloors = (int)value;
    }
    public void ToggleVerticalUI()
    {
        isToggleUI = !isToggleUI;
        verticalUI.SetActive(isToggleUI);
    }

    public void ToggleHorizontalUI()
    {
        isToggleUI = !isToggleUI;
        horizontalUI.SetActive(isToggleUI);
    }
    public void ToggleLight()
    {
        isLightOn = !isLightOn;
        directionalLight.SetActive(isLightOn);

        if (isLightOn)
        {
            verticalShadowToggle.gameObject.SetActive(true);
            horizontalShadowToggle.gameObject.SetActive(true);
        }
        else
        {
            verticalShadowToggle.gameObject.SetActive(false);
            horizontalShadowToggle.gameObject.SetActive(false);
        }
    }

    public void ToggleShadow()
    {
        isShadowOn = !isShadowOn;
        if (isShadowOn)
        {
            directionalLight.GetComponent<Light>().shadows = LightShadows.Hard;
        } 
        else
        {
            directionalLight.GetComponent<Light>().shadows = LightShadows.None;
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ToggleVerticalInfoPanel()
    {
        isVerticalInfoEnabled = !isVerticalInfoEnabled;
        verticalInfoPanel.SetActive(isVerticalInfoEnabled);
    }

    public void ToggleHorizontalInfoPanel()
    {
        isHorizontalInfoEnabled = !isHorizontalInfoEnabled;
        horizontalInfoPanel.SetActive(isHorizontalInfoEnabled);
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ToggleInstructions()
    {
        isInstructionsEnabled = !isInstructionsEnabled;
        instructions.SetActive(isInstructionsEnabled);
        verticalInstructionsSizeSliderGameObject.SetActive(isInstructionsEnabled);
        horizontalInstructionsSizeSliderGameObject.SetActive(isInstructionsEnabled);
    }

    public void ResetIndicatorsValue()
    {
        if (isPortrait)
        {
            this.verticalIndicators1.GetComponent<SliderIndicators>().ReRender(this.verticalRowsSlider.value);
            this.verticalIndicators2.GetComponent<SliderIndicators>().ReRender(this.verticalColumnsSlider.value);
            this.horizontalIndicators1.GetComponent<SliderIndicators>().ReRender(this.verticalRowsSlider.value);
            this.horizontalIndicators2.GetComponent<SliderIndicators>().ReRender(this.verticalColumnsSlider.value);
        }
        else
        {
            this.verticalIndicators1.GetComponent<SliderIndicators>().ReRender(this.horizontalRowsSlider.value);
            this.verticalIndicators2.GetComponent<SliderIndicators>().ReRender(this.horizontalColumnsSlider.value);
            this.horizontalIndicators1.GetComponent<SliderIndicators>().ReRender(this.horizontalRowsSlider.value);
            this.horizontalIndicators2.GetComponent<SliderIndicators>().ReRender(this.horizontalColumnsSlider.value);
        }
    }

    public void ScaleInstructions()
    {
        if (isPortrait)
        {
            float value = this.verticalInstructionsSizeSlider.value;
            instructions.transform.localScale = new Vector3(value, value, value);
        }
        else
        {
            float value = this.horizontalInstructionsSizeSlider.value;
            instructions.transform.localScale = new Vector3(value, value, value);
        }
    }

}
