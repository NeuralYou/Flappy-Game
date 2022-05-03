using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float[] list = new float[100];
        for(int i = 0; i < list.Length; i++)
		{
            list[i] = Random.Range(0.4f, 1) * 4;
		}

        string json = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText(@"C:\Users\Guy\Desktop\random_numbers.json", json);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
