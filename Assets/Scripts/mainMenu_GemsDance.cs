using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu_GemsDance : MonoBehaviour
{
    float dance_StartTime;
    float dance_LastTime;
    float dance_Duration = 1.75f;
    float danceSpeed = 0.02f;
    bool rotatingNegatively;

    // Start is called before the first frame update
    void Start()
    {
        dance_StartTime = Time.time;
        rotatingNegatively = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Dance());
    }

    private IEnumerator Dance()
    {
        //Debug.Log("GEM: Rotate");

        while (rotatingNegatively)
        {
            this.transform.Rotate(0.0f, 0.0f, (danceSpeed) * -1, Space.Self);
            //Debug.Log("NEGATIVE: " + this.transform.rotation);
            if (this.transform.rotation.x <= -0.5)
                rotatingNegatively = !rotatingNegatively;

            yield return 0;
        }

        while (!rotatingNegatively)
        {
            this.transform.Rotate(0.0f, 0.0f, (danceSpeed) * 1, Space.Self);
            //Debug.Log("POSTITIVE: " + this.transform.rotation.z);
            if (this.transform.rotation.x >= 0)
                rotatingNegatively = !rotatingNegatively;

            yield return 0;
        }
    }
}
