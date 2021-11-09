using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;

public class ArcadeKartPowerup : MonoBehaviour {

    public ArcadeKart.StatPowerup boostStats = new ArcadeKart.StatPowerup
    {
        MaxTime = 5
    };

    public bool isCoolingDown { get; private set; }
    public float lastActivatedTimestamp { get; private set; }

    public float cooldown = 5f;

    public bool disableGameObjectWhenActivated;
    public UnityEvent onPowerupActivated;
    public UnityEvent onPowerupFinishCooldown;

    //JF should infer the base material and only need to know what the cooldown material needs to be
    public Material m_Material;
    public Material coolDownMaterial;

    private void Awake()
    {
        lastActivatedTimestamp = -9999f;
    }


    private void Update()
    {
        if (isCoolingDown) { 

            if (Time.time - lastActivatedTimestamp > cooldown) {
                //finished cooldown!
                SetChildMaterials(m_Material);
                isCoolingDown = false;
                onPowerupFinishCooldown.Invoke();
                
            }
        }
    }

    private void SetChildMaterials(Material mat)
    {
        GameObject child;
        int numOfChildren = transform.childCount;

        for (int i = 0; i < numOfChildren; i++)
        {
            child = transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material = mat;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isCoolingDown) return;

        var rb = other.attachedRigidbody;
        if (rb) {

            var kart = rb.GetComponent<ArcadeKart>();

            if (kart)
            { 
                lastActivatedTimestamp = Time.time;
                kart.AddPowerup(this.boostStats);
                onPowerupActivated.Invoke();
                SetChildMaterials(coolDownMaterial);
                isCoolingDown = true;

                if (disableGameObjectWhenActivated) this.gameObject.SetActive(false);
            }
        }
    }

}
