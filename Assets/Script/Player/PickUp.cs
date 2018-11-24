using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PickUp : MonoBehaviour {

	private Transform mainCam;
	[SerializeField] private float dist;
    [SerializeField] private float rotSpeed = 80;
    [SerializeField] private float rayLength = 2f;

    private GameObject carriedObj;
    public bool pressAgain = false;
    private bool isCarrying = false;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    void setIsCarry(){
        if (InputChecker.instance.ButtonsEnabled && Input.GetKeyDown(KeyCode.E))
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
                    if (hit.collider.tag == "pickUp")
                    {
                        Grab(hit.collider.gameObject);
                    }
                    else
                    {
                        isCarrying = !hit.collider.GetComponent<PickUpable>().carrying;
                        SetObjectCarry(hit.collider.gameObject,isCarrying);
                    }
                }
            }
        }
	}

	// Update is called once per frame
	void Update () {

		if (carriedObj != null && carriedObj.GetComponent<PickUpable>().carrying) {
            carriedObj.transform.position = Vector3.Lerp(carriedObj.transform.position, mainCam.position + mainCam.forward * dist, Time.deltaTime * rotSpeed);
            if(carriedObj.tag != "pickUp")
            {
                carriedObj.transform.LookAt(carriedObj.transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
            } else if (Input.GetKeyDown(KeyCode.E))
            {
                Grab(carriedObj);
            }
		} else
        {
            setIsCarry();
        }
	}

    private void SetObjectCarry(GameObject obj, bool isCarry)
    {
        IndicatorMethod._instance.EnableIndicator(!isCarry);
        obj.GetComponent<Rigidbody>().useGravity = !isCarry;
        obj.GetComponent<PickUpable>().carrying = isCarry;

        if (isCarry)
        {
            carriedObj = obj;
            carriedObj.transform.SetParent(this.transform);
        }
        else
        {
            carriedObj.transform.SetParent(null);
            carriedObj = null;
        }
    }

	private void Grab(GameObject hitCollider)
    {
        if (!pressAgain)
        {
            bool canGrab = ItemDB._instance.AddItem(hitCollider.gameObject.GetComponent<ItemHolder>().GetItem());
            if (!canGrab)
            {
                return;
            }            
        }

        ShowObject(hitCollider, pressAgain);
        InputChecker.instance.ButtonsEnabled = pressAgain;
        pressAgain = !pressAgain;
    }

    public void ShowObject(GameObject obj, bool hasShown)
    {
        SetObjectCarry(obj, !hasShown);

        if (!hasShown)
        {   
            carriedObj.GetComponent<PickUpable>().canRotate = true;
            carriedObj.GetComponent<Collider>().enabled = false;
            carriedObj.layer = LayerMask.NameToLayer("pickUp");
        }
        else
        {
            Destroy(obj);
        }

        mainCam.GetComponent<Blur>().enabled = !hasShown;
    }
}
