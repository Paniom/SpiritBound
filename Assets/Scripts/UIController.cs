using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    public GameObject PlayerTarget;
    
    public class RotatingLeft : State<UIController>
    {
        Quaternion start = Quaternion.identity;
        Quaternion end = Quaternion.identity;
        float count = 0;
        float totalTime = 1;

        public RotatingLeft()
        {
            
        }

        public override void OnEnter(UIController owner)
        {
            owner.PlayerTarget.SendMessage("SwitchToStandby");
            int s = Mathf.RoundToInt(owner.SpiritUIParent.transform.rotation.eulerAngles.y);
            int e = s - 120;
            count = 0;
            totalTime = Mathf.Abs(s - e) / 90f;
            start = Quaternion.Euler(0, s, 0);
            end = Quaternion.Euler(0, e, 0);

            owner._isLerping = true;
        }

        public override void Process(UIController owner)
        {
            Quaternion current = Quaternion.Slerp(start, end, count);
            count = Mathf.Clamp01(count + Time.deltaTime / totalTime);
            if (count == 1)
            {
                owner.SpiritUIParent.transform.localEulerAngles = end.eulerAngles;
                owner.stateMachine.ChangeState(owner.states[0]);
            }
            owner.SpiritUIParent.transform.localRotation = current;
            foreach (Transform child in owner.SpiritUIParent.transform)
            {
                child.transform.rotation = Quaternion.identity;
            }
        }

        public override void OnExit(UIController owner)
        {
            owner._isLerping = false;
            owner.PlayerTarget.SendMessage("ChangeLeft");
        }
    }

    public class RotatingRight : State<UIController>
    {
        Quaternion start = Quaternion.identity;
        Quaternion end = Quaternion.identity;
        float count = 0;
        float totalTime = 1;

        public RotatingRight()
        {

        }

        public override void OnEnter(UIController owner)
        {
            owner.PlayerTarget.SendMessage("SwitchToStandby");
            int s = Mathf.RoundToInt(owner.SpiritUIParent.transform.rotation.eulerAngles.y);
            int e = s + 120;
            count = 0;
            totalTime = Mathf.Abs(s - e) / 90f;
            start = Quaternion.Euler(0, s, 0);
            end = Quaternion.Euler(0, e, 0);

            owner._isLerping = true;
        }

        public override void Process(UIController owner)
        {
            Quaternion current = Quaternion.Slerp(start, end, count);
            count = Mathf.Clamp01(count + Time.deltaTime / totalTime);
            if (count == 1)
            {
                owner.SpiritUIParent.transform.localEulerAngles = end.eulerAngles;
                owner.stateMachine.ChangeState(owner.states[0]);
            }
            owner.SpiritUIParent.transform.localRotation = current;
            foreach (Transform child in owner.SpiritUIParent.transform)
            {
                child.transform.rotation = Quaternion.identity;
            }
        }

        public override void OnExit(UIController owner)
        {
            owner._isLerping = false;
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

    public bool _isLerping = false;

    // Use this for initialization
    void Start()
    {
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
