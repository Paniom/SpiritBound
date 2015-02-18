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
            owner._timeStartedLerping = Time.time;
            owner._isLerping = true;
            owner.startRotation = owner.SpiritUIParent.transform.localEulerAngles;
            owner.endRotation = owner.startRotation;
            owner.endRotation.y = owner.startRotation.y - 120;
            owner.endRotation.y = owner.endRotation.y < 0 ? 360 + owner.endRotation.y % 360 : owner.endRotation.y % 360;
        }

        public override void Process(UIController owner)
        {
            if (owner._isLerping)
            {
                Vector3 TempRotation = owner.SpiritUIParent.transform.localEulerAngles;
                TempRotation.y = Mathf.LerpAngle(owner.SpiritUIParent.transform.localEulerAngles.y, owner.endRotation.y, Time.deltaTime*2);
                owner.SpiritUIParent.transform.localEulerAngles = TempRotation;

                //When completed the lerp, set _isLerping to false
                if (Mathf.Abs(owner.SpiritUIParent.transform.localEulerAngles.y - owner.endRotation.y) < 1.5f)
                {
                    owner.SpiritUIParent.transform.localEulerAngles = owner.endRotation;
                    owner._isLerping = false;
                }
            }
            else
            {
                owner.stateMachine.ChangeState(owner.states[0]);
            }
        }

        public override void OnExit(UIController owner)
        {
            //owner._isLerping = false;
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
            owner._timeStartedLerping = Time.time;
            owner._isLerping = true;
            owner.startRotation = owner.SpiritUIParent.transform.localEulerAngles;
            owner.endRotation = owner.startRotation;
            owner.endRotation.y = owner.startRotation.y + 120;
            if(owner.endRotation.y != 360)
                owner.endRotation.y = owner.endRotation.y < 0 ? 360 + owner.endRotation.y % 360 : owner.endRotation.y % 360;
        }

        public override void Process(UIController owner)
        {
            if (owner._isLerping)
            {
                Vector3 TempRotation = owner.SpiritUIParent.transform.localEulerAngles;
                TempRotation.y = Mathf.LerpAngle(owner.SpiritUIParent.transform.localEulerAngles.y, owner.endRotation.y, Time.deltaTime*2);
                owner.SpiritUIParent.transform.localEulerAngles = TempRotation;

                //When completed the lerp, set _isLerping to false
                if (Mathf.Abs(owner.SpiritUIParent.transform.localEulerAngles.y - owner.endRotation.y) < 1.5f)
                {
                    owner.SpiritUIParent.transform.localEulerAngles = owner.endRotation;
                    owner._isLerping = false;
                }
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
																		new RotatingRight() };
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

    float timeTakenDuringLerp = 2.0f;
    public bool _isLerping = false;
    float _timeStartedLerping;

    Vector3 startRotation;
    Vector3 endRotation;

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
