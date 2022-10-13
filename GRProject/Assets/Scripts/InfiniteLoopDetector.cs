using UnityEngine;
using System;
public static class InfiniteLoopDetector                    // 무한루프 감지
{
    private static string prevPoint = "";
    private static int detectionCount = 0;
    private const int DetectionThreshold = 100000;

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Run(
        [System.Runtime.CompilerServices.CallerMemberName] string mn = "",
        [System.Runtime.CompilerServices.CallerFilePath] string fp = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int ln = 0
    )
    {
        string currentPoint = $"{fp}:{ln}, {mn}()";

        if (prevPoint == currentPoint)
            detectionCount++;
        else
            detectionCount = 0;

        if (detectionCount > DetectionThreshold)
            throw new Exception($"Infinite Loop Detected: \n{currentPoint}\n\n");

        prevPoint = currentPoint;
    }

#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoadMethod]
    private static void Init()
    {
        UnityEditor.EditorApplication.update += () =>
        {
            detectionCount = 0;
        };
    }
#endif
    //InfiniteLoopDetector.Run();   <- 이렇게 한 줄 추가 작성
}
    