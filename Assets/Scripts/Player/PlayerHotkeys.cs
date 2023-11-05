using UnityEngine;
using System.Collections;

public class PlayerHotkeys : MonoBehaviour
{	
	public static PlayerHotkeys Instance;
	public static bool isClickingATarget = false;
	public GameObject lumos;
	public GameObject broom;

    private void Awake() {
        Instance = this;
    }

    void Update () {

        if (Chat.Instance.isWritting) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Menu.Instance.togglePanel("ConfigPanel");
        }
	    if (Input.GetKeyDown (KeyCode.F1)) {
                GameObject.Find ("Canvas/TopMenu/Config").GetComponent<ConfigMenu> ().dev.SetActive (true);
	    }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            string now = System.DateTime.Now.ToString("_d-MMM-yyyy-HH-mm-ss-f");
            ScreenCapture.CaptureScreenshot("./Screenshots/" + string.Format("Screenshot{0}.png", now));

            Chat.Instance.LocalMsg("<color=\"#e8bf00\">[Sistema]</color> Captura guardada como Screenshot" + now + ".png");
        }
        // Display/hide the UI
        if (Input.GetKeyDown(KeyCode.Z)) {
            Menu.Instance.gameObject.SetActive(!Menu.Instance.gameObject.GetActive());
        }

		if (Input.GetKeyDown (KeyCode.B) && Camera.main.GetComponent<CameraController>().isMounted != true) {
			Menu.Instance.togglePanel("BagPanel");
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			Menu.Instance.togglePanel("CharacterPanel");
		}
        if (Input.GetKeyDown(KeyCode.T)) {
            Chat.Instance.input2.ActivateInputField();
        }

		if(Input.GetKeyDown(KeyCode.Q)){
            toggleLight();
        }

		if(Input.GetKeyDown(KeyCode.E)){
            Interract();
		}

		if(Input.GetKeyDown(KeyCode.Tab)){
            toggleBroomStick();
		}


        if (Player.Instance.target)
		{
			// unselect target
			if (Input.GetKeyUp(KeyCode.Mouse0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
				if(isClickingATarget) {
					isClickingATarget = false;
				} else {
					Player.Instance.target = null;
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				PlayerCombat.Instance.spellCast(0);
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				PlayerCombat.Instance.spellCast(1);
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				PlayerCombat.Instance.spellCast(2);
			}
		}
	}

	public void Interract() {

	}

    public void toggleLight () {
        lumos.SetActive(!lumos.activeSelf);
    }

    public void toggleBroomStick () {
        if (!broom.activeSelf) {
            if ((!PlayerCombat.Instance.castingSpell) && (!Player.Instance.isInCombat) && (!PlayerPanel.Instance.castingPanel.isCasting)) {
                StartCoroutine("Broom");
            }
        } else {
						Camera.main.GetComponent<CameraController>().mouseXSpeedMod = 5.0f;
						Camera.main.GetComponent<CameraController>().mouseYSpeedMod = 3.0f;
						Camera.main.GetComponent<CameraController>().isMounted = false;

            gameObject.GetComponent<Animator>().SetBool("Broomstick", false);
            gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
            gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<BroomstickControl>().enabled = false;
            broom.SetActive(!broom.activeSelf);
            transform.localEulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

	IEnumerator Broom(){
		PlayerPanel.Instance.castingPanel.Cast ("Escoba voladora", 3);

		gameObject.GetComponent<BroomstickControl> ().curSpeed = 0.0f;

		Player.Instance.anim.SetBool("InvokeSpell", true);
		yield return new WaitForSeconds(1);
		Player.Instance.anim.SetInteger("SpellType", 1);
		Player.Instance.anim.SetBool("InvokeSpell", false);

		yield return new WaitForSeconds(2);
		Camera.main.GetComponent<CameraController>().mouseXSpeedMod = 1.2f;
		Camera.main.GetComponent<CameraController>().mouseYSpeedMod = 1.2f;
		Camera.main.GetComponent<CameraController>().isMounted = true;

		gameObject.transform.position += new Vector3(0, 3, 0);

		gameObject.GetComponent<Animator> ().SetBool ("Broomstick", true);
		gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ().enabled = false;
		gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter> ().enabled = false;
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
		gameObject.GetComponent<BroomstickControl> ().enabled = true;
		broom.SetActive (!broom.activeSelf);
	}
}
