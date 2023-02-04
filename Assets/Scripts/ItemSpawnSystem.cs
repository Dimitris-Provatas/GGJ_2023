using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawnSystem : MonoBehaviour
{
  public GameObject[] prefabs;
  public Transform[] coordinates;

  /// <summary>
  /// Shuffles elements in array of any type. Then returns the corresponding Queue.
  /// </summary>
  /// <param name="array">An array of any size.</param>
  /// <typeparam name="T">Any type.</typeparam>
  /// <returns>A Queue of type T.</returns>
  private Queue<T> Shuffle<T>(T[] array)
  {
    // initialize a queue.
    Queue<T> shuffledQueue = new Queue<T>();

    // iterator helper.
    int n = array.Length;

    // iterate through the entire array.
    while (n > 1)
    {
      // decrease n by 1 and select a random number k between 0 and the size of the list - 1.
      int k = Random.Range(0, n--);

      // switch n and k positions
      (array[n], array[k]) = (array[k], array[n]);
    }

    // then create and return the corresponding queue.
    foreach (var t in array)
    {
      shuffledQueue.Enqueue(t);
    }

    return shuffledQueue;
  }


  void Start()
  {
    // Save the positions and the prefabs to Queues
    Queue<GameObject> prefabs_queue = Shuffle(prefabs);
    Queue<Transform> coordinates_queue = Shuffle(coordinates);

    // Don't continue if there are more Prefabs than Possible Locations
    if (coordinates_queue.Count < prefabs_queue.Count)
    {
      Debug.Log("There are less possible positions than prefabs. Please add more positions.");
      return;
    }

    // Instantiate each prefab.
    while (prefabs_queue.Count != 0)
    {
      Transform t = coordinates_queue.Dequeue();
      Instantiate(prefabs_queue.Dequeue(), t.position, t.rotation);
    }
  }
}
