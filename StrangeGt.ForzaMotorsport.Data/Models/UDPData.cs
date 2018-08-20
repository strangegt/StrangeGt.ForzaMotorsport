using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrangeGt.ForzaMotorsport.Data.Models
{
    [Table("UDPData")]
    public class UDPData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
         [Column(TypeName ="DATETIME")]
       public DateTime Created { get; set; }
        //s32
        public Int32 IsRaceOn { get; set;} // = 1 when race is on. = 0 when in menus/race stopped …
        //u32
        public UInt32 TimestampMS { get; set;} //Can overflow to 0 eventually
        //f32
        public Single EngineMaxRpm { get; set;}
        //f32
        public Single EngineIdleRpm { get; set;}
        //f32
        public Single CurrentEngineRpm { get; set;}
        //f32
        public Single AccelerationX { get; set;} //In the car's local space; X = right, Y = up, Z = forward
        public Single AccelerationY { get; set;}
        //f32
        public Single AccelerationZ { get; set;}
        //f32
        public Single VelocityX { get; set;} //In the car's local space; X = right, Y = up, Z = forward
        //f32
        public Single VelocityY { get; set;}
        //f32
        public Single VelocityZ { get; set;}
        //f32
        public Single AngularVelocityX { get; set;} //In the car's local space; X = pitch, Y = yaw, Z = roll
        //f32
        public Single AngularVelocityY { get; set;}
        //f32
        public Single AngularVelocityZ { get; set;}
        //f32
        public Single Yaw { get; set;}
        //f32
        public Single Pitch { get; set;}
        //f32
        public Single Roll { get; set;}
        //f32
        public Single NormalizedSuspensionTravelFrontLeft { get; set;} // Suspension travel normalized: 0.0f = max stretch; 1.0 = max compression
        public Single NormalizedSuspensionTravelFrontRight { get; set;}
        //f32
        public Single NormalizedSuspensionTravelRearLeft { get; set;}
        //f32
        public Single NormalizedSuspensionTravelRearRight { get; set;}
        //f32
        public Single TireSlipRatioFrontLeft { get; set;} // Tire normalized slip ratio, = 0 means 100% grip and |ratio| > 1.0 means loss of grip.
        public Single TireSlipRatioFrontRight { get; set;}
        //f32
        public Single TireSlipRatioRearLeft { get; set;}
        //f32
        public Single TireSlipRatioRearRight { get; set;}
        //f32
        public Single WheelRotationSpeedFrontLeft { get; set;} // Wheel rotation speed radians/sec. 
        public Single WheelRotationSpeedFrontRight { get; set;}
        //f32
        public Single WheelRotationSpeedRearLeft { get; set;}
        //f32
        public Single WheelRotationSpeedRearRight { get; set;}
        //s32
        public Int32 WheelOnRumbleStripFrontLeft { get; set;} // = 1 when wheel is on rumble strip, = 0 when off.
        //s32
        public Int32 WheelOnRumbleStripFrontRight { get; set;}
        //s32
        public Int32 WheelOnRumbleStripRearLeft { get; set;}
        //s32
        public Int32 WheelOnRumbleStripRearRight { get; set;}
        //f32
        public Single WheelInPuddleDepthFrontLeft { get; set;} // = from 0 to 1, where 1 is the deepest puddle
        //f32
        public Single WheelInPuddleDepthFrontRight { get; set;}
        //f32
        public Single WheelInPuddleDepthRearLeft { get; set;}
        //f32
        public Single WheelInPuddleDepthRearRight { get; set;}
        //f32
        public Single SurfaceRumbleFrontLeft { get; set;} // Non-dimensional surface rumble values passed to controller force feedback
        //f32
        public Single SurfaceRumbleFrontRight { get; set;}
        //f32
        public Single SurfaceRumbleRearLeft { get; set;}
        //f32
        public Single SurfaceRumbleRearRight { get; set;}
        //f32
        public Single TireSlipAngleFrontLeft { get; set;} // Tire normalized slip angle, = 0 means 100% grip and |angle| > 1.0 means loss of grip.
        //f32
        public Single TireSlipAngleFrontRight { get; set;}
        //f32
        public Single TireSlipAngleRearLeft { get; set;}
        //f32
        public Single TireSlipAngleRearRight { get; set;}
        //f32
        public Single TireCombinedSlipFrontLeft { get; set;} // Tire normalized combined slip, = 0 means 100% grip and |slip| > 1.0 means loss of grip.
        //f32
        public Single TireCombinedSlipFrontRight { get; set;}
        //f32
        public Single TireCombinedSlipRearLeft { get; set;}
        //f32
        public Single TireCombinedSlipRearRight { get; set;}
        //f32
        public Single SuspensionTravelMetersFrontLeft { get; set;} // Actual suspension travel in meters
        //f32
        public Single SuspensionTravelMetersFrontRight { get; set;}
        //f32
        public Single SuspensionTravelMetersRearLeft { get; set;}
        //f32
        public Single SuspensionTravelMetersRearRight { get; set;}
        //s32
        public Int32 CarOrdinal { get; set;} //Unique ID of the car make/model
        //s32
        public Int32 CarClass { get; set;} //Between 0 (D -- worst cars) and 7 (X class -- best cars) inclusive 
        //s32
        public Int32 CarPerformanceIndex { get; set;} //Between 100 (slowest car) and 999 (fastest car) inclusive
        //s32
        public Int32 DrivetrainType { get; set;} //Corresponds to EDrivetrainType; 0 = FWD, 1 = RWD, 2 = AWD
        //s32
        public Int32 NumCylinders { get; set;} //Number of cylinders in the engine
    }
}
