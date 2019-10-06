using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;                           //Store a reference to the Game Object PausePanel 
    public GameObject creditsPanel;
    public GameObject creditsTint;
    public GameObject menuButtons;

    private GameObject activePanel;                         
    private MenuObject activePanelMenuObject;
    private EventSystem eventSystem;



    private void SetSelection(GameObject panelToSetSelected)
    {
        activePanelMenuObject = activePanel.GetComponent<MenuObject>();

        if (activePanelMenuObject != null)
        {
            activePanelMenuObject.SetFirstSelected();
        }

        menuSelected = true;
    }

    public void Start()
    {       
        HideCreditsPanel();
        HideOptionsPanel();
        HidePausePanel();
    }

    //Call this function to activate and display the Options panel during the main menu
    public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
        menuButtons.SetActive(false);

        activePanel = optionsPanel;
	    menuSelected = false;
    }

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
	    menuButtons.SetActive(true);
        optionsPanel.SetActive(false);
		optionsTint.SetActive(false);

	    activePanel = menuPanel;
	    menuSelected = false;
    }

    //Call this function to activate and display the Options panel during the main menu
    public void ShowCreditsPanel()
    {
        creditsPanel.SetActive(true);
        creditsTint.SetActive(true);
        menuButtons.SetActive(false);

        activePanel = creditsPanel;
        menuSelected = false;
    }

    //Call this function to deactivate and hide the Options panel during the main menu
    public void HideCreditsPanel()
    {
        menuButtons.SetActive(true);
        creditsPanel.SetActive(false);
        creditsTint.SetActive(false);

        activePanel = menuPanel;
        menuSelected = false;
    }

    //Call this function to activate and display the main menu panel during the main menu
    public void ShowMenu()
	{
		menuPanel.SetActive(true);

	    activePanel = menuPanel;
	    menuSelected = false;
    }

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive(false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive(true);
		optionsTint.SetActive(true);

	    activePanel = pausePanel;
	    menuSelected = false;
    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive(false);
		optionsTint.SetActive(false);

        menuSelected = false;
    }

    private bool menuSelected = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            menuSelected = false;
            return; //ignore mouse click
        }        

        if (Input.anyKeyDown && !menuSelected)
        {
            SetSelection(activePanel);
        }
    }
}
