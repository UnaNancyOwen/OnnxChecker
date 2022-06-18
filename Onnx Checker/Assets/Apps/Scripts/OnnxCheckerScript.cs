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
            var converter = new ONNXModelConverter(optimize);
            var model = converter.Convert(path);
        }
        catch (OnnxImportException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(false);
            Application.Quit();
        }
    }
}
