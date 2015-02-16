using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    public class RotatingLeft : State<UIController>
    {
        public RotatingLeft()
        {
            
        }

        public override void OnEnter(UIController owner)
        {
            
        }

        public override void Process(UIController owner)
        {
            
        }

        public override void OnExit(UIController owner)
        {
            
        }
    }


    public class RotatingRight : State<UIController>
    {

        public RotatingRight()
        {
            
        }

        public override void OnEnter(UIController owner)
        {
            
        }

        public override void Process(UIController owner)
        {
            
        }

        public override void OnExit(UIController owner)
        {
            
        }
    }

    public class Stationary : State<UIController>
    {

        public Stationary()
        {
            
        }

        public override void OnEnter(UIController owner)
        {
            
        }

        public override void Process(UIController owner)
        {
            
        }

        public override void OnExit(UIController owner)
        {
            
        }
    }

    public State<UIController>[] states = new State<UIController>[] {	new Stationary(),
																				new RotatingLeft(),
                                                                                new RotatingRight()};
    public StateMachine<UIController> stateMachine = new StateMachine<UIController>();

    public GameObject muskaloUI;
    public GameObject wolfUI;
    public GameObject foxUI;
    public GameObject foxPowerLevelUI;
    public GameObject wolfPowerLevelUI;
    public float wolfSpiritPowerLevel = 20;
    public float foxSpiritPowerLevel = 20;

    float foxSpiritStartPowerLevel;
    float wolfSpiritStartPowerLevel;

    // Use this for initialization
    void Start()
    {
        foxSpiritStartPowerLevel = foxSpiritPowerLevel;
        wolfSpiritStartPowerLevel = wolfSpiritPowerLevel;
        stateMachine.Configure(this, states[0]);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    void Left()
    {

    }

    void Right()
    {

    }
}
