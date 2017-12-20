using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Rigidbody cannonballInstance;
    public Transform cannon;
    //public Transform target;
    public Vector3 velocity2;
    public Vector3 add;
    //public Quaternion angle2;
    public Vector3 angadd;
    private float angle = 45f;

    public GameObject b;
    public GameObject au;
    public GameObject ad;
    public GameObject vu;
    public GameObject vd;
    private Quaternion rot;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float vel = 12f;

    public Text VELOCITY;
    public Text ANGLE;
    private Quaternion rots;

    private void Start()
    {
        rot = bulletPrefab.transform.rotation;
        rots = rot;
        ANGLE.text = "Angle =  " + (rots.eulerAngles.x - 270f) + " degrees";
        VELOCITY.text = "Velocity =  " + vel.ToString() + " m/s";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.position == b.transform.position)
                {
                    newFire();//hitInfo.point);
                }
                else if(hitInfo.transform.position == au.transform.position)
                {
                    rot *= Quaternion.Euler(-1, 0, 0);
                    cannon.transform.rotation *= Quaternion.Euler(-1, 0, 0);
                    rots *= Quaternion.Euler(1, 0, 0);
                    ANGLE.text = "Angle =  " + (rots.eulerAngles.x - 270f) + " degrees";
                }
                else if (hitInfo.transform.position == ad.transform.position)
                {
                    rot *= Quaternion.Euler(1, 0, 0);
                    cannon.transform.rotation *= Quaternion.Euler(1, 0, 0);
                    rots *= Quaternion.Euler(-1, 0, 0);
                    ANGLE.text = "Angle =  " + (rots.eulerAngles.x - 270f) + " degrees";
                }
                else if (hitInfo.transform.position == vu.transform.position)
                {
                    vel += 1;
                    VELOCITY.text = "Velocity =  " + vel.ToString() + " m/s";
                }
                else if (hitInfo.transform.position == vd.transform.position)
                {
                    vel -= 1;
                    VELOCITY.text = "Velocity =  " + vel.ToString() + " m/s";
                }
            }
        }
        /*else if (Input.GetKeyDown("f"))
        {
            FireCannonAtPoint();            
        }*/
    }

    private void FireCannonAtPoint(Vector3 point)
    {
        var velocity = BallisticVelocity(point, angle);
        Debug.Log("Firing at " + point + " velocity " + velocity);

        cannonballInstance.transform.position = transform.position;
        cannonballInstance.velocity = velocity;
    }

    public void FireCannonAtPoint()
    {
       cannonballInstance.transform.position = transform.position;
       cannonballInstance.velocity = velocity2;
    }

    private Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }

    public void newFire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, rot);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * vel;
        Destroy(bullet, 20.0f);
    }
}