using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

    public GameObject moonPowerBar;
    public GameObject moonPowerImage;
    public GameObject moonSprite;

    public GameObject sunPowerBar;
    public GameObject sunPowerImage;
    public GameObject sunSprite;

    public GameObject muskaloSprite;

    public float moonSpiritPowerLevel = 20;
    public float sunSpiritPowerLevel = 20;

    float sunSpiritStartPowerLevel;
    float moonSpiritStartPowerLevel;

    public enum Characters { Moon, Sun, Muskalo }

    Characters CurrentCharcter;

    //GameObject character;

	// Use this for initialization
	void Start () {
        //character = GameObject.Find("ThirdPersonCharacter");
        sunSpiritStartPowerLevel = sunSpiritPowerLevel;
        moonSpiritStartPowerLevel = moonSpiritPowerLevel;
        CurrentCharcter = Characters.Muskalo;
        moonSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
        sunSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        sunPowerImage.GetComponent<Image>().fillAmount = sunSpiritPowerLevel / sunSpiritStartPowerLevel;
        moonPowerImage.GetComponent<Image>().fillAmount = moonSpiritPowerLevel / moonSpiritStartPowerLevel;

        sunPowerBar.GetComponent<Slider>().value = sunSpiritPowerLevel;
        moonPowerBar.GetComponent<Slider>().value = moonSpiritPowerLevel;

        if (CurrentCharcter == Characters.Muskalo)
        {
            //character.GetComponent<ThirdPersonCharacter>().jumpPower = 10;
            //character.GetComponent<ThirdPersonUserControl>().walkByDefault = true;
            if (Input.GetKeyUp(KeyCode.E))
            {
                CurrentCharcter = Characters.Moon;
                muskaloSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
                moonSprite.GetComponent<Image>().color += new Color(0, 0, 0, 0.85f);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                CurrentCharcter = Characters.Sun;
                muskaloSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
                sunSprite.GetComponent<Image>().color += new Color(0, 0, 0, 0.85f);
            }
            if (moonSpiritPowerLevel < moonSpiritStartPowerLevel)
                moonSpiritPowerLevel += Time.deltaTime/2;
            else if (moonSpiritPowerLevel > moonSpiritStartPowerLevel)
                moonSpiritPowerLevel = moonSpiritStartPowerLevel;
            if(sunSpiritPowerLevel < sunSpiritStartPowerLevel)
                sunSpiritPowerLevel += Time.deltaTime/2;
            else if (sunSpiritPowerLevel > sunSpiritStartPowerLevel)
                sunSpiritPowerLevel = sunSpiritStartPowerLevel;
        }
        else if (CurrentCharcter == Characters.Moon)
        {
            //character.GetComponent<ThirdPersonCharacter>().jumpPower = 20;
            //character.GetComponent<ThirdPersonUserControl>().walkByDefault = true;
            if (moonSpiritPowerLevel > 0)
                moonSpiritPowerLevel -= Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.E))
            {
                CurrentCharcter = Characters.Sun;
                moonSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
                sunSprite.GetComponent<Image>().color += new Color(0, 0, 0, 0.85f);
            }
            else if (Input.GetKeyUp(KeyCode.Q) || moonSpiritPowerLevel <= 0)
            {
                CurrentCharcter = Characters.Muskalo;
                muskaloSprite.GetComponent<Image>().color += new Color(0, 0, 0, 0.85f);
                moonSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
            }
        }
        else if (CurrentCharcter == Characters.Sun)
        {
            //character.GetComponent<ThirdPersonCharacter>().jumpPower = 10;
            //character.GetComponent<ThirdPersonUserControl>().walkByDefault = false;
            if (sunSpiritPowerLevel > 0)
                sunSpiritPowerLevel -= Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.E) || sunSpiritPowerLevel <= 0)
            {
                CurrentCharcter = Characters.Muskalo;
                muskaloSprite.GetComponent<Image>().color += new Color(0, 0, 0, 0.85f);
                sunSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                CurrentCharcter = Characters.Moon;
                moonSprite.GetComponent<Image>().color += new Color(0, 0, 0, 0.85f);
                sunSprite.GetComponent<Image>().color += new Color(0, 0, 0, -0.85f);
            }
        }
    }
}
