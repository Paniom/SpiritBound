using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public GameObject PlayerTarget;

    public class RotatingLeft : State<UIController>
    {
        public RotatingLeft()
        {

        }

        public override void OnEnter(UIController owner)
        {
            owner.startTime = Time.time;
            owner.startRotation = owner.transform.rotation;
            owner.startRotation.y += 120;
            owner.endRotation = owner.startRotation;
        }

        public override void Process(UIController owner)
        {
            if (Time.time < owner.startTime + owner.delayTime)
            {
                owner.SpiritUIParent.transform.rotation = Quaternion.Lerp(owner.SpiritUIParent.transform.rotation, owner.endRotation, Time.deltaTime);
            }
            else
            {
                owner.stateMachine.ChangeState(owner.states[0]);
            }
        }

        public override void OnExit(UIController owner)
        {
            owner.PlayerTarget.SendMessage("ChangeLeft");
        }
    }

    public class RotatingRight : State<UIController>
    {
        public RotatingRight()
        {

        }

        public override void OnEnter(UIController owner)
        {
            owner.startTime = Time.time;
            owner.startRotation = owner.transform.rotation;
            owner.startRotation.y += 120;
            owner.endRotation = owner.startRotation;
        }

        public override void Process(UIController owner)
        {
            if (Time.time < owner.startTime + owner.delayTime)
            {
                owner.SpiritUIParent.transform.rotation = Quaternion.Lerp(owner.SpiritUIParent.transform.rotation, owner.endRotation, Time.deltaTime);
            }
            else
            {
                owner.stateMachine.ChangeState(owner.states[0]);
            }
        }

        public override void OnExit(UIController owner)
        {
            owner.PlayerTarget.SendMessage("ChangeRight");
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

    public GameObject SpiritUIParent;
    public GameObject[] spiritUI;
    public GameObject foxPowerLevelUI;
    public GameObject wolfPowerLevelUI;
    public float wolfSpiritPowerLevel = 20;
    public float foxSpiritPowerLevel = 20;

    float foxSpiritStartPowerLevel;
    float wolfSpiritStartPowerLevel;

    float startTime;
    float delayTime = 2.0f;

    Quaternion startRotation;
    Quaternion endRotation;

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

    void ChangeState(int state)
    {
        stateMachine.ChangeState(states[state]);
    }
}
