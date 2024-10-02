using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    enum TestEnum
    {
        a = 0,
        b = 1,
        c = 2,
        d = 3
    }
    TestEnum testEnum = TestEnum.a;
    List<int> list = new List<int>();

    void Start()
    {
        testEnum = (TestEnum)Random.Range(0, 4);
        int testInt = list[(int)testEnum];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
