using UnityEngine;

public class DissolveObject : MonoBehaviour,Action {
    
    private Material mat;
    private float max;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool dissolving = false;

    private float val;
    private bool isOn = false;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        val = 0;
    }

    private void Update()
    {
        if (isOn)
        {
            if (dissolving && val < max)
            {
                mat.SetFloat("_DissolveY", val);
                val += Time.deltaTime * speed;
            }
            else if (!dissolving && val > max)
            {
                mat.SetFloat("_DissolveY", val);
                val -= Time.deltaTime * speed;
            }
            else
            {
                isOn = false;
                if (dissolving)
                {
                    GetComponent<Collider>().enabled = false;
                    GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    public void PerformAction()
    {
        if (!isOn)
        {

            dissolving = !dissolving;

            if (!dissolving)
            {
                GetComponent<Collider>().enabled = true;
                GetComponent<MeshRenderer>().enabled = true;
                max = 0;
            }
            else
            {
                max = 5;
            }

            isOn = true;
        }
    }
}
