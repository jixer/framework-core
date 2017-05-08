[Back to root of project](../../../)

# MemberwiseCompare
This class can be used to compare two different reference objects to determine equality amongst the primitive types in these complex types.

# Example Usage
```csharp
var t1 = new TestClass1() { IntProperty = 5 };
var t2 = new TestClass1() { IntProperty = 5 };
bool compareResult = MembershipCompare.AreEqual<TestClass1>(t1, t2); // will return true
```

# Lazy Loading
The solution will generate an eqaulity function on the first call to MemberwiseCompare.AreEqual<T1>(...).  This lazy loading approach will result in a slower comparison time for the first call.  Subsequent calls will leverage the cached function that is stored in a Dictionary, which results in a lookup time of O(1), which improves performance.

To pre-cache the function in the dictionary, you can call the register method directly during app initialization and bootstrapping procedures:
```csharp
MemberwiseCompare.Register<SomeClass>();
```


# MembershipwiseCompare Performance Results
Since the compare utility pre-compiles the compare function for each complex type, the comparisons should be fairly quick relative to an alternative comparison approach that uses reflection.

> Results will vary based upon hardware used and availability of resources on the computer where the test is being ran.

## Test Run 1
Results here are slowest because lazy loading is invoked for the first call, which is slower due to the fact that the equality function has to be generated.

```text
Pass #1 test iterations: 10000
Values are equal: True
First time: 1500868 ticks, 150.0868 ms
Second time: 251 ticks, 0.0251 ms
Last time: 153 ticks, 0.0153 ms
Average time: 337.5464 ticks, 0.0337546399999996 ms
Min time: 107 ticks, 0.0107 ms
Max time: 1500868 ticks, 150.0868 ms
Total time: 3375464 ticks, 337.546399999996 ms
```

## Test Run 2
Results here are faster than Test Run 1, since the equality function for TestClass1 is already loaded in the \_func dictionary

```text
Pass #2 test iterations: 10000
Values are equal: True
First time: 318 ticks, 0.0318 ms
Second time: 174 ticks, 0.0174 ms
Last time: 169 ticks, 0.0169 ms
Average time: 176.8364 ticks, 0.0176836399999996 ms
Min time: 92 ticks, 0.0092 ms
Max time: 6209 ticks, 0.6209 ms
Total time: 1768364 ticks, 176.836399999996 ms
```

## Test Run 3
Results here will be the fastest since the objects are different.  This is the case due to the fact that not all of the comparisons in the compiled LINQ expression will be executed.

```text
Pass #3 test iterations: 10000
Values are equal: False
First time: 302 ticks, 0.0302 ms
Second time: 107 ticks, 0.0107 ms
Last time: 107 ticks, 0.0107 ms
Average time: 98.4815 ticks, 0.00984815000000054 ms
Min time: 56 ticks, 0.0056 ms
Max time: 836 ticks, 0.0836 ms
Total time: 984815 ticks, 98.4815000000054 ms
```

# References
This approach is based upon a solution created by Bradley Smith, which is documented here: [http://www.brad-smith.info/blog/archives/385]

This implementation builds on Bradley's solution, but was implemented in a PCL and includes logic to account for NULL values and complex type class properties.
