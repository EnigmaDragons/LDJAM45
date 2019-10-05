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

    private GameObject activePanel;                         
    private MenuObject activePanelMenuObject;
    private EventSystem eventSystem;



    private void SetSelection(GameObject panelToSetSelected)
    {
        //activePanel = panelToSetSelected;
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
        menuPanel.SetActive(false);

	    activePanel = optionsPanel;
	    //SetSelection(optionsPanel);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
		optionsTint.SetActive(false);

	    activePanel = menuPanel;
	}

    //Call this function to activate and display the Options panel during the main menu
    public void ShowCreditsPanel()
    {
        creditsPanel.SetActive(true);
        creditsTint.SetActive(true);
        menuPanel.SetActive(false);

        activePanel = creditsPanel;
        //SetSelection(creditsPanel);
    }

    //Call this function to deactivate and hide the Options panel during the main menu
    public void HideCreditsPanel()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        creditsTint.SetActive(false);

        activePanel = menuPanel;
    }

    //Call this function to activate and display the main menu panel during the main menu
    public void ShowMenu()
	{
		menuPanel.SetActive(true);

	    activePanel = menuPanel;
        //SetSelection(menuPanel);
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
        //SetSelection(pausePanel);
    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive(false);
		optionsTint.SetActive(false);
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
