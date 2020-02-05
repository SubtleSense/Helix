using System.Collections;
using UnityEngine;



public class ForceAndDestroy : MonoBehaviour
{
    public GameObject[] items;      //массив сеуций

    public void PlatformDestroyer()     //метод ломающий плаьформы
    {
        for (int i = 0; i <  items.Length; i++)
        {
            items[i].gameObject.AddComponent<Rigidbody>();                                                                                  //добавил компонент Rigidbody.
            items[i].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));      //крутящий момент для Rigidbody.
            items[i].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(1, 10), ForceMode.Impulse);                    //указал вектор силы, ее мощность и характер воздействия.
            items[i].gameObject.GetComponent<Collider>().enabled = false;                                                                   //отключил коллайдер.
            StartCoroutine(Destroyer());
        }

    }

    IEnumerator Destroyer()             //уничтожил текущий платформу через секунду после срабатывания PlatformDestroyer().
    {
        yield return new WaitForSeconds(1f);   //таймер
        Destroy(this.gameObject);              //дестроер
    }
}