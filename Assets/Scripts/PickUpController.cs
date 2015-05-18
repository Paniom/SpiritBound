using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUpController : MonoBehaviour
{
    bool got = false;
    GameObject player;
    string playerState;

    GameObject ScorePickup;
    GameObject GemPickup;
    GameObject CoinPickup;
    GameObject WolfPowerPickup;
    GameObject FoxPowerPickup;

    public bool foxCanSee;
    public bool wolfCanSee;
    public bool muskaloCanSee;

    public enum PowerUpType { Score, Gem, Coin, WolfPower, FoxPower };
    public PowerUpType ThisPowerUpType;

    AudioClip pickupSound;

    // Use this for initialization
    void Start()
    {
        pickupSound = Resources.Load<AudioClip>("Sounds/pickup");
        player = GameObject.FindGameObjectWithTag("Player");
        ScorePickup = transform.Find("ScorePickup").gameObject;
        GemPickup = transform.Find("GemPickup").gameObject;
        CoinPickup = transform.Find("CoinPickup").gameObject;
        WolfPowerPickup = transform.Find("WolfPowerPickup").gameObject;
        FoxPowerPickup = transform.Find("FoxPowerPickup").gameObject;
        switch (ThisPowerUpType)
        {
            case PowerUpType.Coin:
                {
                    tag = "Coin";
                    ScorePickup.SetActive(false);
                    GemPickup.SetActive(false);
                    CoinPickup.SetActive(true);
                    WolfPowerPickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);

                    break;
                }
            case PowerUpType.Gem:
                {
                    tag = "Gem";
                    GemPickup.SetActive(true);
                    ScorePickup.SetActive(false);
                    CoinPickup.SetActive(false);
                    WolfPowerPickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);
                    break;
                }
            case PowerUpType.Score:
                {
                    CoinPickup.SetActive(false);
                    GemPickup.SetActive(false);
                    ScorePickup.SetActive(true);
                    WolfPowerPickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);
                    break;
                }
            case PowerUpType.FoxPower:
                {
                    WolfPowerPickup.SetActive(true);
                    GemPickup.SetActive(false);
                    CoinPickup.SetActive(false);
                    ScorePickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);
                    break;
                }
            case PowerUpType.WolfPower:
                {
                    FoxPowerPickup.SetActive(true);
                    GemPickup.SetActive(false);
                    CoinPickup.SetActive(false);
                    WolfPowerPickup.SetActive(false);
                    ScorePickup.SetActive(false);
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != player.GetComponent<PlayerController>().stateMachine.getState())
        {
            playerState = player.GetComponent<PlayerController>().stateMachine.getState();

            switch (playerState)
            {
                case "Muskalo":
                    if (muskaloCanSee)
                    {
                        GetComponent<Collider>().enabled = true;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            foreach (Material mat in rend.materials)
                            {
                                if (mat.HasProperty("_Color"))
                                {
                                    Color m = mat.color;
                                    m.a = 1.0f;
                                    mat.color = m;
                                }
                            }
                        }
                    }
                    else
                    {
                        GetComponent<Collider>().enabled = false;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            foreach (Material mat in rend.materials)
                            {
                                if (mat.HasProperty("_Color"))
                                {
                                    Color m = mat.color;
                                    m.a = 0.25f;
                                    mat.color = m;
                                }
                            }
                        }
                    }
                    break;
                case "Fox":
                    if (foxCanSee)
                    {
                        GetComponent<Collider>().enabled = true;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            foreach (Material mat in rend.materials)
                            {
                                if (mat.HasProperty("_Color"))
                                {
                                    Color m = mat.color;
                                    m.a = 1.0f;
                                    mat.color = m;
                                }
                            }
                        }
                    }
                    else
                    {
                        GetComponent<Collider>().enabled = false;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            foreach (Material mat in rend.materials)
                            {
                                if (mat.HasProperty("_Color"))
                                {
                                    Color m = mat.color;
                                    m.a = 0.25f;
                                    mat.color = m;
                                }
                            }
                        }
                    }
                    break;
                case "Wolf":
                    if (wolfCanSee)
                    {
                        GetComponent<Collider>().enabled = true;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            foreach (Material mat in rend.materials)
                            {
                                if (mat.HasProperty("_Color"))
                                {
                                    Color m = mat.color;
                                    m.a = 1.0f;
                                    mat.color = m;
                                }
                            }
                        }
                    }
                    else
                    {
                        GetComponent<Collider>().enabled = false;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            foreach (Material mat in rend.materials)
                            {
                                if (mat.HasProperty("_Color"))
                                {
                                    Color m = mat.color;
                                    m.a = 0.25f;
                                    mat.color = m;
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!got)
            {
                if (ThisPowerUpType == PowerUpType.WolfPower || ThisPowerUpType == PowerUpType.FoxPower)
                {
                    SetLastSpawn.PiecesToReset.Add(gameObject);
                }
                got = true;
                other.SendMessage("PickUp", ThisPowerUpType.ToString(), SendMessageOptions.DontRequireReceiver);
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                gameObject.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        got = false;
    }
}
