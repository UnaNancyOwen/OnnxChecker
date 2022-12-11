using System;
using System.IO;
using UnityEngine;
using Unity.Barracuda.ONNX;

public class OnnxCheckerScript : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_SERVER
        return;
#endif

        var default_handler = Debug.unityLogger.logHandler;
        var custom_handler = new CustomLogHandler(default_handler);
        Debug.unityLogger.logHandler = custom_handler;

        try
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length <= 1)
            {
                throw new ArgumentException("Please specify onnx file path. (onnx_checker ./model.onnx)");
            }

            var path = args[1];
            if (Path.GetExtension(path) != ".onnx")
            {
                throw new ArgumentException("Please specify onnx file path. (onnx_checker ./model.onnx)");
            }
            var optimize = true;
            var converter = new ONNXModelConverter(optimize, true);
            var model = converter.Convert(path);
        }
        catch (OnnxImportException e)
        {
            Debug.LogError(e.Message);
        }
        catch (OnnxLayerImportException e)
        {
            Debug.LogError(e.Message);
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.Message);
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(false);
        Debug.unityLogger.logHandler = default_handler;
        Application.Quit();
    }
}
