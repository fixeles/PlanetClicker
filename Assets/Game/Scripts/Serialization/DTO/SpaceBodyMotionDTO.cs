namespace Game.Scripts.Serialization.DTO
{
    public class SpaceBodyMotionDTO
    {
        public readonly float AxisState;
        public readonly float AxisTilt;
        public readonly float AxisPerSecond;

        public SpaceBodyMotionDTO(float axisState, float axisTilt, float axisPerSecond)
        {
            AxisState = axisState;
            AxisTilt = axisTilt;
            AxisPerSecond = axisPerSecond;
        }
    }
}