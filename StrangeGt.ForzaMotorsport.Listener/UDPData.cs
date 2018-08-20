using System;
using System.Collections.Generic;
using System.Text;

namespace StrangeGt.ForzaMotorsport.Listener
{
    public struct UDPData
    {
        public static UDPData FromByteAttay(byte[] data){
            return data.Deserialize<UDPData>();
        }
        //s32
        public Int32 IsRaceOn { get; } // = 1 when race is on. = 0 when in menus/race stopped …
        //u32
        public UInt32 TimestampMS { get; } //Can overflow to 0 eventually
        //f32
        public Single EngineMaxRpm { get; }
        //f32
        public Single EngineIdleRpm { get; }
        //f32
        public Single CurrentEngineRpm { get; }
        //f32
        public Single AccelerationX { get; } //In the car's local space; X = right, Y = up, Z = forward
        public Single AccelerationY { get; }
        //f32
        public Single AccelerationZ { get; }
        //f32
        public Single VelocityX { get; } //In the car's local space; X = right, Y = up, Z = forward
        //f32
        public Single VelocityY { get; }
        //f32
        public Single VelocityZ { get; }
        //f32
        public Single AngularVelocityX { get; } //In the car's local space; X = pitch, Y = yaw, Z = roll
        //f32
        public Single AngularVelocityY { get; }
        //f32
        public Single AngularVelocityZ { get; }
        //f32
        public Single Yaw { get; }
        //f32
        public Single Pitch { get; }
        //f32
        public Single Roll { get; }
        //f32
        public Single NormalizedSuspensionTravelFrontLeft { get; } // Suspension travel normalized: 0.0f = max stretch; 1.0 = max compression
        public Single NormalizedSuspensionTravelFrontRight { get; }
        //f32
        public Single NormalizedSuspensionTravelRearLeft { get; }
        //f32
        public Single NormalizedSuspensionTravelRearRight { get; }
        //f32
        public Single TireSlipRatioFrontLeft { get; } // Tire normalized slip ratio, = 0 means 100% grip and |ratio| > 1.0 means loss of grip.
        public Single TireSlipRatioFrontRight { get; }
        //f32
        public Single TireSlipRatioRearLeft { get; }
        //f32
        public Single TireSlipRatioRearRight { get; }
        //f32
        public Single WheelRotationSpeedFrontLeft { get; } // Wheel rotation speed radians/sec. 
        public Single WheelRotationSpeedFrontRight { get; }
        //f32
        public Single WheelRotationSpeedRearLeft { get; }
        //f32
        public Single WheelRotationSpeedRearRight { get; }
        //s32
        public Int32 WheelOnRumbleStripFrontLeft { get; } // = 1 when wheel is on rumble strip, = 0 when off.
        //s32
        public Int32 WheelOnRumbleStripFrontRight { get; }
        //s32
        public Int32 WheelOnRumbleStripRearLeft { get; }
        //s32
        public Int32 WheelOnRumbleStripRearRight { get; }
        //f32
        public Single WheelInPuddleDepthFrontLeft { get; } // = from 0 to 1, where 1 is the deepest puddle
        //f32
        public Single WheelInPuddleDepthFrontRight { get; }
        //f32
        public Single WheelInPuddleDepthRearLeft { get; }
        //f32
        public Single WheelInPuddleDepthRearRight { get; }
        //f32
        public Single SurfaceRumbleFrontLeft { get; } // Non-dimensional surface rumble values passed to controller force feedback
        //f32
        public Single SurfaceRumbleFrontRight { get; }
        //f32
        public Single SurfaceRumbleRearLeft { get; }
        //f32
        public Single SurfaceRumbleRearRight { get; }
        //f32
        public Single TireSlipAngleFrontLeft { get; } // Tire normalized slip angle, = 0 means 100% grip and |angle| > 1.0 means loss of grip.
        //f32
        public Single TireSlipAngleFrontRight { get; }
        //f32
        public Single TireSlipAngleRearLeft { get; }
        //f32
        public Single TireSlipAngleRearRight { get; }
        //f32
        public Single TireCombinedSlipFrontLeft { get; } // Tire normalized combined slip, = 0 means 100% grip and |slip| > 1.0 means loss of grip.
        //f32
        public Single TireCombinedSlipFrontRight { get; }
        //f32
        public Single TireCombinedSlipRearLeft { get; }
        //f32
        public Single TireCombinedSlipRearRight { get; }
        //f32
        public Single SuspensionTravelMetersFrontLeft { get; } // Actual suspension travel in meters
        //f32
        public Single SuspensionTravelMetersFrontRight { get; }
        //f32
        public Single SuspensionTravelMetersRearLeft { get; }
        //f32
        public Single SuspensionTravelMetersRearRight { get; }
        //s32
        public Int32 CarOrdinal { get; } //Unique ID of the car make/model
        //s32
        public Int32 CarClass { get; } //Between 0 (D -- worst cars) and 7 (X class -- best cars) inclusive 
        //s32
        public Int32 CarPerformanceIndex { get; } //Between 100 (slowest car) and 999 (fastest car) inclusive
        //s32
        public Int32 DrivetrainType { get; } //Corresponds to EDrivetrainType; 0 = FWD, 1 = RWD, 2 = AWD
        //s32
        public Int32 NumCylinders { get; } //Number of cylinders in the engine
    }
}
