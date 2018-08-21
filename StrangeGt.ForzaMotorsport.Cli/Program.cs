using StrangeGt.ForzaMotorsport.Data;
using StrangeGt.ForzaMotorsport.Listener;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace StrangeGt.ForzaMotorsport.Cli
{
    class Program
    {
        private static bool listening;
        private static Task listeningTask;
        private static CancellationTokenSource tokenSource;
        private static ForzaMotorsportContext context;

        static void Main(string[] args)
        {
            // Console.WriteLine(string.Format("{0}", System.Runtime.InteropServices.Marshal.SizeOf(typeof(UDPData))));SQLitePCL.Batteries.Init().'
            //Call this for create basic xalm
            CreateXaml();
            
            bool running = true;
            while (running)
            {
                WriteMenu();
                var key = Console.ReadKey(true);
                if (key.Modifiers == 0)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.R://Run
                            StartListener();
                            break;
                        case ConsoleKey.S://Run
                            StopListener();
                            break;
                        case ConsoleKey.Q:
                            running = false;
                            break;
                        default:
                            break;
                    }
                }
                else if ((key.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                {
                    if (key.Key == ConsoleKey.C)
                    {
                        StopListener();
                        running = false;
                    }
                }
            }
        }

        private static void CreateXaml()
        {
            var properties = typeof(UDPData).GetProperties();
            string tpl = @"<StackLayout Orientation=""Horizontal"">
< Label Text=""{0}:"" FontSize=""Small"" />
<Label Text=""{{Binding Item.{0},StringFormat='{{0,20:{1}}}'}}"" FontSize=""Micro""/>
</StackLayout>
";
            string xaml = @"<StackLayout Spacing=""20"" Padding=""15"">";
            foreach (PropertyInfo pi in properties)
            {
                xaml += string.Format(tpl, pi.Name, GetFormat(pi));
            }
            xaml += "</StackLayout>";
        }

        private static object GetFormat(PropertyInfo pi)
        {
          return  pi.PropertyType == typeof(Single) ? "F11" : "N0";
        }

        private static void CreateDB(string databasePath)
        {
            CloseDB();
            Directory.CreateDirectory(Path.GetDirectoryName(databasePath));
            context = new ForzaMotorsportContext(databasePath);
        }

        private static void CloseDB()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }



        private static void StopListener()
        {
            tokenSource.Cancel();
            listening = false;
        }

        private static void WriteMenu()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(string.Format("{0,-20}\r\nQ - Quit", IsListening() ? "S - Stop" : "R - Run"));
        }

        private static bool IsListening()
        {
            return listening;
        }

        private static void StartListener()
        {

            Console.Clear();
            listening = true;
            tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;
            cancellationToken.Register(() =>
            {

            });
            listeningTask = Task.Run(() =>
            {
                CreateDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StrangeGt.ForzaMotorsport", "databases", string.Format("Forzamotorsport_{0:yyyyMMddHHmmss}.sqlite", DateTime.Now.ToUniversalTime())));

                UDPListener listener = new UDPListener();
                listener.StartListenerAsync(new Action<UDPDataEventArgs>(UDPDataHandler), cancellationToken).GetAwaiter().GetResult(); ;
                listening = false;
                // CloseDB();

            }, cancellationToken);
        }

        private static void UDPDataHandler(UDPDataEventArgs args)
        {
            UDPData data = args.UdpReceiveResult.Buffer.Deserialize<UDPData>();
            WriteData(data);
        }
        private static void WriteData(UDPData data)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"IsRaceOn:\t{data.IsRaceOn,15:N0}\t\tTimestampMS:\t {data.TimestampMS,15:N0}", data);
            Console.WriteLine($"CarOrdinal:{data.CarOrdinal,7:N0}\tCarClass:{data.CarClass,4:N0}\tCarPerformanceIndex:{data.CarPerformanceIndex,4:N0}\tDrivetrainType:{data.DrivetrainType,4:N0}\tNumCylinders:{data.NumCylinders,4:N0}", data);
            Console.WriteLine($"EngineMaxRpm:\t{data.EngineMaxRpm,17:F11}\tEngineIdleRpm:\t {data.EngineIdleRpm,15:F11}\tCurrentEngineRpm: {data.CurrentEngineRpm,17:F11}", data);
            Console.WriteLine($"AccelerationX:\t{data.AccelerationX,17:F11}\tAccelerationY:\t {data.AccelerationY,15:F11}\tAccelerationZ:\t\t{data.AccelerationZ,15:F11}", data);
            Console.WriteLine($"VelocityX:\t{data.VelocityX,17:F11}\tVelocityY:\t {data.VelocityY,15:F11}\tVelocityZ:\t\t{data.VelocityZ,15:F11}", data);
            Console.WriteLine($"AngularVelocityX: {data.AngularVelocityX,15:F11}\tAngularVelocityY:{data.AngularVelocityY,15:F11}\tAngularVelocityZ:\t{data.AngularVelocityZ,15:F11}", data);
            Console.WriteLine($"Yaw:\t\t{data.Yaw,17:F11}\tPitch:\t\t {data.Pitch,15:F11}\tRoll:\t\t\t{data.Roll,15:F11}", data);
            Console.WriteLine($"NormalizedSuspensionTravelFrontLeft:\t{data.NormalizedSuspensionTravelFrontLeft,15:F11}\t\tNormalizedSuspensionTravelFrontRight:{data.NormalizedSuspensionTravelFrontRight,15:F11}", data);
            Console.WriteLine($"NormalizedSuspensionTravelRearLeft:\t{data.NormalizedSuspensionTravelRearLeft,15:F11}\t\tNormalizedSuspensionTravelRearRight: {data.NormalizedSuspensionTravelRearRight,15:F11}", data);
            Console.WriteLine($"TireSlipRatioFrontLeft:\t\t\t{data.TireSlipRatioFrontLeft,15:F11}\t\tTireSlipRatioFrontRight:\t\t {data.TireSlipRatioFrontRight,15:F11}", data);
            Console.WriteLine($"TireSlipRatioRearLeft:\t\t\t{data.TireSlipRatioRearLeft,15:F11}\t\tTireSlipRatioRearRight:\t\t\t {data.TireSlipRatioRearRight,15:F11}", data);
            Console.WriteLine($"WheelRotationSpeedFrontLeft:\t\t{data.WheelRotationSpeedFrontLeft,15:F11}\t\tWheelRotationSpeedFrontRight:\t\t {data.WheelRotationSpeedFrontRight,15:F11}", data);
            Console.WriteLine($"WheelRotationSpeedRearLeft:\t\t{data.WheelRotationSpeedRearLeft,15:F11}\t\tWheelRotationSpeedRearRight:\t\t {data.WheelRotationSpeedRearRight,15:F11}", data);
            Console.WriteLine($"WheelOnRumbleStripFrontLeft:\t\t{data.WheelOnRumbleStripFrontLeft,15:N0}\t\tWheelOnRumbleStripFrontRight:\t\t {data.WheelOnRumbleStripFrontRight,15:N0}", data);
            Console.WriteLine($"WheelOnRumbleStripRearLeft:\t\t{data.WheelOnRumbleStripRearLeft,15:N0}\t\tWheelOnRumbleStripRearRight:\t\t {data.WheelOnRumbleStripRearRight,15:N0}", data);
            Console.WriteLine($"WheelInPuddleDepthFrontLeft:\t\t{data.WheelInPuddleDepthFrontLeft,15:F11}\t\tWheelInPuddleDepthFrontRight:\t\t {data.WheelInPuddleDepthFrontRight,15:F11}", data);
            Console.WriteLine($"WheelInPuddleDepthRearLeft:\t\t{data.WheelInPuddleDepthRearLeft,15:F11}\t\tWheelInPuddleDepthRearRight:\t\t {data.WheelInPuddleDepthRearRight,15:F11}", data);
            Console.WriteLine($"SurfaceRumbleFrontLeft:\t\t\t{data.SurfaceRumbleFrontLeft,15:F11}\t\tSurfaceRumbleFrontRight:\t\t {data.SurfaceRumbleFrontRight,15:F11}", data);
            Console.WriteLine($"SurfaceRumbleRearLeft:\t\t\t{data.SurfaceRumbleRearLeft,15:F11}\t\tSurfaceRumbleRearRight:\t\t\t {data.SurfaceRumbleRearRight,15:F11}", data);
            Console.WriteLine($"TireSlipAngleFrontLeft:\t\t\t{data.TireSlipAngleFrontLeft,15:F11}\t\tTireSlipAngleFrontRight:\t\t {data.TireSlipAngleFrontRight,15:F11}", data);
            Console.WriteLine($"TireSlipAngleRearLeft:\t\t\t{data.TireSlipAngleRearLeft,15:F11}\t\tTireSlipAngleRearRight:\t\t\t {data.TireSlipAngleRearRight,15:F11}", data);
            Console.WriteLine($"TireCombinedSlipFrontLeft:\t\t{data.TireCombinedSlipFrontLeft,15:F11}\t\tTireCombinedSlipFrontRight:\t\t {data.TireCombinedSlipFrontRight,15:F11}", data);
            Console.WriteLine($"TireCombinedSlipRearLeft:\t\t{data.TireCombinedSlipRearLeft,15:F11}\t\tTireCombinedSlipRearRight:\t\t {data.TireCombinedSlipRearRight,15:F11}", data);
            Console.WriteLine($"SuspensionTravelMetersFrontLeft:\t{data.SuspensionTravelMetersFrontLeft,15:F11}\t\tSuspensionTravelMetersFrontRight:\t {data.SuspensionTravelMetersFrontRight,15:F11}", data);
            Console.WriteLine($"SuspensionTravelMetersRearLeft:\t\t{data.SuspensionTravelMetersRearLeft,15:F11}\t\tSuspensionTravelMetersRearRight:\t {data.SuspensionTravelMetersRearRight,15:F11}", data);
            SaveDataAsync(data);

        }

        private static async void SaveDataAsync(UDPData data)
        {
            if (context != null)
            {
                Data.Models.UDPData record = new Data.Models.UDPData()
                {
                    Created = DateTime.Now.ToUniversalTime(),
                    IsRaceOn = data.IsRaceOn, // = 1 when race is on. = 0 when in menus/race stopped …
                                              //u32
                    TimestampMS = data.TimestampMS, //Can overflow to 0 eventually
                                                    //f32
                    EngineMaxRpm = data.EngineMaxRpm,
                    //f32
                    EngineIdleRpm = data.EngineIdleRpm,
                    //f32
                    CurrentEngineRpm = data.CurrentEngineRpm,
                    //f32
                    AccelerationX = data.AccelerationX, //In the car's local space; X = right, Y = up, Z = forward
                    AccelerationY = data.AccelerationY,
                    //f32
                    AccelerationZ = data.AccelerationZ,
                    //f32
                    VelocityX = data.VelocityX, //In the car's local space; X = right, Y = up, Z = forward
                                                //f32
                    VelocityY = data.VelocityY,
                    //f32
                    VelocityZ = data.VelocityZ,
                    //f32
                    AngularVelocityX = data.AngularVelocityX, //In the car's local space; X = pitch, Y = yaw, Z = roll
                                                              //f32
                    AngularVelocityY = data.AngularVelocityY,
                    //f32
                    AngularVelocityZ = data.AngularVelocityZ,
                    //f32
                    Yaw = data.Yaw,
                    //f32
                    Pitch = data.Pitch,
                    //f32
                    Roll = data.Roll,
                    //f32
                    NormalizedSuspensionTravelFrontLeft = data.NormalizedSuspensionTravelFrontLeft, // Suspension travel normalized: 0.0f = max stretch; 1.0 = max compression
                    NormalizedSuspensionTravelFrontRight = data.NormalizedSuspensionTravelFrontRight,
                    //f32
                    NormalizedSuspensionTravelRearLeft = data.NormalizedSuspensionTravelRearLeft,
                    //f32
                    NormalizedSuspensionTravelRearRight = data.NormalizedSuspensionTravelRearRight,
                    //f32
                    TireSlipRatioFrontLeft = data.TireSlipRatioFrontLeft, // Tire normalized slip ratio, = 0 means 100% grip and |ratio| > 1.0 means loss of grip.
                    TireSlipRatioFrontRight = data.TireSlipRatioFrontRight,
                    //f32
                    TireSlipRatioRearLeft = data.TireSlipRatioRearLeft,
                    //f32
                    TireSlipRatioRearRight = data.TireSlipRatioRearRight,
                    //f32
                    WheelRotationSpeedFrontLeft = data.WheelRotationSpeedFrontLeft, // Wheel rotation speed radians/sec. 
                    WheelRotationSpeedFrontRight = data.WheelRotationSpeedFrontRight,
                    //f32
                    WheelRotationSpeedRearLeft = data.WheelRotationSpeedRearLeft,
                    //f32
                    WheelRotationSpeedRearRight = data.WheelRotationSpeedRearRight,
                    //s32
                    WheelOnRumbleStripFrontLeft = data.WheelOnRumbleStripFrontLeft, // = 1 when wheel is on rumble strip, = 0 when off.
                                                                                    //s32
                    WheelOnRumbleStripFrontRight = data.WheelOnRumbleStripFrontRight,
                    //s32
                    WheelOnRumbleStripRearLeft = data.WheelOnRumbleStripRearLeft,
                    //s32
                    WheelOnRumbleStripRearRight = data.WheelOnRumbleStripRearRight,
                    //f32
                    WheelInPuddleDepthFrontLeft = data.WheelInPuddleDepthFrontLeft, // = from 0 to 1, where 1 is the deepest puddle
                                                                                    //f32
                    WheelInPuddleDepthFrontRight = data.WheelInPuddleDepthFrontRight,
                    //f32
                    WheelInPuddleDepthRearLeft = data.WheelInPuddleDepthRearLeft,
                    //f32
                    WheelInPuddleDepthRearRight = data.WheelInPuddleDepthRearRight,
                    //f32
                    SurfaceRumbleFrontLeft = data.SurfaceRumbleFrontLeft, // Non-dimensional surface rumble values passed to controller force feedback
                                                                          //f32
                    SurfaceRumbleFrontRight = data.SurfaceRumbleFrontRight,
                    //f32
                    SurfaceRumbleRearLeft = data.SurfaceRumbleRearLeft,
                    //f32
                    SurfaceRumbleRearRight = data.SurfaceRumbleRearRight,
                    //f32
                    TireSlipAngleFrontLeft = data.TireSlipAngleFrontLeft, // Tire normalized slip angle, = 0 means 100% grip and |angle| > 1.0 means loss of grip.
                                                                          //f32
                    TireSlipAngleFrontRight = data.TireSlipAngleFrontRight,
                    //f32
                    TireSlipAngleRearLeft = data.TireSlipAngleRearLeft,
                    //f32
                    TireSlipAngleRearRight = data.TireSlipAngleRearRight,
                    //f32
                    TireCombinedSlipFrontLeft = data.TireCombinedSlipFrontLeft, // Tire normalized combined slip, = 0 means 100% grip and |slip| > 1.0 means loss of grip.
                                                                                //f32
                    TireCombinedSlipFrontRight = data.TireCombinedSlipFrontRight,
                    //f32
                    TireCombinedSlipRearLeft = data.TireCombinedSlipRearLeft,
                    //f32
                    TireCombinedSlipRearRight = data.TireCombinedSlipRearRight,
                    //f32
                    SuspensionTravelMetersFrontLeft = data.SuspensionTravelMetersFrontLeft, // Actual suspension travel in meters
                                                                                            //f32
                    SuspensionTravelMetersFrontRight = data.SuspensionTravelMetersFrontRight,
                    //f32
                    SuspensionTravelMetersRearLeft = data.SuspensionTravelMetersRearLeft,
                    //f32
                    SuspensionTravelMetersRearRight = data.SuspensionTravelMetersRearRight,
                    //s32
                    CarOrdinal = data.CarOrdinal, //Unique ID of the car make/model
                                                  //s32
                    CarClass = data.CarClass, //Between 0 (D -- worst cars) and 7 (X class -- best cars) inclusive 
                                              //s32
                    CarPerformanceIndex = data.CarPerformanceIndex, //Between 100 (slowest car) and 999 (fastest car) inclusive
                                                                    //s32
                    DrivetrainType = data.DrivetrainType, //Corresponds to EDrivetrainType; 0 = FWD, 1 = RWD, 2 = AWD
                                                          //s32
                    NumCylinders = data.NumCylinders, //Number of cylinders in the engine
                };
                await context.UDPDatas.AddAsync(record);
                await context.SaveChangesAsync();
            }

        }
    }
}
