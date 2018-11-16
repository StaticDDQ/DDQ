using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PickUp : MonoBehaviour {

	private Transform mainCam;
	[SerializeField] private float dist;
    [SerializeField] private float rotSpeed = 80;
    [SerializeField] private float rayLength = 2f;

    private GameObject carriedObj;
    public bool pressAgain = false;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    void setIsCarry(){
        if (Input.GetKeyDown(KeyCode.E))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, rayLength))
            {
                PickUpable p = hit.collider.GetComponent<PickUpable>();
                if(p != null)
                {
                    IndicatorMethod._instance.EnableIndicator(false);

                    p.carrying = true;

                    if (hit.collider.tag == "pickUp")
                    {
                        bool canAdd = (!pressAgain && ItemDB._instance.AddItem(hit.collider.gameObject.GetComponent<Item>()));
                        Grab(hit.collider.gameObject, canAdd);
                    }
                    else
                    {
                        SetObjectCarry(hit.collider.gameObject);
                        hit.collider.gameObject.layer = 11;
                    }
                }
            }
        }
	}

	// Update is called once per frame
	void Update () {

		if (carriedObj != null && carriedObj.GetComponent<PickUpable>().carrying) {
            Carry(carriedObj);
            CheckDrop();
		} else
        {
            setIsCarry();
        }
	}

    void Carry(GameObject obj)
    {
        obj.transform.position = Vector3.Lerp(obj.transform.position, mainCam.position + mainCam.forward * dist, Time.deltaTime * rotSpeed);
        if(obj.tag != "pickUp")
        {
            obj.transform.LookAt(obj.transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
        }
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carriedObj.tag == "pickUp")
            {
                Grab(carriedObj, false);
            }
            DropObj();
        }
    }

    void DropObj()
    {
        IndicatorMethod._instance.EnableIndicator(true);
        carriedObj.GetComponent<PickUpable>().carrying = false;
        carriedObj.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObj.transform.SetParent(null);
        carriedObj.layer = 0;
        carriedObj = null;
    }

    void SetObjectCarry(GameObject obj)
    {
        carriedObj = obj;
        carriedObj.GetComponent<Rigidbody>().useGravity = false;
        carriedObj.transform.SetParent(this.transform);
    }

	public void Grab(GameObject hitCollider, bool canGrab)
    {

        IndicatorMethod._instance.EnableIndicator(!canGrab);
        DisableInputs.ButtonsEnabled = !canGrab;
        mainCam.GetComponent<Blur>().enabled = canGrab;

        if (pressAgain)
        {
            hitCollider.SetActive(false);
            pressAgain = false;
            return;
        }

        pressAgain = canGrab;

        SetObjectCarry(hitCollider);
        carriedObj.GetComponent<PickUpable>().canRotate = canGrab;
        carriedObj.GetComponent<Collider>().enabled = !canGrab;

        if (canGrab)
        {
            hitCollider.layer = LayerMask.NameToLayer("pickUp");
        }
    }
}
