using Lab5;
using System.Text;
using System.IO;

internal class Program
{
    static List<Camera> cameras;

    static void PrintCameras()
    {
        foreach (Camera camera in cameras)
        {
            Console.WriteLine(camera.Info().Replace('i', 'i'));
        }
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        cameras = new List<Camera>();
        FileStream fs = new FileStream("cameras.cameras", FileMode.Open);
        BinaryReader reader = new BinaryReader(fs, Encoding.UTF8);
        try
        {
            Console.WriteLine("Read data from the file... \n");
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Camera camera = new Camera();
                for (int i = 1; i <= 8; i++)
                {
                    switch (i)
                    {
                        case 1:
                            camera.Brand = reader.ReadString();
                            break;
                        case 2:
                            camera.Model = reader.ReadString();
                            break;
                        case 3:
                            camera.CountryOfOrigin = reader.ReadString();
                            break;
                        case 4:
                            camera.SensorType = reader.ReadString();
                            break;
                        case 5:
                            camera.SensorResolution = reader.ReadInt32();
                            break;
                        case 6:
                            camera.LensType = reader.ReadString();
                            break;
                        case 7:
                            camera.VideoFormat = reader.ReadString();
                            break;
                        case 8:
                            camera.WeatherSealing = reader.ReadBoolean();
                            break;
                    }
                }
                cameras.Add(camera); 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: {0}", ex.Message);
        }
        finally
        {
            reader.Close();
        }
        Console.WriteLine("Unsorted list of cameras: {0}", cameras.Count);
        PrintCameras();
        cameras.Sort();
        Console.WriteLine("Sorted list of cameras: {0}", cameras.Count);
        PrintCameras();
        Console.WriteLine("Add a new entry: Sony Alpha 7 IV");
        Camera cameraSony = new Camera("Sony", "Alpha 7 IV", "Japan", 
            "Full-frame Exmor R CMOS", 33, "E-mount", "4K, Full HD", true);
        cameras.Add(cameraSony);
        cameras.Sort();
        Console.WriteLine("List of cameras: {0}", cameras.Count);
        PrintCameras();

        Console.WriteLine("Delete the last value");
        cameras.RemoveAt(cameras.Count - 1);

        Console.WriteLine("List of cameras: {0}", cameras.Count);
        PrintCameras();

        Console.ReadKey();
    }
}