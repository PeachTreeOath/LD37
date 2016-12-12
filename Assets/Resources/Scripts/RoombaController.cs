using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoombaController : MonoBehaviour
{
    private Rigidbody2D rb;
    private RoombaData rd;
    public float rotUpgradeMult;

    private Vector3 lastPos;
    private float startTime;

    /// <summary>
    /// Tracks if the roomba is in a reversal state, if it has collided with an object.
    /// </summary>
    private bool isReversing = false;

    /// <summary>
    /// Tracks time duration of a reversal.
    /// </summary>
    private float currReverseTime = 0;

    /// <summary>
    /// Adjusts roomba flyback speed.
    /// </summary>
    private float bounceVelocity = 5;

    /// <summary>
    /// Duration of time in miliseconds to reverse after a collision.
    /// </summary>
    private float reverseDuration = 700;

    private bool isBraking;

    /// <summary>
    /// Flag indicating if the roomba is docking.
    /// </summary>
    private bool isDocking;

    /// <summary>
    /// The time it takes to dock in ms.
    /// </summary>
    private const float DOCK_TIME = 1000;

    private float dockTime = 0;

    private Vector3 dockLocation;

    private float dragControl;

    private SceneTransitionManager sceneManager;

    private bool isDocked = false;

    private Text warning;

    // Use this for initialization
    private void Start()
    {
        MyTime.Instance.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<RoombaData>();
        lastPos = gameObject.transform.position;
        startTime = MyTime.Instance.time;
        dragControl = 0;
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sceneManager = gameManager.GetComponent<SceneTransitionManager>();
        warning = GameObject.Find("RechargeWarning").GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.FindChild("RoombaBody").rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        isBraking = Input.GetKey(KeyCode.Space) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow);
        if (isBraking)
        {
            rb.drag = Mathf.Lerp(0, 10, dragControl);
            if (dragControl < 1)
            {
                dragControl += MyTime.Instance.deltaTime / 5f;
            }
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (isReversing)
            rb.AddForce(transform.up * Mathf.Lerp(rd.maxMoveSpeed + bounceVelocity, rd.minMoveSpeed, (currReverseTime / reverseDuration)));
        else if (isDocking)
        {
            dockTime += MyTime.Instance.deltaTime * 1000;
            // After a set amount of time has passed, jump to the docked position.
            if (dockTime >= DOCK_TIME)
            {
                if (!isDocked)
                {
					transform.position = new Vector3(dockLocation.x, dockLocation.y - 0.11f);
                    MyTime.Instance.timeScale = 0;

                    AudioManager.instance.PlaySound("Buzz");
                    warning.enabled = false;
                    UpgradePanelShowHide.instance.ShowHide(true);
                    isDocked = true;
                }
            }
            else
            {
                // Before the time limit has passed, attempt to lerp to the dock.
                dockTime += MyTime.Instance.deltaTime * 1000;
				transform.position = Vector3.Lerp(transform.localPosition, new Vector3(dockLocation.x, dockLocation.y - 0.11f), dockTime / DOCK_TIME);
            }
        }
        else
        {
            float minSpeed = rd.minMoveSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.SPEED) * 2;
            float maxSpeed = rd.maxMoveSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.SPEED);
            float force = -Mathf.Lerp(minSpeed, maxSpeed, (MyTime.Instance.time - startTime) * rd.accelSpeed);
            if (rd.curBatteryPerc < 0.001f)
            { // floats...
                rb.velocity = Vector3.zero;
                return;
            }
            rb.AddForce(transform.up * force);

            if (isBraking)
            {
                rb.AddForce(transform.up * -force);
            }
            else
            {
                rb.drag = 10;
                dragControl = 0;
            }
        }

        float rSpeed = rd.rotSpeed + UpgradeManager.Instance.GetUpgradeValue(UpgradeManager.UpgradeEnum.TURN_RADIUS) * rotUpgradeMult;
        rb.MoveRotation(rb.rotation - moveHorizontal * rSpeed);

        lastPos = gameObject.transform.position;

        if (isReversing)
        {
            dragControl = 1;
            currReverseTime += MyTime.Instance.deltaTime * 1000;
            if (currReverseTime >= reverseDuration)
            {
                currReverseTime = 0;
                isReversing = false;
                startTime = MyTime.Instance.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger a reverse after collision with obstacle
        if (other.gameObject.CompareTag("Obstacle"))
        {
            bounceVelocity = 2;
            reverseDuration = 500;
            isReversing = true;
			GetComponent<BatteryController>().Damage();
        }
        else if (other.gameObject.CompareTag("AnimalObstacle"))
        {
            bounceVelocity = 7;
            reverseDuration = 700;
            isReversing = true;
            GetComponent<BatteryController>().Damage();
        }
        else if (other.gameObject.CompareTag("Dock"))
        {
            isDocking = true;
			dockLocation = other.transform.position;
            GetComponent<BatteryController>().enabled = false;
        }
    }
}