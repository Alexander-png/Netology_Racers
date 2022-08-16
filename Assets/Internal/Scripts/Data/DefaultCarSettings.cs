namespace Cars_5_5.Assets.Internal.Scripts.Data
{
    public class CarSettings
    {
        public static PlayerCarData Defaults => new PlayerCarData()
        {
            MaxMotorTorgue = 950f,
            MaxTurnAngle = 30f,
            MaxBrakeTorgue = 1500f,
            CarMass = 1200,
            DownForce = 5,
        };
    }
}
