using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrowOff : MonoBehaviour {


    private void Start() {
        this.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => r.isKinematic = true);
        StartCoroutine(this.browOffWithDelay(3.0f));
    }

    private IEnumerator ragdollWithDelay(float wait) {
        yield return new WaitForSeconds(wait);
        this.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => r.isKinematic = false);
    }

    private IEnumerator browOffWithDelay(float wait) {
        yield return new WaitForSeconds(wait);
        this.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            r.isKinematic = false;
            r.AddForce(-this.transform.forward * 50.0f, ForceMode.Impulse);
        });
    }
}
