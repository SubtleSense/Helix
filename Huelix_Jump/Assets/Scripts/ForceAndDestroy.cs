using System.Collections;
using UnityEngine;



public class ForceAndDestroy : MonoBehaviour
{
    public GameObject[] items;

    public void PlatformDestroyer()
    {
        for (int i = 0; i <  items.Length; i++)
        {
            Debug.Log("i = " + i);
            items[i].gameObject.AddComponent<Rigidbody>();
            items[i].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            items[i].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(1, 10), ForceMode.Impulse);
            StartCoroutine(Destroyer());
        }

    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}