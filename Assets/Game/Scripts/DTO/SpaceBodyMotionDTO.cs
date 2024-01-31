namespace Game.Scripts.DTO
{
    public class SpaceBodyMotionDTO
    {
        public float AxisState;
        public float AxisTilt;
        public float AxisPerSecond;

        public SpaceBodyMotionDTO(float axisState, float axisTilt, float axisPerSecond)
        {
            AxisState = axisState;
            AxisTilt = axisTilt;
            AxisPerSecond = axisPerSecond;
        }
    }
}